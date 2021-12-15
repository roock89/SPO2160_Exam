using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;


public class CommunicationController : MonoBehaviour
{
    public string loginUrl, logoutUrl, registerUrl;
    private string username, password;
    public Canvas loginCanvas, logoutCanvas, errorCanvas, ChallengerCanvas;
    public InputField usernameText, passwordText;
    public Text errorText, welcomeText;
    public Button loginButton;

    private string userFeedback;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("UserName").Length > 0)
        {
            loginCanvas.gameObject.SetActive(false);
            logoutCanvas.gameObject.SetActive(true);
            errorCanvas.gameObject.SetActive(false);
        }
        else
        {
            loginCanvas.gameObject.SetActive(true);
            loginButton.interactable = false;
            logoutCanvas.gameObject.SetActive(false);
            errorCanvas.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (usernameText.text.Length > 0 && passwordText.text.Length > 7)
        {
            loginButton.interactable = true;
        }
    }

    public void LoginUser()
    {
        StartCoroutine(Login(loginUrl));
    }

    public void LogoutUser()
    {
        StartCoroutine(Logout(logoutUrl));
        this.enabled = true;
    }

    private IEnumerator Login(string uri)
    {
        Debug.Log("Test login script: " + uri);
        username = usernameText.text;
        password = passwordText.text;

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();

        // Add username and password as separate form data sections!
        formData.Add(new MultipartFormDataSection("username", username));
        formData.Add(new MultipartFormDataSection("password", password));

        UnityWebRequest www = UnityWebRequest.Post(uri, formData);
    

        yield return www.SendWebRequest();
        Debug.Log(www.downloadHandler.text);

        string message = "";
        string userId = "";

        // Split string using space, semi colon, comma, dot
        if (www.downloadHandler.text.Length > 1)
        {
            Debug.Log("More than one character received");
            //Split string into two parts using string.split
            string[] feedback = www.downloadHandler.text.Split(';');
            message = feedback[0].ToString();
            message = new string(message.Where(c => char.IsDigit(c)).ToArray());
            Debug.Log("Message: " + message + " (" + message.Length + ")");

            if (feedback.Length > 1)
            {

                userId = feedback[1].ToString();
                userId = new string(userId.Where(c => char.IsDigit(c)).ToArray());
                Debug.Log("UserID: " + userId);
                PlayerPrefs.SetString("UserId", userId);
            }
        }
        else
        {
            message = www.downloadHandler.text;
        }

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            switch (message)
            {
                case "0":
                    Debug.Log("Success! You're logged on: " + username);
                    PlayerPrefs.SetString("UserName", username);
                    PlayerPrefs.SetString("Date", DateTime.Now.ToString());
                    loginCanvas.gameObject.SetActive(false);
                    logoutCanvas.gameObject.SetActive(true);
                    errorCanvas.gameObject.SetActive(false);
                    break;
                case "1":
                    userFeedback = "The password you entered was not valid. Please try again.";
                    ShowError(userFeedback);                    
                    break;
                case "2":
                    userFeedback = "No account found with that username. Try again, or register a new user.";
                    ShowError(userFeedback);
                    break;
                case "3":
                    userFeedback = "Sorry. We could not log you on at present. Please try again later.";
                    ShowError(userFeedback);
                    break;
                case "4":
                    Debug.Log("Game session already in progress.");
                    Debug.Log("Username in session: " + PlayerPrefs.GetString("UserName"));
                    Debug.Log("Logged on since: " + PlayerPrefs.GetString("Date"));
                    welcomeText.text = "Welcome - " + PlayerPrefs.GetString("UserName");
                    loginCanvas.gameObject.SetActive(false);
                    logoutCanvas.gameObject.SetActive(true);
                    errorCanvas.gameObject.SetActive(false);
                    break;
                default:
                    break;
            }
        }
    }

    private IEnumerator Logout(string uri)
    {
        Debug.Log("Test logout script: " + uri);

        UnityWebRequest www = UnityWebRequest.Post(uri,"");
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
                    loginCanvas.gameObject.SetActive(true);
                    logoutCanvas.gameObject.SetActive(false);
                    usernameText.text = passwordText.text = "";
                    username = password = "";
                    loginButton.interactable = false;
                    Debug.Log("Logout Successful!");
                    break;
                default:
                    break;
            }
        }
    }

    public void ShowLogin()
    {
        loginCanvas.gameObject.SetActive(true);
        logoutCanvas.gameObject.SetActive(false);
        errorCanvas.gameObject.SetActive(false);
        ChallengerCanvas.gameObject.SetActive(false);
        usernameText.text = passwordText.text = "";
        loginButton.interactable = false;
    }

    public void ShowLogout()
    {
        loginCanvas.gameObject.SetActive(false);
        logoutCanvas.gameObject.SetActive(true);
        errorCanvas.gameObject.SetActive(false);
        ChallengerCanvas.gameObject.SetActive(false);
        usernameText.text = passwordText.text = "";
        loginButton.interactable = false;
    }

    public void ShowLeaderboard()
    {
        loginCanvas.gameObject.SetActive(false);
        logoutCanvas.gameObject.SetActive(false);
        errorCanvas.gameObject.SetActive(false);
        ChallengerCanvas.gameObject.SetActive(true);
        usernameText.text = passwordText.text = "";
        loginButton.interactable = false;
    }

    public void RegisterWeb()
    {
        Application.OpenURL(registerUrl);
    }

    private void ShowError(string feedback)
    {
        loginCanvas.gameObject.SetActive(false);
        logoutCanvas.gameObject.SetActive(false);
        errorCanvas.gameObject.SetActive(true);
        ChallengerCanvas.gameObject.SetActive(false);
        errorText.text = feedback;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    
}
