using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAI : MonoBehaviour {

    private Vector3 movement;
    private GameStart gs;
    private CharacterController control;
    public GameObject human;
    private GameObject humanobj;
    private EndlessSpawnerScript ess;
    private MovementScript ms;
    private HumanCollisionControl hcc;
    private List<GameObject> active;
    private float runspeed;
    private float jumpSpeed = 0.0f;
    private float strafespeed;
    private bool collided;
    private bool begin = false;
    public void Init(bool started)
    {
        active = new List<GameObject>();
        strafespeed = 3;
        ess = GameObject.Find("EndlessSpawner").GetComponent<EndlessSpawnerScript>();
        ms = GameObject.Find("Player").GetComponent<MovementScript>();

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
