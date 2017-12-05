﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementScript : MonoBehaviour {

    private GameStart gs;
    private CharacterController control;
    private EndlessSpawnerScript ess;
    private MobMovement mob;
    private BoxCollider bc;
    public Text UItext;
    private Vector3 movement;
    public GameObject[] hearts;
    private Transform canvas;
    private Image panel;
    private List<GameObject> activeHearts;
    private Vector3 offset;
    private Animator anim;
    private bool dead, deathanimover;
    private bool started;
    public Image GameOverImage1, GameOverImage2;
    private float gamma;
    private float distancebetween;
    public float strafespeed;
    public float runspeed;
    private float totalspeed;
    private int score;
    private int heartcount;
    private float jumpSpeed = 0.0f;
    private float gravity = 9.8f;
    private float scale;
    private float timer1, timer2, timer3 = 0;
    private bool begin = false;

    public void Init(bool started)
    {
        control = GetComponent<CharacterController>();
        ess = GameObject.Find("EndlessSpawner").GetComponent<EndlessSpawnerScript>();
        mob = GameObject.Find("Mob").GetComponent<MobMovement>();
        //GameOverImage = GameObject.Find("GameOverImage").GetComponent<GameObject>();
        score = 0;
        heartcount = 3;
        gamma = 0;
        scale = 118f;
        dead = false;
        deathanimover = false;
        totalspeed = runspeed;
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
        panel = GameObject.FindGameObjectWithTag("Panel").GetComponent<Image>();
        activeHearts = new List<GameObject>();
        anim = GetComponent<Animator>();
        GameObject go;
        for (int i = 0; i < 3; i++)
        {
            go = Instantiate(hearts[i]) as GameObject;
            go.transform.position = new Vector3(153 + (i * 90), -57, 0);
            go.transform.SetParent(canvas, false);
            activeHearts.Add(go);
        }
        begin = true;
    }

        void Start () {
            gs = GameObject.FindGameObjectWithTag("Menu").GetComponent<GameStart>();
        }

    // Update is called once per frame
    void Update () {
        if (begin)
        {
            //Jumping
            if (dead == false)
            {
                distancebetween = transform.position.z - mob.transform.position.z;
                totalspeed = runspeed + (score * 0.01f);
                if (Input.GetButtonDown("Jump") && control.isGrounded)
                {
                    jumpSpeed = 5.0f;
                }
                else
                {
                    jumpSpeed = movement.y + (Physics.gravity.y * Time.deltaTime);
                }
                movement.x = Input.GetAxisRaw("Horizontal") * strafespeed;
                movement.y = jumpSpeed;
                movement.z = totalspeed;
                control.Move(movement * Time.deltaTime);
            }
            else
            {
                timer1 += Time.deltaTime;
                if (timer1 > 2 && scale > 15)
                {
                    timer2 += Time.deltaTime;
                    scale = scale - timer2 * 2;
                    GameOverImage1.rectTransform.localScale = new Vector3(1, 1, 1) * scale;
                }
                else if (scale < 15)
                {
                    if (GameOverImage2.fillAmount < 1)
                    {
                        timer3 += Time.deltaTime;
                        GameOverImage2.fillAmount += timer3;
                    }
                    if (GameOverImage2.fillAmount >= 1)
                    {
                        deathanimover = true;
                    }
                }
            }
            //UI
            panel.color = new Color(1, 0, 0, gamma);
            if (distancebetween > -1.5f)
            {
                gamma = 0;
            }
            else
            {
                gamma = (distancebetween * -1) / 1.5f - 0.8f;
            }
            UItext.text = score.ToString("D3");
            //Collisions
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Brain")
        {
            hit.gameObject.SetActive(false);
            score++;
            if (score % 10 == 0)
            {
                mob.GainSpeed(1);
                if (heartcount == 0)
                {
                    heartcount++;
                    activeHearts[0].gameObject.SetActive(true);
                    anim.SetBool("damaged", false);
                }
                else if (heartcount == 1)
                {
                    heartcount++;
                    activeHearts[0].gameObject.SetActive(true);
                    activeHearts[1].gameObject.SetActive(true);
                }
                else if (heartcount == 2)
                {
                    heartcount++;
                    activeHearts[0].gameObject.SetActive(true);
                    activeHearts[1].gameObject.SetActive(true);
                    activeHearts[2].gameObject.SetActive(true);
                }

            } else if (score % 20 == 0)
            {
                mob.GainSpeed(2);
            }
        } else if (hit.gameObject.tag == "Human")
        {
            Destroy(hit.gameObject);
            ess.ResetHumanCount();
            score = score + 50;
            mob.GainSpeed(5);
        } else if (hit.gameObject.tag == "Mob")
        {
            Destroy(this.gameObject);
        }
            else if (hit.point.z > transform.position.z + control.radius && hit.gameObject.tag == "Obstacle")
        {
            hit.gameObject.SetActive(false);
            if (score > 0)
            {
                score = score - 20;
            }
            if (heartcount == 3) {
                activeHearts[2].gameObject.SetActive(false);
            } else if( heartcount == 2) {
                anim.SetBool("damaged", false);
                activeHearts[2].gameObject.SetActive(false);
                activeHearts[1].gameObject.SetActive(false);
            } else if (heartcount == 1)
            {
                anim.SetBool("damaged", true);
                activeHearts[2].gameObject.SetActive(false);
                activeHearts[1].gameObject.SetActive(false);
                activeHearts[0].gameObject.SetActive(false);
            } else if (heartcount == 0)
            {
                mob.StopSpeed();
                anim.SetBool("dead", true);
                dead = true;
            }
            heartcount--;
            //HeartDisplay.RemoveHeart();
            //hearts--;
        }
    }

    public bool HasDied()
    {
        return deathanimover;
    }

    public float GetTotalSpeed()
    {
        return totalspeed;
    }

    public int GetScore()
    {
        return score;
    }
}
