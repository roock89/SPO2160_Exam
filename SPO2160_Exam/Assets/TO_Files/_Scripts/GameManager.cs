using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Canvas gameCanvas;
    public Button gameButton, restartButton;
    public Text feedbackText;

    private int numberToGuess, numberOfAttempts = 0, level = 0;
    private List<KeyValuePair<int, int>> gameBoards = new List<KeyValuePair<int, int>>();
    private float buttonWidth = 50f;
    private bool isPlayingLevel;

    // Start is called before the first frame update
    void Start()
    {
        isPlayingLevel = false;

        gameBoards.Add(new KeyValuePair<int, int>(10, 10));
        /*
         * gameBoards.Add(new KeyValuePair<int, int>(12, 12));
         * gameBoards.Add(new KeyValuePair<int, int>(13, 13));
         * gameBoards.Add(new KeyValuePair<int, int>(12, 15));
         */

        SetupBoard(level);
    }


    private void SetupBoard(int gameLevel)
    {
        // Hide restart button
        restartButton.gameObject.SetActive(false);
        
        // Retrieve the rows and columns for the current level
        KeyValuePair<int, int> dimensions = gameBoards[gameLevel];

        int rows = dimensions.Key;
        int cols = dimensions.Value;

        // Create random number to guess
        numberToGuess = Random.Range(1, rows * cols);
        PlayerPrefs.SetInt("NumberToGuess", numberToGuess);

        // Setup board of buttons on screen
        int buttonNr = 0;
        Vector3 origin = 
            new Vector3(buttonWidth / 2f - buttonWidth * cols / 2f, -buttonWidth + buttonWidth * rows / 2f, 0f);
        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                Button newButton = Instantiate(gameButton);
                newButton.transform.SetParent(gameCanvas.transform, false);
                newButton.GetComponent<RectTransform>().localPosition = origin + new Vector3(i * buttonWidth, -j * buttonWidth, 0f);
                newButton.GetComponentInChildren<Text>().text = (++buttonNr).ToString();
                newButton.name = buttonNr.ToString();
            }
        }
    }

    public void SelectNumber(int guess)
    {
        if (!isPlayingLevel)
            isPlayingLevel = true;

        if (guess < numberToGuess)
        {
            feedbackText.text = guess.ToString() + " is too low";
            numberOfAttempts++;
        }
        else if (guess > numberToGuess)
        {
            feedbackText.text = guess.ToString() + " is too high";
            numberOfAttempts++;
        }
        else
        {
            feedbackText.text = guess.ToString() + " is correct! " + ++numberOfAttempts + " guesses!";
            if (level < (gameBoards.Count - 1))
            {
                level++;
            }
            PlayerPrefs.SetInt("AttemptsUsed", numberOfAttempts);
            SceneManager.LoadScene(2);
        }
    }

    // Not in use
    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
}
