using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour {

    public Button b1, b2, b3, b4, b5, b6;
    public GameObject Menu1, Menu2;
    public Transform sun;
    private int hiscore;
    public Text hiscoretext, environmenttext;
    private bool darkmode;
    private MobMovement script1;
    private EndlessSpawnerScript script2;
    private HumanAI script3;
    private BrainGenerator script4;
    private MovementScript script5;

    //MovementScript.HasDied(); MovementScript.GetScore();

    public void Init(int selectedmenu)
    {
        if (selectedmenu == 1)
        {
            b1 = GameObject.FindGameObjectWithTag("Play").GetComponent<Button>();
            b2 = GameObject.FindGameObjectWithTag("Options").GetComponent<Button>();
            b1.onClick.AddListener(TaskOnClick1);
            b2.onClick.AddListener(TaskOnClick2);
            b3.onClick.RemoveAllListeners();
            b4.onClick.RemoveAllListeners();
            b5.onClick.RemoveAllListeners();
            b6.onClick.RemoveAllListeners();
            script1 = GameObject.Find("Mob").GetComponent<MobMovement>();
            script2 = GameObject.Find("EndlessSpawner").GetComponent<EndlessSpawnerScript>();
            script4 = GameObject.Find("BrainSpawner").GetComponent<BrainGenerator>();
            script5 = GameObject.Find("Zombie").GetComponent<MovementScript>();
        } else if (selectedmenu == 2)
        {
            b1.onClick.RemoveAllListeners();
            b2.onClick.RemoveAllListeners();
            hiscoretext.text = "Hiscore: " + hiscore.ToString("D3");
            b3 = GameObject.FindGameObjectWithTag("ResetScore").GetComponent<Button>();
            b4 = GameObject.FindGameObjectWithTag("Dark").GetComponent<Button>();
            b5 = GameObject.FindGameObjectWithTag("Light").GetComponent<Button>();
            b6 = GameObject.FindGameObjectWithTag("Return").GetComponent<Button>();
            b3.onClick.AddListener(TaskOnClick3);
            b4.onClick.AddListener(TaskOnClick4);
            b5.onClick.AddListener(TaskOnClick5);
            b6.onClick.AddListener(TaskOnClick6);
        }
    }

	void Start () {
        darkmode = true;
        hiscore = 0;
        Init(1);
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    private void TaskOnClick1()
    {
        Debug.Log("Clicked!");
        sun = GameObject.FindGameObjectWithTag("Sun").transform;
        if (darkmode)
        {
            sun.transform.localEulerAngles = new Vector3(210, sun.transform.localEulerAngles.y, sun.transform.localEulerAngles.z);
        } else
        {
            sun.transform.localEulerAngles = new Vector3(20, sun.transform.localEulerAngles.y, sun.transform.localEulerAngles.z);
        }
        script1.Init(true);
        script2.Init(true);
        script4.Init(true);
        script5.Init(true);
        Menu1.gameObject.SetActive(false);
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
}
