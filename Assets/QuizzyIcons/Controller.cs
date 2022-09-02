using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Game game;
    public UI ui;

    private int currentCounter;

    public int CurrentCounter
    {
        get { return currentCounter; }
        set
        {   
            if (value == 0)
            {
                HandleWrongAnswer();
                return;
            }
            ui.SetTimer(value.ToString());
            currentCounter = value;
        }
    }




    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        game.InitializeGame();
        UpdateUI();

        ResetCounter();
        StartCoroutine(UpdateCounter());
    }

    public void HandleWrongAnswer()
    {
        game.HandleWrongAnswer();
        UpdateUI();
        ResetCounter();
    }

    public void HandleCorrectAnswer()
    {
        game.HandleCorrectAnswer();
        UpdateUI();
        ResetCounter();
    }

    public void CheckAnswer(string answer)
    {
        bool answerCorrect = game.IsAnswerCorrect(answer);

        if (answerCorrect)
        {
            HandleCorrectAnswer();
            Debug.Log("Answer was correct");
        }
        else
        {
            HandleWrongAnswer();
            Debug.Log("Answer was wrong");
        }

        ui.GiveAnswerFeedback(answerCorrect);
    }


    public void UpdateUI()
    {
        ui.SetHint(game.getCurrentHint());
        ui.SetHintNumber(game.getCurrentHintNum());
        ui.SetQuestionNumber(game.getCurrentQuestionNum());
    }

    public List<Question> getAllQuestions()
    {
        return game.questions;
    }

    void ResetCounter()
    {
        CurrentCounter = 30;
    }

    IEnumerator UpdateCounter()
    {
        yield return new WaitForSeconds(1);
        CurrentCounter--;
        StartCoroutine(UpdateCounter());
    }
}
