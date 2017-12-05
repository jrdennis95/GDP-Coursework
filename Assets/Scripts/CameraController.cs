using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float moveSpeed;
    private Transform ms;
    public Vector3 offset;
    private Vector3 newtar;
    private Vector3 startingPosition;
    private Vector3 camerabehind;
    private bool begin = false;

    public void Init(bool started)
    {
        ms = GameObject.FindGameObjectWithTag("Zombie").transform;
        transform.position = camerabehind;
        startingPosition = ms.position + transform.position;
        begin = true;
    }
        // Use this for initialization
        void Start()
    {
        camerabehind = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (begin)
        {
            transform.position = ms.position + camerabehind;
            newtar = ms.position + offset;
            transform.LookAt(newtar);
        }
    }

    public void EndBegin()
    {
        begin = false;
    }
}
