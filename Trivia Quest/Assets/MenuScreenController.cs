using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreenController : MonoBehaviour
{

    public GameObject MenuScreen;
    public GameObject HelpScreen;
    public GameObject CreditsScreen;
    public GameObject GameModeScreen;

    private bool isMultiplayer = false;

    public GameObject TimeAttackPanel;
    public GameObject MultiplayerPanel;

    public GameObject QuestionSetScreen;
    public GameObject DifficultyScreen;

    private int CurrentQuestionSet = 0;
    private int CurrentDifficulty = 0;
    

    // Start is called before the first frame update
    public void StartGame()
    {
        GameMode();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Help()
    {
        MenuScreen.SetActive(false);
        HelpScreen.SetActive(true);
    }

    public void Credits()
    {
        MenuScreen.SetActive(false);
        CreditsScreen.SetActive(true);
    }

    public void CreditsBack()
    {
        MenuScreen.SetActive(true);
        CreditsScreen.SetActive(false);
    }

    public void GameModeBack()
    {
        GameModeScreen.SetActive(false);
        MenuScreen.SetActive(true);
        TimeAttackPanel.SetActive(false);
        MultiplayerPanel.SetActive(false);
    }

    public void GameMode()
    {
        GameModeScreen.SetActive(true);
        MenuScreen.SetActive(false);
        TimeAttackPanel.SetActive(false);
        MultiplayerPanel.SetActive(false);
    }

    public void TimeAttack()
    {
        TimeAttackPanel.SetActive(true);
        MultiplayerPanel.SetActive(false);
    }

    public void Multiplayer()
    {
        TimeAttackPanel.SetActive(false);
        MultiplayerPanel.SetActive(true);
    }

    public void TimeAttackPlay()
    {
        FindObjectOfType<DataController>().roundIndex = 3 * CurrentQuestionSet + CurrentDifficulty;
        if (!isMultiplayer)
        {
            SceneManager.LoadScene("Game");
        }
        else
        {
            SceneManager.LoadScene("MultiplayerGame");
        }
    }

    public void HelpBack()
    {
        MenuScreen.SetActive(true);
        HelpScreen.SetActive(false);
    }

    public void SetQuestionSet(int value)
    {
        CurrentQuestionSet = value;
    }

    public void SetDifficulty(int value)
    {
        CurrentDifficulty = value;
    }

    public void QuestionSet()
    {
        GameModeScreen.SetActive(false);
        QuestionSetScreen.SetActive(true);
    }

    public void setMultiplayer(bool isMultiplayerParam)
    {
        FindObjectOfType<DataController>().isMultiplayer = isMultiplayerParam;
        isMultiplayer = isMultiplayerParam;
    }

    public void QuestionSetBack()
    {
        GameModeScreen.SetActive(true);
        QuestionSetScreen.SetActive(false);
    }

    public void Difficulty()
    {
        DifficultyScreen.SetActive(true);
        QuestionSetScreen.SetActive(false);
    }

    public void DifficultyBack()
    {
        DifficultyScreen.SetActive(false);
        QuestionSetScreen.SetActive(true);
    }

}