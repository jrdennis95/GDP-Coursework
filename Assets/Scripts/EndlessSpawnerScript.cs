using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessSpawnerScript : MonoBehaviour {

    public GameObject[] prefabs;
    private Transform player;
    private float spawnLocation = -10.5f;
    private float spawnLength = 10.5f;
    private int maxSpawns = 8;
    private List<GameObject> activeSpawn;
    private System.Random rnd;
    private int lastPrefab = 0;
    private int randomNo;
    private Vector3 copyposition;
    private BrainGenerator bg;
    // Use this for initialization

    private void Awake()
    {
        activeSpawn = new List<GameObject>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
    }
    void Start () {

    }

    // Update is called once per frame
    void Update () {
		if(player.position.z - 20.0f > (spawnLocation - maxSpawns * spawnLength))
        {
            activeSpawn[1].tag = "Behind";
            activeSpawn[5].tag = "Untagged";
            activeSpawn[6].tag = "Ground";
            Spawn();
            Delete();
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
        activeSpawn.Add(go);
    }

    void Delete()
    {
        Destroy(activeSpawn[0]);
        activeSpawn.RemoveAt(0);
    }

    int RandomNumberGenerator()
    {
        int randomNo = lastPrefab;
        while (randomNo == lastPrefab)
        {
            randomNo = rnd.Next(0, prefabs.Length);
        }

        lastPrefab = randomNo;
        return randomNo;
    }
}
