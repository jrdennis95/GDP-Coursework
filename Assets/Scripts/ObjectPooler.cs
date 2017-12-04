using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

    public static ObjectPooler SharedInstance;
    public List<GameObject> pooledObj;
    public GameObject objectToPool;
    public int poolAmount;

	// Use this for initialization
	void Start () {
        pooledObj = new List<GameObject>();
        for (int i = 0; i < poolAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObj.Add(obj);
        }

    }

    public GameObject GetObj()
    {
        for (int i = 0; i < pooledObj.Count; i++)
        {
            if (!pooledObj[i].activeInHierarchy)
            {
                return pooledObj[i];
            }
        } 
        return null;
    }

    // Update is called once per frame
    void Update () {
		
	}

    void Awake()
    {
        SharedInstance = this;
    }
}
