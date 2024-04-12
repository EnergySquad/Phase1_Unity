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
        //Get the questionnaire status
        CheckQuesCompleted QuesCoroutine = gameObject.AddComponent<CheckQuesCompleted>();
        IEnumerator IsQuesCompleteCoroutine = QuesCoroutine.CheckQuesStatus();
        yield return StartCoroutine(IsQuesCompleteCoroutine);
        bool response = (bool)IsQuesCompleteCoroutine.Current;
        Debug.Log("response: " + response);

        if (response)
        {
            string IsQuestionaireCompleted = PlayerPrefs.GetString("IsQuestionnaireCompleted");

            //If the questionnaire is completed, then go back to the welcome page
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
                    LinkToQuestions();      //Link to the Questions page
                }
                else
                {
                    popUpMsg.GetComponent<PopUpMessage>().ClickButton();    //Show the pop-up message if the player clicks the button again without completing the questionnaire
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