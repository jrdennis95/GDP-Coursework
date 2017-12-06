using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionControl : MonoBehaviour
{

    private CharacterController control;
    private MobMovement mobscript;
    private GameObject mob;
    private MovementScript ms;
    private EndlessSpawnerScript ess;
    private HumanAI ha;
    private GameStart gs;
    private Animator anim;
    private bool begin = false;

    public void Init(bool started)
    {
        ms = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementScript>();
        mobscript = GameObject.Find("Mobs").GetComponent<MobMovement>();
        ess = GameObject.Find("EndlessSpawner").GetComponent<EndlessSpawnerScript>();
        gs = GameObject.FindGameObjectWithTag("Canvas").GetComponent<GameStart>();
        ha = GameObject.Find("Humans").GetComponent<HumanAI>();
        control = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        begin = true;
    }
        // Use this for initialization
        void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (begin)
        {

        }
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "Brain")
        {
            hit.gameObject.SetActive(false);
            ms.addScore(1);
            if (ms.GetDistanceBetween() < 3)
            {
                ms.AddSpeed(0.5f);
            }
            if (ms.GetScore() % 10 == 0)
            {
                ms.AddSpeed(0.1f);
                mobscript.GainSpeed(1);
                if (ms.GetHearts() == 0)
                {
                    ms.AddHeart();
                    ms.SetHeartActive(0, true);
                    anim.SetBool("damaged", false);
                }
                else if (ms.GetHearts() == 1)
                {
                    ms.AddHeart();
                    ms.SetHeartActive(0, true);
                    ms.SetHeartActive(1, true);
                }
                else if (ms.GetHearts() == 2)
                {
                    ms.AddHeart();
                    ms.SetHeartActive(0, true);
                    ms.SetHeartActive(1, true);
                    ms.SetHeartActive(2, true);
                }

            }
            else if (ms.GetScore() % 20 == 0)
            {
                mobscript.GainSpeed(2);
            }
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Human")
        {
            ha.DeleteHuman();
            ha.NewHuman();
            ms.addScore(20);
            mobscript.GainSpeed(2);
        }
        else if (hit.gameObject.tag == "Mob")
        {
            Destroy(mobscript.GetComponent<Collider>());
            anim.SetBool("dead", true);
            ms.SetDead(true);
            gs.ControlDeath(true);
        }
        else if (hit.point.z > transform.position.z + control.radius && hit.gameObject.tag == "Obstacle")
        {
            hit.gameObject.SetActive(false);
            if (ms.GetDistanceBetween() < 5)
            {
                ms.SubtractSpeed(0.01f);
            } else
            {
                ms.SubtractSpeed(0.5f);
            }
            if (ms.GetScore() > 0)
            {
                ms.subtractScore(10);
            } else if(ms.GetScore() > 100)
            {
                ms.subtractScore(20);
            }
            if (ms.GetHearts() == 3)
            {
                ms.SetHeartActive(2, false);
            }
            else if (ms.GetHearts() == 2)
            {
                anim.SetBool("damaged", false);
                ms.SetHeartActive(2, false);
                ms.SetHeartActive(1, false);
            }
            else if (ms.GetHearts() == 1)
            {
                anim.SetBool("damaged", true);
                ms.SetHeartActive(2, false);
                ms.SetHeartActive(1, false);
                ms.SetHeartActive(0, false);
            }
            else if (ms.GetHearts() == 0)
            {
                anim.SetBool("dead", true);
                ms.SetDead(true);
                gs.ControlDeath(true);
            }
            ms.RemoveHeart();
        }
    }
}
