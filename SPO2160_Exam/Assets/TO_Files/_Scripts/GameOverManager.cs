using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections.Generic;

public class GameOverManager : MonoBehaviour
{
    public string logoutUrl, savingScoreURL;
    public int attemptsUsed;
    public Text feedbackText, scoreText;

    // Start is called before the first frame update
    void Start()
    {
        attemptsUsed = PlayerPrefs.GetInt("AttemptsUsed");
        feedbackText.text = "You guessed " + attemptsUsed.ToString() + " times";
        StartCoroutine(SavingScore(savingScoreURL, attemptsUsed.ToString()));
    }

    public IEnumerator SavingScore(string uri, string score)
    {
        Debug.Log("Test score script: " + uri);

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();

        // Add username and password as separate form data sections!
        formData.Add(new MultipartFormDataSection("score", score));

        UnityWebRequest www = UnityWebRequest.Post(uri, formData);
        yield return www.SendWebRequest();
        Debug.Log(www.downloadHandler.text);

        // Split string using space, semi colon, comma, dot
        if (www.downloadHandler.text.Length > 1)
        {
            Debug.Log("More than one character received");
            //Split string into two parts using string.split
            string[] feedback = www.downloadHandler.text.Split(';');
            score = feedback[0].ToString();
        }
        else
        {
            score = www.downloadHandler.text;
        }

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }

        scoreText.text = score;
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
