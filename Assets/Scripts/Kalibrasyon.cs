using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Kalibrasyon : MonoBehaviour {

    public GameObject Halka;

    public Text bildirim;

    public Text axisUI;
    public Text CalibratedUI;
    public Text KalibrasyonXUI;

    bool kalibrasyonOnay;

    

    public float maxHareket = 0.3049313f;

    float KalibrasyonX = 0;

    // Use this for initialization
    void Start () {
        KalibrasyonX = PlayerPrefs.GetFloat("KalibrasyonX");
	}
	
	// Update is called once per frame
	void Update () {
        axisUI.text = Input.acceleration.x.ToString();
        KalibrasyonXUI.text = KalibrasyonX.ToString();
        CalibratedUI.text = ((Input.acceleration.x * 4) - KalibrasyonX).ToString();

        if(Input.GetKeyDown("escape"))
        {
            anaMenu();
        }

    }

    private void FixedUpdate()
    {
        float translation = (Input.GetAxis("Horizontal") + ((Input.acceleration.x - KalibrasyonX) * 4 ) + Input.GetAxis("Mouse X")) * 3f;
        translation *= Time.deltaTime;
        
        if(Halka.gameObject.transform.position.x > maxHareket)
        {
            Halka.gameObject.transform.position = new Vector3(maxHareket, 0, 0);
            //translation = maxHareket;
        }
        else if(Halka.gameObject.transform.position.x < -maxHareket)
        {
            Halka.gameObject.transform.position = new Vector3(-maxHareket, 0, 0);
            //translation = -maxHareket;
        }
        
        Halka.transform.Translate(translation, 0, 0);
        //Halka.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(translation, 0,0);
        //Halka.gameObject.GetComponent<Rigidbody>().AddForce(translation, 0, 0);

        /*
        float rayCastLength = 0.446f;
        RaycastHit hit;

        if (Physics.Raycast(Vector3.zero, transform.right, out hit, rayCastLength))
        {
            Debug.DrawLine(Vector3.zero, hit.point);
            Debug.Log("Sağ");
            Halka.gameObject.transform.position = new Vector3(-maxHareket, 0,0);
        }

        if (Physics.Raycast(Vector3.zero, -transform.right, out hit, rayCastLength))
        {
            Debug.DrawLine(Vector3.zero, hit.point);
            Debug.Log("Sol");
            Halka.gameObject.transform.position = new Vector3(maxHareket, 0, 0);
        }
        */
    }

    public void KalibrasyonOnayla()
    {
        if (!kalibrasyonOnay) // Kalibrasyon onaylanmamışsa işleme başla - Bu kontrolün nedeni kalibrasyon onaylandıktan sonraki süre boyunca butona tekrar basılmasını engellemek
        {
            kalibrasyonOnay = true; // Kalibrasyon onaylandı
            PlayerPrefs.SetFloat("KalibrasyonX", Input.acceleration.x);// Kalibrasyon bilgisi çek
            KalibrasyonX = PlayerPrefs.GetFloat("KalibrasyonX"); // Kalibrasyon bilgisi kaydet
            StartCoroutine(bildirimGoster("SET_CALIBRATION_INFO")); // Onaylandı bildirimi
        }
    }

    public void KalibrasyonSifirla()
    {
        if (!kalibrasyonOnay) // Kalibrasyon onaylanmamışsa işleme başla - Bu kontrolün nedeni kalibrasyon onaylandıktan sonraki süre boyunca butona tekrar basılmasını engellemek
        {
            kalibrasyonOnay = true; // Kalibrasyon onaylandı
            PlayerPrefs.SetFloat("KalibrasyonX", 0);
            KalibrasyonX = PlayerPrefs.GetFloat("KalibrasyonX");
            StartCoroutine(bildirimGoster("RESET_CALIBRATION_INFO"));
        }
    }


    IEnumerator bildirimGoster(string icerik ,float waitTime = 3f)
    {
        bildirim.text = icerik; // Bildirimi label a yaz
        LanguageManager.reloadTranslations(); // Çevirileri sıfırla - Eğer sıfırlamayı yapmazsak bildirim içeriği doğru çalışmıyor
        bildirim.gameObject.SetActive(true);

        yield return new WaitForSeconds(waitTime);
        bildirim.gameObject.SetActive(false);
        anaMenu();
    }

    public void anaMenu()
    {
        SceneManager.LoadScene(0);
    }

}
