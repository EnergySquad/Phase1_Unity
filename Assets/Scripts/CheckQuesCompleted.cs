using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;


public class CheckQuesCompleted : MonoBehaviour
{
    public class Flag
    {
        public string flag;
    }

    //Check if the questionnaire is completed
    public IEnumerator CheckQuesStatus()
    {
        //Get the questionnaire status from the server
        IEnumerator getCoroutine = AuthenticationManager.GetQuestionnaireStatus("https://38cc307c-2fe1-4c2f-9187-3335b4f9cf8d.mock.pstmn.io/Flag");
        yield return StartCoroutine(getCoroutine);
        string responseBody = getCoroutine.Current as string;

        //If the response is not null then set the status of the Questionnaire in the player prefs
        if (responseBody != null)
        {
            //Set the status of the Questionnaire in the player prefs
            Flag Response = JsonUtility.FromJson<Flag>(responseBody);
            string QuestionaireCompleted = Response.flag;
            PlayerPrefs.SetString("IsQuestionnaireCompleted", QuestionaireCompleted);   //Set the status of the Questionnaire in the player prefs

            yield return true;
        }
        else
        {
            Debug.Log("Error in authentication ");
            yield return false;
        }
    }


}