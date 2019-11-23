using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour {

    public Camera mainCamera;

    public GameObject[] SilindirPrefabs;
    public GameObject Halka;


    public float OyunHizi = 2.0f;
    public float Hiz;
    public float HalkaHiz = 40f;

    public float zorluk = 1f;
    public float maxZorluk = 5f;

    bool gameOver = false;
    bool kontrol = false;

    int tekrar = 0;

    float duration = 60f;
    static float t = 0.0f;

    public GameObject baslangicModel;
    public GameObject anlikModel;

    public bool dur = false;
    public int skor;

    GameObject skorUI;
    public GameObject skorGameoverUI;
    public GameObject OyunIciUI;
    public GameObject GameOverUI;
    public GameObject DuraklamaUI;
    //
    GameObject basGeri_say;
    float geri_sayim = 3;
    bool geriSayimDurum = true;

    float KalibrasyonX = 0;

    /* Debug */
    public Text ProxXUI;
    public Text ProxYUI;
    public float proxX;
    public float proxY;


    /*  Sky Boxes */
    public Material MorningToNoon;
    public Material NoonToAfterNoon;
    public Material AfterNoonToBrightMorning;
    public Material BrightMorningToSunset;
    public Material SunsetToHaloSky;
    public Material HaloskyToCloudy;
    public Material CloudyToEarlyDusk;
    public Material EarlyDuskToMidnight;
    public Material MidnightToNight;
    public Material NightToMorning;

    float a;


    private Stack<GameObject> model0 = new Stack<GameObject>();

    public Stack<GameObject> Model0
    {
        get { return model0; }
        set { model0 = value; }
    }

    private Stack<GameObject> model1 = new Stack<GameObject>();

    public Stack<GameObject> Model1
    {
        get { return model1; }
        set { model1 = value; }
    }

    private Stack<GameObject> model2 = new Stack<GameObject>();

    public Stack<GameObject> Model2
    {
        get { return model2; }
        set { model2 = value; }
    }

    private Stack<GameObject> model3 = new Stack<GameObject>();

    public Stack<GameObject> Model3
    {
        get { return model3; }
        set { model3 = value; }
    }

    private static Level instance;

    public static Level Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<Level>();
            }
            return instance;
        }
    }

    // Use this for initialization
    void Start () {
        //dur = false; // Oyunu Başlat
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        RenderSettings.skybox = MorningToNoon;
        t = 0f;
        //Karisik.SetFloat("_Blend", 1f);
        
        basGeri_say = GameObject.Find("basGeri_say");
        


        KalibrasyonX = PlayerPrefs.GetFloat("KalibrasyonX");
        
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            dur = true;
        }
        
        Time.timeScale = 1;
        SilindirPrefabs = Resources.LoadAll<GameObject>("Modeller");
        createModel(20);
        anlikModel = baslangicModel;

        for (int i = 0; i <10; i++)
        {
            spawnModel();
        }
    }
    
    // Update is called once per frame
    void Update () {
        // DEBUG
        proxX = Input.acceleration.x;
        ProxXUI.text = proxX.ToString();
        // DEBUG
        
        if(geri_sayim > 0)
        {
            geri_sayim = geri_sayim - Time.deltaTime; // Geri sayım başlıyor
            basGeri_say.GetComponent<Text>().text = geri_sayim.ToString("0"); // Sayaç UI da gösteriliyor
        }
        else if(geriSayimDurum) // Geri sayım indikatörünü kontrol et, Sayaç bitti geri sayım hala aktif, son işlemleri gerçekleştir ve geri sayımı bitir
        {
            basGeri_say.SetActive(false); // Geri sayım sayacı yok oluyor
            dur = false; // Geri sayım bitince oyun başlıyor
            geriSayimDurum = false; // Geri sayım bitti indikatörü
        }
        
        if (Time.timeSinceLevelLoad > 1f)
        {
            kontrol = true;
        }
        if (!dur)
        {
            
            //float lerp = Mathf.PingPong(Time.time, duration) / duration;
            float lerp = Mathf.Lerp(0, duration, t) / duration;
            
            t += Time.deltaTime/ duration;
            //Debug.Log("Lerp :: " + lerp);

            if (t > 1.0f)
            {
                tekrar += 1;
                lerp = 0;
                t = 0.0f;
            }
            if (tekrar < 1)
            {
                RenderSettings.skybox = MorningToNoon;
                MorningToNoon.SetFloat("_Blend", lerp);
                Debug.Log("Hava :: MorningToNoon");

            }
            else if (tekrar >= 1 && tekrar < 2)
            {
                RenderSettings.skybox = NoonToAfterNoon;
                NoonToAfterNoon.SetFloat("_Blend", lerp);
                Debug.Log("Hava :: NoonToAfterNoon");
            }
            else if (tekrar >= 2 && tekrar < 3)
            {
                RenderSettings.skybox = AfterNoonToBrightMorning;
                AfterNoonToBrightMorning.SetFloat("_Blend", lerp);
                Debug.Log("Hava :: AfterNoonToBrightMorning");
            }
            else if (tekrar >= 3 && tekrar < 4)
            {
                RenderSettings.skybox = BrightMorningToSunset;
                BrightMorningToSunset.SetFloat("_Blend", lerp);
                Debug.Log("Hava :: BrightMorningToSunset");
            }
            else if (tekrar >= 4 && tekrar < 5)
            {
                RenderSettings.skybox = SunsetToHaloSky;
                SunsetToHaloSky.SetFloat("_Blend", lerp);
                Debug.Log("Hava :: SunsetToHaloSky");
            }
            else if (tekrar >= 5 && tekrar < 6)
            {
                RenderSettings.skybox = HaloskyToCloudy;
                HaloskyToCloudy.SetFloat("_Blend", lerp);
                Debug.Log("Hava :: HaloskyToCloudy");
            }
            else if (tekrar >= 6 && tekrar < 7)
            {
                RenderSettings.skybox = CloudyToEarlyDusk;
                CloudyToEarlyDusk.SetFloat("_Blend", lerp);
                Debug.Log("Hava :: CloudyToEarlyDusk");
            }
            else if (tekrar >= 7 && tekrar < 8)
            {
                RenderSettings.skybox = EarlyDuskToMidnight;
                EarlyDuskToMidnight.SetFloat("_Blend", lerp);
                Debug.Log("Hava :: EarlyDuskToMidnight");
            }
            else if (tekrar >= 8 && tekrar < 9)
            {
                RenderSettings.skybox = MidnightToNight;
                MidnightToNight.SetFloat("_Blend", lerp);
                Debug.Log("Hava :: MidnightToNight");
                
            }
            else if (tekrar >= 9 && tekrar < 10)
            {
                RenderSettings.skybox = NightToMorning;
                NightToMorning.SetFloat("_Blend", lerp);
                Debug.Log("Hava :: NightToMorning");
                tekrar = 0;
            }
            Debug.Log("tekrar :: " + tekrar + " :: lerp :: " + lerp + ":: t ::" + t );
        }

        if (Time.timeSinceLevelLoad > 5)
        {
            //Debug.Log(Time.timeSinceLevelLoad);
            
        }
        Hiz = OyunHizi + zorluk;
        if (!dur)
        {
            skorUI = GameObject.Find("skorUI");
            skorUI.GetComponent<Text>().text = skor.ToString();
        }

        if(Input.GetKeyDown("escape") && !gameOver)
        {
            duraklatDevam();
        }
    }
    private void FixedUpdate()
    {
        if(!dur)
        {
            mainCamera.transform.position += Vector3.up * Time.deltaTime * Hiz;
            Halka.transform.position += Vector3.up * Time.deltaTime * Hiz;
            if (kontrol)
            {
                float translation = ((Input.acceleration.x - KalibrasyonX) * 6)/* + (Input.GetAxis("Horizontal") + Input.GetAxis("Mouse X")) * 3f*/;
                translation *= Time.deltaTime;
                Halka.transform.Translate(translation, 0, 0);
                
            }
            float offset = Time.deltaTime * Hiz;
            Halka.GetComponent<Renderer>().material.mainTextureOffset += new Vector2(-offset, 0);
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        gameOver = true;
        //Time.timeScale = 0;
        skorGameoverUI.GetComponent<Text>().text = skor.ToString();
        if(PlayerPrefs.GetInt("rekorSkor") < skor)
        {
            PlayerPrefs.SetInt("rekorSkor",skor);
        }
        GameOverMenu();
    }
    public void createModel(int amount)
    {
        for (int i =0; i<amount; i++)
        {
            model0.Push(Instantiate(SilindirPrefabs[0]));
            model1.Push(Instantiate(SilindirPrefabs[1]));
            model2.Push(Instantiate(SilindirPrefabs[2]));
            model3.Push(Instantiate(SilindirPrefabs[3]));

            model0.Peek().name = "SilindirModel1";
            model0.Peek().SetActive(false);
            model1.Peek().name = "SilindirModel2";
            model1.Peek().SetActive(false);
            model2.Peek().name = "SilindirModel3";
            model2.Peek().SetActive(false);

            model3.Peek().name = "SilindirModel4";
            model3.Peek().SetActive(false);

        }
    }
    public void spawnModel()
    {
        if(model0.Count ==0 || model1.Count == 0 || model2.Count == 0 || model3.Count == 0)
        {
            createModel(10);
        }

        int randomIndex = Random.Range(0,SilindirPrefabs.Length);

        if(randomIndex == 3) // 3. Model gelme şansı azaltıldı.
        {
            randomIndex = Random.Range(0, SilindirPrefabs.Length);
        }

        if(randomIndex ==0)
        {
            GameObject tmp = model0.Pop();
            tmp.SetActive(true);
            tmp.transform.position = anlikModel.transform.GetChild(0).position + SilindirPrefabs[randomIndex].transform.GetChild(0).position;
            anlikModel = tmp;
        }
        else if(randomIndex == 1)
        {
            GameObject tmp = model1.Pop();
            tmp.SetActive(true);
            tmp.transform.position = anlikModel.transform.GetChild(0).position + SilindirPrefabs[randomIndex].transform.GetChild(0).position;
            anlikModel = tmp;
        }
        else if (randomIndex == 2)
        {
            GameObject tmp = model2.Pop();
            tmp.SetActive(true);
            tmp.transform.position = anlikModel.transform.GetChild(0).position + SilindirPrefabs[randomIndex].transform.GetChild(0).position;
            anlikModel = tmp;
        }
        else if (randomIndex == 3)
        {
            GameObject tmp = model3.Pop();
            tmp.SetActive(true);
            tmp.transform.position = anlikModel.transform.GetChild(0).position + SilindirPrefabs[randomIndex].transform.GetChild(0).position;
            anlikModel = tmp;
        }

        //anlikModel = (GameObject)Instantiate(SilindirPrefabs[modelNo], anlikModel.transform.GetChild(0).transform.GetChild(0).position + SilindirPrefabs[modelNo].transform.GetChild(0).transform.GetChild(0).position, SilindirPrefabs[modelNo].transform.rotation);
    }

    public void anaMenu()
    {
        SceneManager.LoadScene(0);
        /*
        MenuUI.SetActive(true);
        GameOverUI.SetActive(false);
        OyunIciUI.SetActive(false);
        dur = true;
        */
    }

    public void GameOverMenu()
    {
        GameOverUI.SetActive(true);
        OyunIciUI.SetActive(false);
        dur = true;
    }

    public void tekrarOyna()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void cikis()
    {
        Application.Quit();
    }

    public void duraklatDevam()
    {
        if(!geriSayimDurum)
        { 
            if(dur)
            {
                dur = !dur;
                DuraklamaUI.SetActive(false);
            }
            else if(!dur)
            {
                dur = !dur;
                DuraklamaUI.SetActive(true);
            }
        }
    }


}
