using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainGenerator : MonoBehaviour
{

    public GameObject[] prefabs;
    private GameStart gs;
    private Transform ground;
    private Transform groundBehind;
    private Transform player;
    private float spawnLocation = -10.5f;
    private float spawnLength = 2.0f;
    private int maxSpawns = 3;
    private List<GameObject> activeSpawn;
    private GameObject[] brainList;
    private int brainNo;
    private int brainsActive;
    private System.Random rnd;
    private int lastLane = 0; //left = 0, middle = 1, right = 2
    private int randomNo;
    private Vector3 copyposition;
    private float lastposition;
    private bool begin = false;

    public void Init(bool started)
    {
        brainList = new GameObject[3];
        brainNo = 3;
        brainsActive = 0;
        activeSpawn = new List<GameObject>();
        lastposition = 0;
        rnd = new System.Random();
        groundBehind = GameObject.FindGameObjectWithTag("Behind").transform;
        begin = true;
    }

    void Start()
    {
        gs = GameObject.FindGameObjectWithTag("Menu").GetComponent<GameStart>();
    }

    // Update is called once per frame
    void Update()
    {
        if (begin)
        {
            ground = GameObject.FindGameObjectWithTag("Ground").transform;
            player = GameObject.FindGameObjectWithTag("Zombie").transform;

            if (lastposition != ground.position.z/*ground.position.z - 20.0f > (spawnLocation - maxSpawns * spawnLength)*/)
            {
                lastposition = ground.position.z;
                Spawn();
                groundBehind = GameObject.FindGameObjectWithTag("Behind").transform;
            }

            if (player.position.z > groundBehind.position.z && activeSpawn.Count > 20)
            {
                for (int i = 0; i < brainNo; i++)
                {
                    activeSpawn[i].tag = "Inactive";
                }
                groundBehind.tag = "Untagged";
                Delete();
            }
        }
    }

    void Spawn(int foo = -1)
    {
        if (RandomNumberGenerator() == 1)
        {
            GameObject go;
            for (int i = 0; i < brainNo; i++)
            {
                go = Instantiate(prefabs[i]) as GameObject;
                go.transform.SetParent(transform);
                go.transform.position = new Vector3(5.75f + (1.75f * RandomNumberGenerator()), 1, ground.position.z + (i * 2));
                go.tag = "Brain";
                spawnLocation += spawnLength;
                activeSpawn.Add(go);
                brainsActive++;
            }
        } else
        {
            GameObject go;
            int lane = RandomNumberGenerator();
            for (int i = 0; i < brainNo; i++)
            {
                go = Instantiate(prefabs[i]) as GameObject;
                go.transform.SetParent(transform);
                go.transform.position = new Vector3(5.75f + (1.75f * lane), 1, ground.position.z + (i * 2));
                go.tag = "Brain";
                spawnLocation += spawnLength;
                activeSpawn.Add(go);
                brainsActive++;
            }
        }
    }

    void Delete()
    {
        if (activeSpawn.Count != 0)
        {
            for (int i = 0; i < activeSpawn.Count; i++)
            {
                if (activeSpawn[0].tag == "Inactive")
                {
                    Destroy(activeSpawn[0]);
                    activeSpawn.RemoveAt(0);
                }
            }
        }
    }

    public void DeleteBrains()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    int RandomNumberGenerator()
    {
        int randomNo = lastLane;
        while (randomNo == lastLane)
        {
            randomNo = rnd.Next(0, 3);
        }

        lastLane = randomNo;
        return randomNo;
    }
    public void EndBegin()
    {
        begin = false;
    }
}
