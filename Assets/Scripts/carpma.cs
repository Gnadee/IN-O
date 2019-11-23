using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carpma : MonoBehaviour {

    //public GameObject Level;

    public bool devMode = false;

    public float yokolusSure = 2;

    bool _triggered;

    // Use this for initialization
    void Start () {
        //Level = GameObject.Find("Level");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        
        if (Time.timeSinceLevelLoad > 2 && !devMode)
        {
            Level.Instance.GameOver();
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (_triggered)
        {
            
            return;
        }
        _triggered = true;
        //Debug.Log("entered");
    }

    void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player" && _triggered)
        {
            if (Time.timeSinceLevelLoad > 2)
            {
                Level.Instance.skor += 1;
            }
            //Level.GetComponent<Level>().spawnModel(0);
            Level.Instance.spawnModel();
            if (Level.Instance.zorluk < Level.Instance.maxZorluk)
            {
                Level.Instance.zorluk += 0.015f;
            }
            StartCoroutine(yokolus());
            //Debug.Log(other.isTrigger);
        }
        _triggered = false;
        //Debug.Log("exit");
    }

    IEnumerator yokolus()
    {
        yield return new WaitForSeconds(yokolusSure);
        if (!Level.Instance.dur)
        {
            switch (gameObject.name)
            {
                case "SilindirModel1":
                    Level.Instance.Model0.Push(gameObject);
                    gameObject.SetActive(false);
                    break;

                case "SilindirModel2":
                    Level.Instance.Model1.Push(gameObject);
                    gameObject.SetActive(false);
                    break;

                case "SilindirModel3":
                    Level.Instance.Model2.Push(gameObject);
                    gameObject.SetActive(false);
                    break;

                case "SilindirModel4":
                    Level.Instance.Model3.Push(gameObject);
                    gameObject.SetActive(false);
                    break;
            }
        }
    }
}
