using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class LeaderboardController : MonoBehaviour
{
    public GameObject _highScorePanel;
    public Transform ScrollContent;
    private RectTransform ContentSize;
    public string HighscoreURL;
    public string[] scores;
    
    private readonly Vector3 StartPos = new Vector3(0,660,0); // highscore starting pos
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
        StartCoroutine(loadHighScores(HighscoreURL));
    }

    public IEnumerator loadHighScores(string uri)
    {
        Debug.Log("Test score script: " + uri);

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();

        UnityWebRequest www = UnityWebRequest.Post(uri, formData);
        yield return www.SendWebRequest();
        // Debug.Log(www.downloadHandler.text);

        // Split string using space, semi colon, comma, dot
        if (www.downloadHandler.text.Length > 1)
        {
            Debug.Log("More than one character received");
            //Split string into two parts using string.split
            Debug.Log( www.downloadHandler.text);
            scores = www.downloadHandler.text.Split(';');
        }
        else
        {
            scores = new string[1];
            scores[0] = www.downloadHandler.text;
        }

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }

        ContentSize = ScrollContent.GetComponent<RectTransform>();
        for(int i = 0; i < ScrollContent.childCount; i++)
        {
            Destroy(ScrollContent.GetChild(i).gameObject);
        }
        ContentSize.sizeDelta = new Vector2(3237, 1520.5f); // resetting scroll content size
        int ChallengerNumb = scores.Length; // number of scores loading in
        ContentSize.sizeDelta = new Vector2(3237, (ChallengerNumb-1) * 230f); // ScrollContent total size
        for(int i = 0; i < ChallengerNumb -1 && ChallengerNumb > 0; i++)
        {
            GameObject spawn = Instantiate(_highScorePanel);
            spawn.transform.SetParent(ScrollContent, false);
            spawn.transform.localPosition = StartPos + new Vector3(0, i, 0) * -200; // position delta

            HighScorePanelController controller = spawn.GetComponent<HighScorePanelController>();
            string[] _split = scores[i].Split(':');
            if(_split.Length > 1)
            {
                controller.PlayerText.text = _split[0];
                controller.ScoreText.text = _split[1]; 
                // Debug.LogWarning(_split[2]);
                int.TryParse(_split[2], out controller.scoreID);
            }
            else
                controller.PlayerText.text = _split[0];
            Debug.Log(spawn);
        }
        ScrollContent.transform.position -= new Vector3(0, 100 ,0); // correct starting position. might need a bigger number if loading more than 10 highscores
        
        // if(scores != null)
        // {
        //     foreach(string score in scores)
        //     {
        //         Debug.Log(score);
        //     }
        // }
    }

}