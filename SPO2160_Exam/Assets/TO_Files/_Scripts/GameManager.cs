using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Canvas gameOverCanvas;

    public void GameOver()
    {
        gameOverCanvas.gameObject.SetActive(true);
    }
}
