using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections.Generic;

public class GameOverManager : MonoBehaviour
{
    public string logoutUrl, savingScoreURL;
    public GhostHolder playerData; 

    // Start is called before the first frame update
    void Start()
    {
        playerData.lapTime = Score.LapTime;
        if(playerData.lapTime != 0)
            StartCoroutine(SavingScore(savingScoreURL, playerData));
    }

    public IEnumerator SavingScore(string uri, GhostHolder data)
    {
        Debug.Log("save score url: " + uri);

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();

        // Add username and password as separate form data sections!
        formData.Add(new MultipartFormDataSection("time", data.lapTime.ToString()));

        string positionData = "";
        foreach(Vector3 pos in data.position)
        {
            positionData += pos + ":";
        }
        positionData += ";";        
        foreach(Vector3 rot in data.rotation)
        {
            positionData += rot + ":";
        }
        positionData += ";";
        foreach (float time in data.timeStamp)
        {
            positionData += time + ":";
        }
        formData.Add(new MultipartFormDataSection("ghostInput", positionData));

        UnityWebRequest www = UnityWebRequest.Post(uri, formData);
        yield return www.SendWebRequest();

        // Split string using space, semi colon, comma, dot
        if (www.downloadHandler.text.Length > 1)
        {
            Debug.Log("More than one character received");
            //Split string into two parts using string.split
            Debug.Log(www.downloadHandler.text);

        }

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1); // Load the GamePlayScene again
    }

    public void Logout()
    {
        StartCoroutine(Logout(logoutUrl));
    }

    private IEnumerator Logout(string uri)
    {
        Debug.Log("logout url: " + uri);

        UnityWebRequest www = UnityWebRequest.Post(uri, "");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            switch (www.downloadHandler.text)
            {
                case "0":
                    PlayerPrefs.DeleteKey("UserName");
                    PlayerPrefs.DeleteKey("Date");
                    PlayerPrefs.DeleteKey("AttemptsUsed");
                    SceneManager.LoadScene(0);
                    break;
                default:
                    break;
            }
        }
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
