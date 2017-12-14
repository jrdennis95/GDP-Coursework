using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionControl : MonoBehaviour
{

    private CharacterController control;
    private MobMovement mobscript;
    private MovementScript ms;
    private HumanAI ha;
    private GameStart gs;
    private Animator anim;
    public AudioClip hitaudio, brainaudio, humandeathaudio, zombiedeathaudio;
    private AudioSource hitsource, brainsource, humandeathsource, zombiedeathsource, mobsource;
    private bool begin = false;

    public void Init(bool started)
    {
        ms = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementScript>();
        mobscript = GameObject.Find("Mobs").GetComponent<MobMovement>();
        gs = GameObject.FindGameObjectWithTag("Canvas").GetComponent<GameStart>();
        ha = GameObject.Find("Humans").GetComponent<HumanAI>();
        hitsource = GameObject.Find("HitSound").GetComponent<AudioSource>();
        brainsource = GameObject.Find("BrainSound").GetComponent<AudioSource>();
        humandeathsource = GameObject.Find("HumanDeathSound").GetComponent<AudioSource>();
        zombiedeathsource = GameObject.Find("ZombieDeathSound").GetComponent<AudioSource>();
        mobsource = GameObject.Find("MobSound").GetComponent<AudioSource>();
        hitsource.clip = hitaudio;
        brainsource.clip = brainaudio;
        humandeathsource.clip = humandeathaudio;
        zombiedeathsource.clip = zombiedeathaudio;
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
            brainsource.Play();
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
            humandeathsource.Play();
            ha.DeleteHuman();
            ha.NewHuman();
            ms.addScore(20);
            mobscript.GainSpeed(2);
        }
        else if (hit.gameObject.tag == "Mob")
        {
            mobsource.Stop();
            zombiedeathsource.Play();
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
                ms.SubtractSpeed(0.7f);
            }
            if (ms.GetScore() > 0)
            {
                ms.subtractScore(5);
            } else if(ms.GetScore() > 100)
            {
                ms.subtractScore(10);
            }
            if (ms.GetHearts() == 3)
            {
                hitsource.Play();
                ms.SetHeartActive(2, false);
            }
            else if (ms.GetHearts() == 2)
            {
                hitsource.Play();
                anim.SetBool("damaged", false);
                ms.SetHeartActive(2, false);
                ms.SetHeartActive(1, false);
            }
            else if (ms.GetHearts() == 1)
            {
                hitsource.Play();
                anim.SetBool("damaged", true);
                ms.SetHeartActive(2, false);
                ms.SetHeartActive(1, false);
                ms.SetHeartActive(0, false);
            }
            else if (ms.GetHearts() == 0)
            {
                mobsource.Stop();
                zombiedeathsource.Play();
                anim.SetBool("dead", true);
                ms.SetDead(true);
                gs.ControlDeath(true);
            }
            ms.RemoveHeart();
        }
    }
}
