using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanCollisionControl : MonoBehaviour {

    private CharacterController control;
    private Vector3 movement;
    private MovementScript ms;
    private bool collided;
    private float jumpSpeed = 0.0f;
    private float strafespeed;
    private float runspeed;
    private System.Random rnd;
    private int randomNo;
    private float direction;
    private float lockedcounter;

    // Use this for initialization
    void Start () {
        rnd = new System.Random();
        lockedcounter = 0;
        randomNo = 0;
        direction = 0;
        control = GetComponent<CharacterController>();
        strafespeed = 3;
        ms = GameObject.Find("Player").GetComponent<MovementScript>();
        runspeed = ms.GetTotalSpeed();
    }
	
	// Update is called once per frame
	void Update () {
        if (RandomNumberGenerator() == 0 && lockedcounter <= 0)
        {
            direction = -0.1f;
            lockedcounter = 10;
            Debug.Log("left");
        }
        else if (RandomNumberGenerator() == 1 && lockedcounter <= 0)
        {
            direction = 0.1f;
            lockedcounter = 10;
            Debug.Log("right");
        }
        else if (RandomNumberGenerator() == 2 && lockedcounter <= 0)
        {
            direction = 0;
            lockedcounter = 10;
            Debug.Log("middle");
        }

        if (collided && control.isGrounded)
        {
            jumpSpeed = 5.0f;
        }
        else
        {
            jumpSpeed = movement.y + (Physics.gravity.y * Time.deltaTime);
        }
        movement.x = direction * strafespeed;
        movement.y = jumpSpeed;
        movement.z = runspeed;
        control.Move(movement * Time.deltaTime);
        lockedcounter -= (5*Time.deltaTime);
    }

    int RandomNumberGenerator()
    {
        if (lockedcounter < 0)
        {
            randomNo = rnd.Next(0, 3);
            return randomNo;
        } else
        {
            return randomNo;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Brain")
        {
            Physics.IgnoreCollision(control, hit.collider);
        }

        else if (hit.point.z > transform.position.z + control.radius && hit.gameObject.tag == "Obstacle")
        {
            collided = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        collided = false;
    }

}
