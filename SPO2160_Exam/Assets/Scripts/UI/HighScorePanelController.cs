using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScorePanelController : MonoBehaviour
{
    public Text PlayerText, ScoreText;
    private string ghostInputs;
    private GameManager manager;

    void Start()
    {
        manager = GameObject.FindObjectOfType<GameManager>();
    }

    public void challengePlayer()
    {

    }
}
