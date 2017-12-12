using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    // Use this for initialization
    public int hiscore;

    public int GetScore()
    {
        return hiscore;
    }

    public void SetScore(int x)
    {
        hiscore = x;
    }
    public void addScore(int x)
    {
        hiscore = hiscore + x;
    }

    public void subtractScore(int x)
    {
        hiscore = hiscore - x;
    }
}
