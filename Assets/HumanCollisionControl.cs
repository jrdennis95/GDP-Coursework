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

    // Use this for initialization
    void Start () {
        control = GetComponent<CharacterController>();
        strafespeed = 3;
        ms = GameObject.Find("Player").GetComponent<MovementScript>();
        runspeed = ms.GetTotalSpeed();
    }
	
	// Update is called once per frame
	void Update () {
        if (collided && control.isGrounded)
        {
            jumpSpeed = 5.0f;
        }
        else
        {
            jumpSpeed = movement.y + (Physics.gravity.y * Time.deltaTime);
        }
        movement.x = 0.1f * strafespeed;
        movement.y = jumpSpeed;
        movement.z = runspeed;
        control.Move(movement * Time.deltaTime);
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
