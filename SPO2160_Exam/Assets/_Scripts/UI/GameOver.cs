using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text scoreText;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Your time was: " + Score.LapTime.ToString();
        Debug.Log(Score.LapTime);
    }
}
