/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationCommands : MonoBehaviour
{
    public SceneLoader sceneLoader;

    public void BackToWelcomePage()
    {
        StartCoroutine(LoadWelcomePage());
    }

    private IEnumerator LoadWelcomePage()
    {
        PDetailesComplete pdetailesComplete = gameObject.AddComponent<PDetailesComplete>();
        IEnumerator playerDetailsCoroutine = pdetailesComplete.AuthenticateAndGetProfile();
        yield return StartCoroutine(playerDetailsCoroutine);
        Debug.Log("playerDetailsCoroutine: " + playerDetailsCoroutine);
        bool IsPlayerDetailsComplete = (bool)playerDetailsCoroutine.Current;

        Debug.Log("IsPlayerDetailsComplete: " + IsPlayerDetailsComplete);

        if (IsPlayerDetailsComplete)
        {
            sceneLoader.GetComponent<SceneLoader>().LoadNextScene("Ingame");
        }
        else
        {
            sceneLoader.GetComponent<SceneLoader>().LoadProfilePage();
        }
    }

    public void GoToProfilePage()
    {
        sceneLoader.GetComponent<SceneLoader>().LoadProfilePage();
    }

    public void Continue()
    {
        sceneLoader.GetComponent<SceneLoader>().LoadNextScene("Questionnaire");
    }
}*/


///////////////////////////////////////////////////////////////////////////////
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationCommands : MonoBehaviour
{
    public SceneLoader sceneLoader;
    public CheckQuesCompleted linkToQues;

    public void BackToWelcomePage()
    {
        StartCoroutine(LoadWelcomePage());
    }

    private IEnumerator LoadWelcomePage()
    {
        //Check if the player details are complete
        PDetailesComplete pdetailesComplete = gameObject.AddComponent<PDetailesComplete>();
        IEnumerator playerDetailsCoroutine = pdetailesComplete.AuthenticateAndGetProfile();
        yield return StartCoroutine(playerDetailsCoroutine);
        Debug.Log("playerDetailsCoroutine: " + playerDetailsCoroutine);
        bool IsPlayerDetailsComplete = (bool)playerDetailsCoroutine.Current;

        Debug.Log("IsPlayerDetailsComplete: " + IsPlayerDetailsComplete);

        if (IsPlayerDetailsComplete)
        {
            sceneLoader.GetComponent<SceneLoader>().LoadNextScene("Ingame");
        }
        else
        {
            GoToProfilePage();
        }
    }

    public void GoToProfilePage()
    {
        sceneLoader.GetComponent<SceneLoader>().LoadProfilePage();
    }

    /*public void Continue()
    {

        string QFlag = PlayerPrefs.GetString("IsQuestionnaireCompleted");
        if (QFlag == "True")
        {
            sceneLoader.GetComponent<SceneLoader>().LoadNextScene("Ingame");
        }
        else
        {
            sceneLoader.GetComponent<SceneLoader>().LoadNextScene("Questionnaire");
        }
    }*/

    public void Exit()
    {
        BackToWelcomePage();
    }

    public void Continue()
    {
        StartCoroutine(ContinueGame());
    }

    private IEnumerator ContinueGame()
    {
        //Check if the questionnaire is complete
        CheckQuesCompleted quesDetails = gameObject.AddComponent<CheckQuesCompleted>();
        IEnumerator QuestionnaireCoroutine = quesDetails.CheckQuesStatus();
        yield return StartCoroutine(QuestionnaireCoroutine);
        /*Debug.Log("playerDetailsCoroutine: " + QuestionnaireCoroutine);
        bool IsQuestionnaireCompleted = (bool)QuestionnaireCoroutine.Current;*/
        bool response = (bool)QuestionnaireCoroutine.Current;
        Debug.Log("response: " + response);

        string IsQuestionnaireCompleted = PlayerPrefs.GetString("IsQuestionnaireCompleted");
        Debug.Log("IsQuestionnaireCompleted: " + IsQuestionnaireCompleted);

        if (IsQuestionnaireCompleted == "True")
        {
            sceneLoader.GetComponent<SceneLoader>().LoadNextScene("Game");
        }
        else
        {
            sceneLoader.GetComponent<SceneLoader>().LoadNextScene("Questionnaire");
        }
    }

    public void LinkToQuestions()
    {
        // Load the Questions scene
        Application.OpenURL("https://docs.unity3d.com/ScriptReference/Application.OpenURL.html");
    }
}
