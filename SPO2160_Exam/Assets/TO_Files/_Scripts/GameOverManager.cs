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
        StartCoroutine(SavingScore(savingScoreURL, playerData));
    }

    public IEnumerator SavingScore(string uri, GhostHolder data)
    {
        Debug.Log("Test score script: " + uri);

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();

        // Add username and password as separate form data sections!
        formData.Add(new MultipartFormDataSection("score", data.lapTime.ToString()));

        string positionData = "";
        foreach(Vector3 pos in data.position)
        {
            positionData += pos + ":";
        }
        positionData += ";";        
        foreach(Vector3 pos in data.rotation)
        {
            positionData += pos + ":";
        }

        formData.Add(new MultipartFormDataSection("input", positionData));

        UnityWebRequest www = UnityWebRequest.Post(uri, formData);
        yield return www.SendWebRequest();
        Debug.Log(www.downloadHandler.text);

        // Split string using space, semi colon, comma, dot
        if (www.downloadHandler.text.Length > 1)
        {
            Debug.Log("More than one character received");
            //Split string into two parts using string.split
            string[] feedback = www.downloadHandler.text.Split(';');
            Debug.Log(feedback[0].ToString());
        }
        else
        {
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
        Debug.Log("Test logout script: " + uri);

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
}
