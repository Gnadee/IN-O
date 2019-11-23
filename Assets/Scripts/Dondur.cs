using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dondur : MonoBehaviour {
    public float rotationSpeed = 15f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.right * Time.deltaTime * rotationSpeed);
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
    }
}
