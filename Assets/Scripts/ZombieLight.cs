using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieLight : MonoBehaviour {

    public Transform tar;
    public Vector3 offset;
    private Vector3 newtar;
    public Vector3 startingPosition;

    // Use this for initialization
    void Start()
    {
        startingPosition = tar.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = tar.position - startingPosition;
        newtar = tar.position + offset;
        transform.LookAt(newtar);
    }
}
