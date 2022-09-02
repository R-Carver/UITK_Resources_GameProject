using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Question : ScriptableObject
{
    public string answer;
    public string display_answer;

    public string[] hints = new string[3];

    public string[] GetHints()
    {
        if (hints.Length == 0)
        {
            Debug.LogError("hints not inizitilized");
        }
        return hints;
    }

}
