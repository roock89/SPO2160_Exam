using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardController : MonoBehaviour
{
    public GameObject _highScorePanel;
    public Transform ScrollContent;
    private RectTransform ContentSize;
    
    private readonly Vector3 StartPos = new Vector3(0, 897.4f, 0); // highscore starting pos
    // Start is called before the first frame update
    
    void OnEnable()
    {
        loadHighScores();
    }

    public void refreshLeaderboard()
    {
        loadHighScores();
    }

    private void loadHighScores()
    {
        ContentSize = ScrollContent.GetComponent<RectTransform>();
        for(int i = 0; i < ScrollContent.childCount; i++)
        {
            Destroy(ScrollContent.GetChild(i).gameObject);
        }
        ContentSize.sizeDelta = new Vector2(3237, 0); // resetting scroll content size
        int ChallengerNumb = 10; // number of scores loading in
        ContentSize.sizeDelta = new Vector2(3237, (ChallengerNumb-1) * 230f); // ScrollContent total size
        for(int i = 0; i < ChallengerNumb; i++)
        {
            GameObject spawn = Instantiate(_highScorePanel);
            spawn.transform.SetParent(ScrollContent, false);
            spawn.transform.localPosition = StartPos + new Vector3(0, i, 0) * -200; // position delta
        }
        ScrollContent.transform.position -= new Vector3(0, 100 ,0); // correct starting position. might need a bigger number if loading more than 10 highscores
    }
}