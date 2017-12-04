using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseLife : MonoBehaviour {

    public GameObject[] hearts;
    private Transform canvas;
    private List<GameObject> activeHearts;
    private Vector3 offset;
    // Use this for initialization
    void Start () {
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
        activeHearts = new List<GameObject>();
        GameObject go;
        offset = new Vector3(105, 700, 0);
        for (int i = 0; i < 3; i++)
        {
            go = Instantiate(hearts[i]) as GameObject;
            go.transform.SetParent(canvas);
            go.transform.position = go.transform.position + offset;
            go.transform.localScale += new Vector3(-0.5f, -0.5f, 0);
            activeHearts.Add(go);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Jump"))
        {
            Destroy(activeHearts[0]);
            activeHearts.RemoveAt(0);
        }
	}
}
