using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessSpawnerScript : MonoBehaviour {

    public GameObject[] prefabs;
    private HumanAI script;
    private GameStart gs;
    public GameObject human;
    private Transform player;
    private Transform torch;
    private Transform bonfire;
    private float spawnLocation = -10.5f;
    private float spawnLength = 10.5f;
    private int maxSpawns = 8;
    private int humanCount;
    private List<GameObject> activeSpawn;
    private List<int> spawnHistory;
    private System.Random rnd;
    private int lastPrefab = 0;
    private int randomNo;
    private Vector3 copyposition;
    private BrainGenerator bg;
    private bool begin = false;
    // Use this for initialization

    public void Init(bool started)
    {
            activeSpawn = new List<GameObject>();
            spawnHistory = new List<int>();
            humanCount = 0;
            spawnLocation = -10.5f;
            player = GameObject.FindGameObjectWithTag("Zombie").transform;
            rnd = new System.Random();
            for (int i = 0; i < maxSpawns; i++)
            {
                if (i < 1)
                {
                    Spawn(0);
                }
                else
                {
                    Spawn();
                }
            }
            activeSpawn[0].tag = "Behind";
            activeSpawn[5].tag = "Ground";
            activeSpawn[6].tag = "Untagged";
            bg = FindObjectOfType<BrainGenerator>();
        begin = true;
    }
        private void Awake()
        {
            gs = GameObject.FindGameObjectWithTag("Menu").GetComponent<GameStart>();
        }
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        if (begin)
        {
            if (player.position.z - 20.0f > (spawnLocation - maxSpawns * spawnLength))
            {
                activeSpawn[1].tag = "Behind";
                activeSpawn[5].tag = "Untagged";
                activeSpawn[6].tag = "Ground";
                Spawn();
                Delete();
            }
        }
	}

    void Spawn(int foo = -1)
    {
        GameObject go;
        if (foo == -1)
        {
            go = Instantiate(prefabs[RandomNumberGenerator()]) as GameObject;
        } else
        {
            go = Instantiate(prefabs[foo]) as GameObject;
        }
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnLocation;
        copyposition = go.transform.position;
        //bg.spawnBrains(new Vector3(copyposition.x, copyposition.y + 2, copyposition.z + 10));
        spawnLocation += spawnLength;
        torch = go.GetComponentInChildren<Transform>().Find("modelTorch");
        bonfire = go.GetComponentInChildren<Transform>().Find("modelBonfire");
        if (torch != null)
        {
            torch.GetComponent<TorchLight>().Init(true);
        } else if(bonfire != null)
        {
            bonfire.GetComponent<TorchLight>().Init(true);
        }
        activeSpawn.Add(go);
        if(humanCount == 0 && activeSpawn.Count > 5)
        {
            humanCount = 1;
        }
    }

    void Delete()
    {
        Destroy(activeSpawn[0]);
        activeSpawn.RemoveAt(0);
    }

    public void DeleteSpawner()
    {
        for(int i = 0; i < maxSpawns; i++)
        {
            Destroy(activeSpawn[0]);
            activeSpawn.RemoveAt(0);
        }
    }

        int RandomNumberGenerator()
    {
        int randomNo = lastPrefab;
        while (randomNo == lastPrefab)
        {
            randomNo = rnd.Next(0, prefabs.Length);
        }

        lastPrefab = randomNo;
        spawnHistory.Add(randomNo);
        return randomNo;

    }

public List<int> getSpawnHistory()
    {
        return spawnHistory;
    }

public void ResetHumanCount()
    {
        humanCount = 0;
    }

public int GetHumanCount()
    {
        return humanCount;
    }

public Vector3 spawnPosition()
    {
        return activeSpawn[4].transform.position;
    }

    public void EndBegin()
    {
        begin = false;
    }
}
