using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMovement : MonoBehaviour {

    public Transform[] tar;
    private Vector3 movement;
    private MovementScript ms;
    private Collider collider;
    private float runspeed;
    private float additionalspeed;
    private float totalspeed;

    // Use this for initialization
    void Start () {
        runspeed = 5.05f;
        ms = GameObject.Find("Zombie").GetComponent<MovementScript>();
        collider = GetComponent<Collider>();
    }
	
	// Update is called once per frame
	void Update () {
        totalspeed = runspeed;
        movement.x = 0;
        movement.y = 0;
        movement.z = totalspeed;
        collider.transform.Translate(movement * Time.deltaTime);
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
