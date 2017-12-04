using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainSpinner : MonoBehaviour {

    private float magnitude;
    private float time;

	// Use this for initialization
	void Start () {
        transform.Rotate(0, 90, 0);
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime*2;
        magnitude = Mathf.Sin(time + Time.deltaTime);
       transform.Rotate(0, magnitude, 0);
	}
}
