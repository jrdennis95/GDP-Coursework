using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementScript : MonoBehaviour {

    private GameStart gs;
    private CharacterController control;
    private CollisionControl cc;
    private GameObject mob;
    public GameObject zombie;
    public Text UItext, pauseText, playText, pauseText2;
    public GameObject pausemenu;
    private Vector3 movement;
    public GameObject[] hearts;
    private Transform canvas;
    private List<GameObject> activeHearts;
    private List<GameObject> active;
    public Button pause;
    public AudioClip mobaudio;
    private AudioSource mobsource, music;
    private bool dead;
    private float distancebetween;
    public float strafespeed;
    public float runspeed;
    private float fixedrunspeed;
    private float totalspeed;
    private int score;
    private int heartcount;
    private float jumpSpeed = 0.0f;
    private bool begin = false;
    private bool paused;
    private Vector2 touchOrigin;

    public void Init(bool started)
    {
        active = new List<GameObject>();
        GameObject go;
        go = Instantiate(zombie) as GameObject;
        go.transform.position = new Vector3(7.63f, 0, -10.55f);
        go.transform.SetParent(transform);
        go.transform.tag = "Zombie";
        active.Add(go);
        paused = false;
        gs = GameObject.FindGameObjectWithTag("Canvas").GetComponent<GameStart>();
        control = GameObject.FindGameObjectWithTag("Zombie").GetComponent<CharacterController>();
        mobsource.Play();
        mobsource.volume = 0.05f;
        pause.onClick.AddListener(TaskOnClickPause);
        //GameOverImage = GameObject.Find("GameOverImage").GetComponent<GameObject>();
        score = 0;
        heartcount = 3;
        dead = false;
        runspeed = fixedrunspeed;
        totalspeed = fixedrunspeed;
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
        activeHearts = new List<GameObject>();
        for (int i = 0; i < 3; i++)
        {
            go = Instantiate(hearts[i]) as GameObject;
            go.transform.position = new Vector3(153 + (i * 90), -57, 0);
            go.transform.SetParent(canvas, false);
            activeHearts.Add(go);
        }
        cc = GameObject.FindGameObjectWithTag("Zombie").GetComponent<CollisionControl>();
        cc.Init(true);
        begin = true;
    }

        void Start () {
        fixedrunspeed = runspeed;
            heartcount = 3;
        mobsource = GameObject.Find("MobSound").GetComponent<AudioSource>();
        music = GameObject.Find("Music").GetComponent<AudioSource>();
        mobsource.clip = mobaudio;
        mobsource.volume = 0.05f;
    }

    // Update is called once per frame
    void Update () {
        if (begin)
        {
            mob = GameObject.FindGameObjectWithTag("Mob");

            //Jumping
            if (dead == false && paused == false)
            {
                distancebetween = active[0].transform.position.z - mob.transform.position.z;
                mobsource.volume = (1 - distancebetween * 0.1f) - 0.2f;
                totalspeed = runspeed + (score * 0.05f);
                float horizontal = 0;
                float vertical = 0;
#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR

                if (Input.GetButtonDown("Jump") && control.isGrounded)
                {
                    jumpSpeed = 5.0f;
                }
                else
                {
                    jumpSpeed = movement.y + (Physics.gravity.y * Time.deltaTime);
                }
                horizontal = Input.GetAxisRaw("Horizontal");
#else

                if (Input.touchCount > 0)
                {
                    Touch myTouch = Input.touches[0];
                    if (myTouch.phase == TouchPhase.Began)
                    {
                        touchOrigin = myTouch.position;
                    }
                    else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
                    {
                        Vector2 touchEnd = myTouch.position;
                        float y = touchEnd.y - touchOrigin.y;
                            vertical = y > 0 ? 1 : -1;
                    }
                }

                if (vertical == 1 && control.isGrounded)
                {
                    jumpSpeed = 5.0f;
                }
                else
                {
                    jumpSpeed = movement.y + (Physics.gravity.y * Time.deltaTime);
                }
                horizontal = Input.acceleration.x*2;
#endif
                movement.x = horizontal * strafespeed;
                    movement.y = jumpSpeed;
                    movement.z = totalspeed;
                    control.Move(movement * Time.deltaTime);
                }

            //UI
           /* panel.color = new Color(1, 0, 0, gamma);
            if (distancebetween > -1.5f)
            {
                gamma = 0;
            }
            else
            {
                gamma = (distancebetween * -1) / 1.5f - 0.8f;
            } */
            UItext.text = score.ToString("D3");
            //Collisions
        }
    }
    
    private void TaskOnClickPause()
    {
        if (Time.timeScale == 1 && paused == false)
        {
            mobsource.Pause();
            music.Pause();
            Time.timeScale = 0;
            playText.gameObject.SetActive(true);
            pauseText.gameObject.SetActive(false);
            pauseText2.gameObject.SetActive(true);
            pausemenu.gameObject.SetActive(true);
            gs.SetPauseListener();
        } else
        {
            mobsource.Play();
            music.Play();
            Time.timeScale = 1;
            pauseText.gameObject.SetActive(true);
            playText.gameObject.SetActive(false);
            pauseText2.gameObject.SetActive(false);
            pausemenu.gameObject.SetActive(false);
            gs.RemovePauseListener();
        }
    }
    
    public void DeletePlayer()
    {
        cc.Init(false);
        Destroy(active[0]);
        active.RemoveAt(0);
        Destroy(activeHearts[0]);
        Destroy(activeHearts[1]);
        Destroy(activeHearts[2]);
        activeHearts.Clear();
    }

    public float GetDistanceBetween()
    {
        return distancebetween;
    }

    public void addScore(int x)
    {
        score = score + x;
    }

    public void subtractScore(int x)
    {
        score = score - x;
    }

    public void SubtractSpeed(float x)
    {
        runspeed = runspeed - x;
    }
    public void AddSpeed(float x)
    {
        runspeed = runspeed + x;
    }


    public float GetTotalSpeed()
    {
        return totalspeed;
    }

    public int GetScore()
    {
        return score;
    }


    public void EndBegin()
    {
        begin = false;
    }

    public void StartBegin()
    {
        begin = true;
    }

    public int GetHearts()
    {
        return heartcount;
    }
    
    public void AddHeart()
    {
        heartcount++;
    }

    public void RemoveHeart()
    {
        heartcount--;
    }

    public void SetHeartActive(int x, bool y)
    {
        activeHearts[x].gameObject.SetActive(y);
    }

    public void SetDead(bool x)
    {
        dead = x;
    }
}
