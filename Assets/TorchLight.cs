using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchLight : MonoBehaviour {

    private List<GameObject> active;
    public GameObject torch;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Init(bool started)
    {
        active = new List<GameObject>();
        GameObject go;
        go = Instantiate(torch) as GameObject;
        if (transform.gameObject.name == "modelTorch")
        {
            go.transform.position = transform.position + new Vector3(-0.04747009f, 1.823304f, -0.108977f);
        } else
        {
            go.transform.position = transform.position + new Vector3(-0.03116465f, 0.5788216f, 0.255291f);
        }
        go.transform.SetParent(transform);
        go.transform.tag = "Torch";
        active.Add(go);
    }

    public void DeleteLight()
    {
        Destroy(active[0]);
        active.RemoveAt(0);
    }
}
