using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAI : MonoBehaviour {

    private Vector3 movement;
    private CharacterController control;
    private Transform tar;
    private EndlessSpawnerScript ess;
    private MovementScript ms;
    private float runspeed;
    private float jumpSpeed = 0.0f;
    public float strafespeed;
    private bool collided;
    // Use this for initialization
    void Start () {
        control = GetComponent<CharacterController>();
        //tar.transform.position = new Vector3(0, 1, 0);
        ess = GameObject.Find("EndlessSpawner").GetComponent<EndlessSpawnerScript>();
        ms = GameObject.Find("Zombie").GetComponent<MovementScript>();
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
        //Collisions
        if (movement.y < - 10 && control.isGrounded != true)
        {
            Destroy(this.gameObject);
            ess.ResetHumanCount();
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
