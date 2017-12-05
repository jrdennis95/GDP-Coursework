﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMovement : MonoBehaviour {

    public Transform[] tar;
    private GameStart gs;
    private Vector3 movement;
    private Collider collider;
    //public GameObject originalmob;
    public GameObject mob;
    private Transform copymob;
    private MovementScript ms;
    private List<GameObject> active;
    private float runspeed;
    private float totalspeed;
    private bool begin = false;

    public void Init(bool started)
    {
        active = new List<GameObject>();
        runspeed = 5.05f;
        ms = GameObject.Find("Player").GetComponent<MovementScript>();
        GameObject go;
        go = Instantiate(mob) as GameObject;
        go.transform.SetParent(transform);
        go.transform.tag = "Mob";
        collider = go.transform.GetComponent<Collider>();
        active.Add(go);
        begin = true;
    }

    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (begin)
        {
            totalspeed = runspeed;
            movement.x = 0;
            movement.y = 0;
            movement.z = totalspeed;
            collider.transform.Translate(movement * Time.deltaTime);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Zombie")
        {
            Debug.Log("KILLED");
        }
    }

    public void GainSpeed(float x)
    {
        totalspeed = totalspeed + (0.05f * x);
    }

    public void EndBegin()
    {
        begin = false;
    }

    public void DeleteMob()
    {
        Destroy(active[0]);
        active.RemoveAt(0);
    }
}