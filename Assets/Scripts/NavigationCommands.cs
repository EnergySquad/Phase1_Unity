using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationCommands : MonoBehaviour
{
    public SceneLoader sceneLoader;
    public CheckQuesCompleted linkToQues;
    private string currentSceneName;
    public PopUpMessage popUpMsg;

    /*public void BackToWelcomePage()
    {
        StartCoroutine(LoadWelcomePage());
    }*/
    public void GoToWelcomePage(string currentScene)
    {
        if (currentScene == "Login" || currentScene == "DisplayPDetails")
        {
            Debug.Log("currentScene1: " + currentScene);
            StartCoroutine(LoadWelcomePage());
        }
        else
        {
            sceneLoader.GetComponent<SceneLoader>().LoadWelcomeWindow();
        }
    }

    //Load the welcome page if the player details are complete
    /*private IEnumerator LoadWelcomePage()
    {
        //Check if the player details are complete
        PDetailesComplete pdetailesComplete = gameObject.AddComponent<PDetailesComplete>();
        IEnumerator playerDetailsCoroutine = pdetailesComplete.AuthenticateAndGetProfile();
        yield return StartCoroutine(playerDetailsCoroutine);
        Debug.Log("playerDetailsCoroutine: " + playerDetailsCoroutine);
        bool IsPlayerDetailsComplete = (bool)playerDetailsCoroutine.Current;

        Debug.Log("IsPlayerDetailsComplete: " + IsPlayerDetailsComplete);

        //If the player details are complete, load the welcome page
        if (IsPlayerDetailsComplete)
        {
            sceneLoader.GetComponent<SceneLoader>().LoadWelcomeWindow();
        }
        else
        {
            GoToProfilePage();
        }
    }*/

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
            sceneLoader.GetComponent<SceneLoader>().LoadWelcomeWindow();
        }
        else
        {
            currentSceneName = SceneManager.GetActiveScene().name;
            Debug.Log("CurrentScene = " + currentSceneName);
            if (currentSceneName == "DisplayPDetails")
            {
                popUpMsg.GetComponent<PopUpMessage>().ClickButton();
            }
            else
            {
                GoToProfilePage();
            }
        }
    }

    //When the player wants to go to the profile page
    public void GoToProfilePage()
    {
        sceneLoader.GetComponent<SceneLoader>().LoadProfilePage();
    }

    //When the player wants to exit the game it directs to the welcome page
    public void Exit()
    {
        //BackToWelcomePage();
        GoToWelcomePage("Game");
    }

    //When the player wants to continue the game
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
        /*bool response = (bool)QuestionnaireCoroutine.Current;
        Debug.Log("response: " + response);*/

        string IsQuestionnaireCompleted = PlayerPrefs.GetString("IsQuestionnaireCompleted");
        Debug.Log("IsQuestionnaireCompleted: " + IsQuestionnaireCompleted);

        //If the questionnaire is complete, load the game
        if (IsQuestionnaireCompleted == "True")
        {
            sceneLoader.GetComponent<SceneLoader>().LoadGame();
        }
        else
        {
            sceneLoader.GetComponent<SceneLoader>().LoadQuestionnairePage();
        }
    }
}
