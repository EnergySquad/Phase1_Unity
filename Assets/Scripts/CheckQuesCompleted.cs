using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;


public class CheckQuesCompleted : MonoBehaviour
{
    //public PopUpMessage popUpMsg;
    //public Back welcomePage;

    public class Flag
    {
        public string flag;
    }

    /*public void getQuesStatus()
    {
        StartCoroutine(CheckQuesStatus());
    }*/

    public IEnumerator CheckQuesStatus()
    {
        IEnumerator getCoroutine = AuthenticationManager.LinkToQues("https://38cc307c-2fe1-4c2f-9187-3335b4f9cf8d.mock.pstmn.io/Flag");
        yield return StartCoroutine(getCoroutine);
        string responseBody = getCoroutine.Current as string;

        if (responseBody != null)
        {
            Flag Response = JsonUtility.FromJson<Flag>(responseBody);
            Debug.Log("Response=" + Response);
            string QuestionaireCompleted = Response.flag;
            Debug.Log("result=" + QuestionaireCompleted);
            PlayerPrefs.SetString("IsQuestionnaireCompleted", QuestionaireCompleted);

            yield return true;
        }
        else
        {
            Debug.Log("Error in authentication ");
            yield return false;
        }
    }


}