using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariablesList:MonoBehaviour
{
    //public static bool isPlayerDetailsUpdated = false;
    //public static bool isQuestionaireCompleted = false;

    public void SetIsPlayerDetailsUpdated(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    public int IsPlayerDetailsUpdated(string key)
    {
        return PlayerPrefs.GetInt(key);
    }


    //call the function to set the value of the key
    public void SetIsQuestionaireCompleted(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    //call the function to get the value of the key
    public int IsQuestionaireCompleted(string key)
    {
        return PlayerPrefs.GetInt(key);
    }




}
