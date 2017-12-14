using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAI : MonoBehaviour {

    public GameObject human;
    private EndlessSpawnerScript ess;
    private List<GameObject> active;
    private bool begin = false;
    public void Init(bool started)
    {
        active = new List<GameObject>();
        ess = GameObject.Find("EndlessSpawner").GetComponent<EndlessSpawnerScript>();

        GameObject go;
        go = Instantiate(human) as GameObject;
        go.transform.position = ess.spawnPosition() + new Vector3(7, 0, 0);
        go.transform.tag = "Human";
        go.transform.SetParent(transform);
        active.Add(go);

        begin = true;
    }

        void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (begin) {
            if (active[0].transform.position.y < -5)
            {
                DeleteHuman();
                NewHuman();
            }
        }
    }


    public void NewHuman()
    {
        GameObject go;
        go = Instantiate(human) as GameObject;
        go.transform.position = ess.spawnPosition() + new Vector3(7, 0, 0);
        go.transform.tag = "Human";
        go.transform.SetParent(transform);
        active.Add(go);
    }

    public void DeleteHuman()
    {
        Destroy(active[0]);
        active.RemoveAt(0);
    }

    public void EndBegin()
    {
        begin = false;
    }

    public void StartBegin()
    {
        begin = true;
     }
}
