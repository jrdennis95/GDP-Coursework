using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMovement : MonoBehaviour {

    public Transform[] tar;
    private GameStart gs;
    private Vector3 movement;
    private MovementScript ms;
    private Collider collider;
    private float runspeed;
    private float totalspeed;
    private bool begin = false;

    public void Init(bool started)
    {
        runspeed = 5.05f;
        ms = GameObject.Find("Zombie").GetComponent<MovementScript>();
        collider = GetComponent<Collider>();
        begin = true;
    }

    void Start () {
        gs = GameObject.FindGameObjectWithTag("Menu").GetComponent<GameStart>();
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
    public void StopSpeed()
    {
        Destroy(collider.gameObject);
    }
}
