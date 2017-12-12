using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour {

    public Button b1, b2, b3, b4, b5, b6, b7, b8, b9, b10, b11;
    public GameObject Menu1, Menu2, Menu3, Menu4;
    public Transform sun;
    private int hiscore;
    public Text hiscoretext, environmenttext, strafe1, strafe2, jump1, jump2;
    private bool darkmode;
    private bool died;
    private MobMovement script1;
    private EndlessSpawnerScript script2;
    private HumanAI script3;
    private BrainGenerator script4;
    private MovementScript script5;
    private CameraController script6;
    private Score score;
    public Image GameOverImage1, GameOverImage2;
    private Transform TransformImage1, TransformImage2;
    private float Image2Fill;
    private float timer1, timer2, timer3 = 0;
    private float scale;

    //MovementScript.HasDied(); MovementScript.GetScore();
    void Awake()
    {
        Screen.SetResolution(1000, 1500, false, 120);
#if UNITY_STANDALONE || UNITY_WEBPLAYER
        strafe2.gameObject.SetActive(true);
        jump2.gameObject.SetActive(true);
#else
        strafe1.gameObject.SetActive(true);
        jump1.gameObject.SetActive(true);
#endif
    }

    public void Init(int selectedmenu)
    {
        b1.onClick.RemoveAllListeners();
        b2.onClick.RemoveAllListeners();
        b3.onClick.RemoveAllListeners();
        b4.onClick.RemoveAllListeners();
        b5.onClick.RemoveAllListeners();
        b6.onClick.RemoveAllListeners();
        b7.onClick.RemoveAllListeners();
        b8.onClick.RemoveAllListeners();
        b9.onClick.RemoveAllListeners();
        b10.onClick.RemoveAllListeners();
        b11.onClick.RemoveAllListeners();
        if (selectedmenu == 1)
        {
            b1 = GameObject.FindGameObjectWithTag("Play").GetComponent<Button>();
            b2 = GameObject.FindGameObjectWithTag("Options").GetComponent<Button>();
            b1.onClick.AddListener(TaskOnClick1);
            b2.onClick.AddListener(TaskOnClick2);
            b9.onClick.AddListener(TaskOnClick9);
            script1 = GameObject.Find("Mobs").GetComponent<MobMovement>();
            script2 = GameObject.Find("EndlessSpawner").GetComponent<EndlessSpawnerScript>();
            script3 = GameObject.Find("Humans").GetComponent<HumanAI>();
            script4 = GameObject.Find("BrainSpawner").GetComponent<BrainGenerator>();
            script5 = GameObject.Find("Player").GetComponent<MovementScript>();
            script6 = GameObject.Find("Main Camera").GetComponent<CameraController>();
        } else if (selectedmenu == 2)
        {
            hiscoretext.text = "Hiscore: " + hiscore.ToString("D3");
            b3 = GameObject.FindGameObjectWithTag("ResetScore").GetComponent<Button>();
            b4 = GameObject.FindGameObjectWithTag("Dark").GetComponent<Button>();
            b5 = GameObject.FindGameObjectWithTag("Light").GetComponent<Button>();
            b6 = GameObject.FindGameObjectWithTag("Return").GetComponent<Button>();
            b3.onClick.AddListener(TaskOnClick3);
            b4.onClick.AddListener(TaskOnClick4);
            b5.onClick.AddListener(TaskOnClick5);
            b6.onClick.AddListener(TaskOnClick6);
        } else if (selectedmenu == 3)
        {
            b7 = GameObject.FindGameObjectWithTag("Retry").GetComponent<Button>();
            b8 = GameObject.FindGameObjectWithTag("Exit").GetComponent<Button>();
            b7.onClick.AddListener(TaskOnClick7);
            b8.onClick.AddListener(TaskOnClick8);
        } else if(selectedmenu == 4)
        {
            b10 = GameObject.FindGameObjectWithTag("Return").GetComponent<Button>();
            b10.onClick.AddListener(TaskOnClick10);
        }
    }

	void Start () {
        darkmode = true;
        died = false;
        TransformImage1 = GameOverImage1.transform;
        TransformImage2 = GameOverImage2.transform;
        Image2Fill = GameOverImage2.fillAmount;
        score = gameObject.AddComponent<Score>();
        LoadScore();
        hiscore = GetScore();
        scale = 150f;
        Init(1);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Cancel") && died != false){
            script1.DeleteMob();
            script1.EndBegin();
            script2.DeleteSpawner();
            script2.EndBegin();
            script3.DeleteHuman();
            script3.EndBegin();
            script4.DeleteBrains();
            script4.EndBegin();
            script5.DeletePlayer();
            script5.EndBegin();
            script6.EndBegin();
            Menu1.gameObject.SetActive(true);
            Menu2.gameObject.SetActive(false);
            Menu3.gameObject.SetActive(false);
            Init(1);
        }
        if (died)
        {
            if (script5.GetScore() > hiscore)
            {
                hiscore = script5.GetScore();
                score.SetScore(hiscore);
                SubmitScore(hiscore);
            }
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
                    GameOverImage2.fillAmount += timer3 * 0.5f;
                }
                if (GameOverImage2.fillAmount == 1)
                {
                    Menu3.gameObject.SetActive(true);
                    died = false;
                    Time.timeScale = 1;
                    script5.pause.onClick.RemoveAllListeners();
                    script1.DeleteMob();
                    script1.EndBegin();
                    script2.DeleteSpawner();
                    script2.EndBegin();
                    script3.DeleteHuman();
                    script3.EndBegin();
                    script4.DeleteBrains();
                    script4.EndBegin();
                    script5.EndBegin();
                    script5.DeletePlayer();
                    script6.EndBegin();
                    Menu1.gameObject.SetActive(false);
                    Menu2.gameObject.SetActive(false);
                    Menu3.gameObject.SetActive(true);
                    Init(3);
                }
            }

        }
	}


    private void TaskOnClick1()
    {
        sun = GameObject.FindGameObjectWithTag("Sun").transform;
        if (darkmode)
        {
            sun.transform.localEulerAngles = new Vector3(210, sun.transform.localEulerAngles.y, sun.transform.localEulerAngles.z);
        } else
        {
            sun.transform.localEulerAngles = new Vector3(20, sun.transform.localEulerAngles.y, sun.transform.localEulerAngles.z);
        }
        script5.Init(true);
        script6.Init(true);
        script1.Init(true);
        script2.Init(true);
        script3.Init(true);
        script4.Init(true);
        Menu1.gameObject.SetActive(false);

    }

    public void TaskOnClickPause()
    {
        Time.timeScale = 1;
        script5.pauseText.gameObject.SetActive(true);
        script5.playText.gameObject.SetActive(false);
        script5.pauseText2.gameObject.SetActive(false);
        script5.pausemenu.gameObject.SetActive(false);
        script5.pause.onClick.RemoveAllListeners();
        script1.DeleteMob();
        script1.EndBegin();
        script2.DeleteSpawner();
        script2.EndBegin();
        script3.DeleteHuman();
        script3.EndBegin();
        script4.EndBegin();
        script4.DeleteBrains();
        script5.EndBegin();
        script5.DeletePlayer();
        script6.EndBegin();
        if (darkmode)
        {
            sun.transform.localEulerAngles = new Vector3(210, sun.transform.localEulerAngles.y, sun.transform.localEulerAngles.z);
        }
        else
        {
            sun.transform.localEulerAngles = new Vector3(20, sun.transform.localEulerAngles.y, sun.transform.localEulerAngles.z);
        }
        timer1 = 0f;
        timer2 = 0f;
        scale = 150f;
        GameOverImage1.rectTransform.localScale = new Vector3(1, 1, 1) * scale;
        GameOverImage2.fillAmount = Image2Fill;
        Menu1.gameObject.SetActive(true);
        Init(1);
    }
    private void TaskOnClick2()
    {
        Menu1.gameObject.SetActive(false);
        Menu2.gameObject.SetActive(true);
        Init(2);
    }
    private void TaskOnClick3()
    {
        hiscore = 0;
        ResetScore();
        hiscoretext.text = "Hiscore: " + hiscore.ToString("D3");
    }
    private void TaskOnClick4()
    {
        darkmode = true;
        environmenttext.text = "Current Gamemode: Dark";
    }
    private void TaskOnClick5()
    {
        darkmode = false;
        environmenttext.text = "Current Gamemode: Light";
    }
    private void TaskOnClick6()
    {
        Menu1.gameObject.SetActive(true);
        Menu2.gameObject.SetActive(false);
        Init(1);
    }
    private void TaskOnClick7()
    {
        if (darkmode)
        {
            sun.transform.localEulerAngles = new Vector3(210, sun.transform.localEulerAngles.y, sun.transform.localEulerAngles.z);
        }
        else
        {
            sun.transform.localEulerAngles = new Vector3(20, sun.transform.localEulerAngles.y, sun.transform.localEulerAngles.z);
        }
        timer1 = 0f;
        timer2 = 0f;
        scale = 150f;
        GameOverImage1.rectTransform.localScale = new Vector3(1,1,1) * scale;
        GameOverImage2.fillAmount = Image2Fill;

        script5.Init(true);
        script6.Init(true);
        script1.Init(true);
        script2.Init(true);
        script3.Init(true);
        script4.Init(true);
        Menu3.gameObject.SetActive(false);
    }
    private void TaskOnClick8()
    {
        if (darkmode)
        {
            sun.transform.localEulerAngles = new Vector3(210, sun.transform.localEulerAngles.y, sun.transform.localEulerAngles.z);
        }
        else
        {
            sun.transform.localEulerAngles = new Vector3(20, sun.transform.localEulerAngles.y, sun.transform.localEulerAngles.z);
        }
        timer1 = 0f;
        timer2 = 0f;
        scale = 150f;
        GameOverImage1.rectTransform.localScale = new Vector3(1, 1, 1) * scale;
        GameOverImage2.fillAmount = Image2Fill;
        Menu3.gameObject.SetActive(false);
        Menu1.gameObject.SetActive(true);
        Init(1);

    }

    private void TaskOnClick9()
    {
        Menu1.gameObject.SetActive(false);
        Menu4.gameObject.SetActive(true);
        Init(4);
    }

    private void TaskOnClick10()
    {
        Menu4.gameObject.SetActive(false);
        Menu1.gameObject.SetActive(true);
        Init(1);
    }
    
    public void SetPauseListener()
    {
        b11.onClick.AddListener(TaskOnClickPause);
    }

    public void RemovePauseListener()
    {
        b11.onClick.RemoveAllListeners();
    }

    public void ControlDeath(bool x)
    {
        died = x;
    }
    
    private void LoadScore()
    {
        score = new Score();
        if (PlayerPrefs.HasKey("hiscore"))
        {
            score.hiscore = PlayerPrefs.GetInt("hiscore");
        }
    }

    private void SaveScore()
    {
        PlayerPrefs.SetInt("hiscore", score.hiscore);
    }

    private void ResetScore()
    {
        PlayerPrefs.SetInt("hiscore", 0);
    }

    public int GetScore()
    {
        return score.hiscore;
    }

    public void SubmitScore(int score2)
    {
            score.hiscore = score2;
            SaveScore();
    }

    }
