using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{

    public Text answerText;

    public AnswerData answerData;

    private void Start()
    {

    }

    public void Setup(AnswerData data)
    {
        answerData = data;
        answerText.text = answerData.answerText;
    }

    public void HandleClick()
    {
        if (!FindObjectOfType<DataController>().isMultiplayer) {
            FindObjectOfType<GameController>().AnswerButtonClicked(answerData.isCorrect);
        } else{
            FindObjectOfType<MultiplayerGameController>().AnswerButtonClicked(answerData.isCorrect);
        }
    }

    void Update()
    {

    }

}