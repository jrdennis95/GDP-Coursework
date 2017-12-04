using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementScript : MonoBehaviour {

    private CharacterController control;
    private BoxCollider bc;
    private Text UItext;
    private Vector3 movement;
    public float strafespeed;
    public float runspeed;
    private int score;
    private float jumpSpeed = 0.0f;
    private float gravity = 9.8f;
    // Use this for initialization
    void Start () {
        control = GetComponent<CharacterController>();
        score = 0;
    }
	
	// Update is called once per frame
	void Update () {
        //Jumping
        if (Input.GetButtonDown("Jump") && control.isGrounded)
        {
            jumpSpeed = 5.0f;
        } else
        {
            jumpSpeed = movement.y + (Physics.gravity.y * Time.deltaTime);
        }
        movement.x = Input.GetAxisRaw("Horizontal") * strafespeed;
        movement.y = jumpSpeed;
        movement.z = runspeed;
        control.Move(movement * Time.deltaTime);

        //Collisions

	}

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.point.z > transform.position.z + control.radius && hit.gameObject.tag == "Brain")
        {
            Debug.Log("Hit");
            //hit.gameObject.SetActive(false);
        }
        // if (other.gameObject.tag == "Brain")
        //  {
        //other.gameObject.SetActive(false);
        //  }
    }
}
