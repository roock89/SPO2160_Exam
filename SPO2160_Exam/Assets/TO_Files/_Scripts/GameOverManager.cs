using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;


public class GameOverManager : MonoBehaviour
{
    public string logoutUrl;
    public int attemptsUsed;

    public Text feedbackText;

    // Start is called before the first frame update
    void Start()
    {
        attemptsUsed = PlayerPrefs.GetInt("AttemptsUsed");
        feedbackText.text = "You guessed " + attemptsUsed.ToString() + " times";
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
