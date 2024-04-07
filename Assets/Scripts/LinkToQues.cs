using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;


public class LinkToQues : MonoBehaviour
{
    private int NoOfClicks = 0;
    public PopUpMessage popUpMsg;
    public NavigationCommands welcomePage;

    public class Flag
    {
        public string flag;
    }

    public void QuestionnairePage()
    {
        StartCoroutine(LinkToQuestionnaire());
    }

    public IEnumerator LinkToQuestionnaire()
    {
        /*IEnumerator QuesCoroutine = CheckQuesCompleted.CheckQuesStatus();
        yield return StartCoroutine(getCoroutine);
        string responseBody = getCoroutine.Current as string;*/
        CheckQuesCompleted QuesCoroutine = gameObject.AddComponent<CheckQuesCompleted>();
        IEnumerator IsQuesCompleteCoroutine = QuesCoroutine.CheckQuesStatus();
        yield return StartCoroutine(IsQuesCompleteCoroutine);
        bool response = (bool)IsQuesCompleteCoroutine.Current;
        Debug.Log("response: " + response);

        if (response)
        {
            string IsQuestionaireCompleted = PlayerPrefs.GetString("IsQuestionnaireCompleted");

            if (IsQuestionaireCompleted == "True")
            {
                Debug.Log("Error Linking to Questions ");
                welcomePage.GetComponent<NavigationCommands>().BackToWelcomePage();
            }
            else
            {
                if (NoOfClicks == 0)
                {
                    NoOfClicks++;
                    Debug.Log("Linking to Questions");
                    LinkToQuestions();
                }
                else
                {
                    popUpMsg.GetComponent<PopUpMessage>().ClickButton();
                }

            }
        }
        else
        {
            Debug.Log("Error in authentication ");
        }
    }

    public void LinkToQuestions()
    {
        // Load the Questions scene
        Application.OpenURL("https://docs.unity3d.com/ScriptReference/Application.OpenURL.html");
    }
}


//https://38cc307c-2fe1-4c2f-9187-3335b4f9cf8d.mock.pstmn.io/Flag