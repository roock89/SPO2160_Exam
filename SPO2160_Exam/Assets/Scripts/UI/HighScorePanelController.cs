using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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
        manager.ghostInputData = ghostInputs;
        SceneManager.LoadScene(0);
    }
}
