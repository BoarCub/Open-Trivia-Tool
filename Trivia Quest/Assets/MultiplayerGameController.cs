using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MultiplayerGameController : MonoBehaviour
{

    public Text questionDisplayText;
    public Text scoredDisplayText;
    public Text timeRemainingDisplayText;
    public SimpleObjectPool answerButtonObjectPool;
    public Transform answerButtonParent;
    public GameObject questionDisplay;
    public GameObject gameOverDisplay;
    public GameObject winGameDisplay;

    protected DataController dataController;
    protected RoundData currentRoundData;
    protected QuestionData[] questionPool;

    protected bool isRoundActive;
    protected float timeRemaining;
    protected int questionIndex;
    protected int playerScore;
    protected List<GameObject> answerButtonGameObjects = new List<GameObject>();

    public AudioSource GameMusic;
    public AudioSource EndgameSound;

    public GameObject correctImage;
    public GameObject incorrectImage;

    int playerScoreOne;

    public GameObject UI;
    public GameObject PlayerOneScreen;
    public GameObject PlayerTwoScreen;

    private bool lastRound = false;

    public Text EndGameText;

    private void Start()
    {
        
    }

    public void StartPlayerOneGame()
    {

        questionDisplay.SetActive(true);

        GameMusic.Play();

        UI.SetActive(true);
        PlayerOneScreen.SetActive(false);

        dataController = FindObjectOfType<DataController>();

        currentRoundData = dataController.GetCurrentRoundData();
        questionPool = currentRoundData.questions;
        timeRemaining = currentRoundData.timeLimitInSeconds;
        UpdateTimeRemainingDisplay();

        playerScore = 0;
        playerScoreOne = 0;
        questionIndex = 0;

        ShowQuestion();
        isRoundActive = true;
    }

    public void StartPlayerTwoGame()
    {

        correctImage.SetActive(false);
        incorrectImage.SetActive(false);

        questionDisplay.SetActive(true);

        GameMusic.Play();

        playerScoreOne = playerScore;

        UI.SetActive(true);
        PlayerTwoScreen.SetActive(false);

        dataController = FindObjectOfType<DataController>();

        currentRoundData = dataController.GetCurrentRoundData();
        questionPool = currentRoundData.questions;
        timeRemaining = currentRoundData.timeLimitInSeconds;
        UpdateTimeRemainingDisplay();

        playerScore = 0;
        questionIndex = 0;

        ShowQuestion();
        isRoundActive = true;

        scoredDisplayText.text = "Score: " + playerScore.ToString();
    }

    private void ShowQuestion()
    {
        RemoveAnswerButtons();
        QuestionData questionData = questionPool[questionIndex];
        questionDisplayText.text = questionData.questionText;

        for (int i = 0; i < questionData.answers.Length; i++)
        {
            GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();
            answerButtonGameObjects.Add(answerButtonGameObject);
            answerButtonGameObject.transform.SetParent(answerButtonParent);

            AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();
            answerButton.Setup(questionData.answers[i]);
        }
    }

    protected void RemoveAnswerButtons()
    {
        while (answerButtonGameObjects.Count > 0)
        {
            answerButtonObjectPool.ReturnObject(answerButtonGameObjects[0]);
            answerButtonGameObjects.RemoveAt(0);
        }
    }

    public void AnswerButtonClicked(bool isCorrect)
    {
        if (isCorrect)
        {
            playerScore += currentRoundData.pointsAddedForCorrectAnswer;
            scoredDisplayText.text = "Score: " + playerScore.ToString();
            correctImage.SetActive(true);
            incorrectImage.SetActive(false);
        }
        else
        {
            correctImage.SetActive(false);
            incorrectImage.SetActive(true);
        }

        if (questionPool.Length > questionIndex + 1)
        {
            questionIndex++;
            ShowQuestion();
        }
        else
        {
            EndRound();
        }
    }

    public void EndRound()
    {
        UI.SetActive(false);

        questionDisplay.SetActive(false);

        GameMusic.Stop();
        EndgameSound.Play();

        isRoundActive = false;

        if (lastRound)
        {
            gameOverDisplay.SetActive(true);

            if(playerScoreOne > playerScore)
            {
                EndGameText.text = "Player   One   Wins\nWith   " + playerScoreOne + "   Points";
            } else if (playerScoreOne < playerScore)
            {
                EndGameText.text = "Player   Two   Wins\nWith   " + playerScore + "   Points";
            }
            else
            {
                EndGameText.text = "Players Tie\nWith   " + playerScoreOne + "   Points";
            }
        }
        else
        {
            PlayerTwoScreen.SetActive(true);
            lastRound = true;
        }
    }

    public void EndRoundMultiplayer()
    {
        UI.SetActive(false);

        questionDisplay.SetActive(false);

        GameMusic.Stop();
        EndgameSound.Play();

        isRoundActive = false;

        if (lastRound)
        {
            gameOverDisplay.SetActive(true);

            if (playerScoreOne > playerScore)
            {
                EndGameText.text = "Player   One   Wins\nWith   " + playerScoreOne + "   Points";
            }
            else if (playerScoreOne < playerScore)
            {
                EndGameText.text = "Player   Two   Wins\nWith   " + playerScore + "   Points";
            }
            else
            {
                EndGameText.text = "Players Tie\nWith   " + playerScoreOne + "   Points";
            }
        }
        else
        {
            PlayerTwoScreen.SetActive(true);
            lastRound = true;
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MenuScreen");
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void UpdateTimeRemainingDisplay()
    {
        timeRemainingDisplayText.text = "Time: " + Mathf.Round(timeRemaining).ToString();
    }

    protected void Update()
    {
        if (isRoundActive)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimeRemainingDisplay();

            if (timeRemaining <= 0f)
            {
                EndRoundMultiplayer();
            }
        }
    }

}