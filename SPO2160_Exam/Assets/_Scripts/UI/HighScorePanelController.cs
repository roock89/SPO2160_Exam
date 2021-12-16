using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class HighScorePanelController : MonoBehaviour
{
    public Text PlayerText, ScoreText;
    public int scoreID;
    private GameManager manager;

    void Start()
    {
        manager = GameObject.FindObjectOfType<GameManager>();
    }

    public void challengePlayer()
    {
        manager.getGhostData(scoreID);
        SceneManager.LoadScene(1);
    }
}
