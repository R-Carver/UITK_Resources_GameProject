using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public List<Question> questions = new List<Question>();

    Question currentQuestion;
    int questionIndex = 0;

    string currentHint;
    int hintIndex = 0;

    public void InitializeGame()
    {
        currentQuestion = questions[questionIndex];
        currentHint = currentQuestion.GetHints()[hintIndex];
    }

    public bool IsAnswerCorrect(string answer)
    {
        return currentQuestion.answer == answer;
    }

    public void HandleCorrectAnswer()
    {
        NextQuestion();
    }

    public void HandleWrongAnswer()
    {
        if (hintIndex < 2)
        {
            currentHint = currentQuestion.GetHints()[++hintIndex];
        }
        else
        {
            NextQuestion();
        }
    }

    public void NextQuestion()
    {
        currentQuestion = questions[++questionIndex];

        hintIndex = 0;
        currentHint = currentQuestion.GetHints()[hintIndex];
    }

    public Question getCurrentQuestion()
    {
        return currentQuestion;
    }

    public int getCurrentQuestionNum()
    {
        return questionIndex + 1;
    }

    public string getCurrentHint()
    {
        return currentHint;
    }

    public int getCurrentHintNum()
    {
        return hintIndex + 1;
    }


}
