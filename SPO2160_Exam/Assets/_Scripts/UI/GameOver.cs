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
            string seconds = (int)Score.LapTime%60 + "";
            if(seconds.ToCharArray().Length == 1)
                seconds = "0" + seconds;

            scoreText.text = "Your time was: " + (Score.LapTime / 60) + ":" + seconds;
        }
        else
        {
            scoreText.text = "";
            gameoverText.text = "You failed";
        }
    }
}
