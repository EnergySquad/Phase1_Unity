using System.Collections;
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
}


