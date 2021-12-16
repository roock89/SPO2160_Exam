using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text scoreText;
    public Text gameoverText;

    // Update is called once per frame
    void Update()
    {
        if(Score.LapTime != 0)
        {
            scoreText.text = "Your time was: " + (Score.LapTime / 60).ToString("00") + ":" + (Score.LapTime%60).ToString("00");
        }
        else
        {
            scoreText.text = "";
            gameoverText.text = "You failed";
        }
    }
}
