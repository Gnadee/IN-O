using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuSistem : MonoBehaviour {
    public int rekorSkor;
    public Text rekorSkorUI;

    public Material SkyNight;

    public float donusSuresi = 90f;
    static float timerx = 0.0f;

    public GameObject anaMenuUI;
    public GameObject ayarlarUI;

    public Button trUI;
    public Button enUI;
    public Button frUI;
    public Button deUI;

    public Button soundOn;
    public Button soundOff;

    public Slider soundSliderUI;

    public AudioMixer masterMixer;

    float masterVolume = 0;



    // Use this for initialization
    void Start () {
        Time.timeScale = 1;
        RenderSettings.skybox = SkyNight;
		if(PlayerPrefs.HasKey("rekorSkor"))
        {
            rekorSkor = PlayerPrefs.GetInt("rekorSkor");
            rekorSkorUI.text = rekorSkor.ToString();
        }
        Debug.Log(Application.systemLanguage.ToString());

        if(PlayerPrefs.HasKey("MasterVolume"))
        {
            masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        }

        SesAyarla(masterVolume);
        soundSliderUI.value = masterVolume;
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("escape") && anaMenuUI.gameObject.activeSelf)
        {
            Cikis();
        }
        else if(Input.GetKeyDown("escape") && ayarlarUI.gameObject.activeSelf)
        {
            anaMenu();
        }
        float lerp = Mathf.Lerp(0, 360, timerx);
        timerx += Time.deltaTime/ donusSuresi;


        
        if (timerx > 1f)
        {
            lerp = 0;
            timerx = 0.0f;
        }
        
        //Debug.Log("Lerp :: " + lerp + " :: t :: " + timerx);
        SkyNight.SetFloat("_Rotation", lerp);
        //RenderSettings.skybox.SetFloat("_Rotation", Time.time); //To set the speed, just multiply the Time.time with whatever amount you want.
    }

    public void Oyna()
    {
        SceneManager.LoadScene(1);
    }

    public void Cikis()
    {
        Application.Quit();
    }


    public void kalibrasyon()
    {
        SceneManager.LoadScene(2);
    }

    public void anaMenu()
    {
        ayarlarUI.SetActive(false);
        anaMenuUI.SetActive(true);
        dilDegistir(PlayerPrefs.GetString("Language"));
    }
    
    public void ayarlar()
    {
        anaMenuUI.SetActive(false);
        ayarlarUI.SetActive(true);
    }


    public void dilDegistir(string dil)
    {
        PlayerPrefs.SetString("Language", dil);
        LanguageManager.playerLanguage = LanguageManager.stringToSystemLanguage(dil);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SesAyarla(float soundLevel)
    {
        masterMixer.SetFloat("volume", soundLevel);
        PlayerPrefs.SetFloat("MasterVolume", soundLevel);
        masterVolume = soundLevel;
        
        if (masterVolume == -80)
        {
            soundOff.gameObject.SetActive(true);
            soundOn.gameObject.SetActive(false);
        }
        else if (masterVolume > -80)
        {
            soundOff.gameObject.SetActive(false);
            soundOn.gameObject.SetActive(true);
        }
        soundSliderUI.value = masterVolume;
        Debug.Log("masterVolume :: " + masterVolume);
    }
}
