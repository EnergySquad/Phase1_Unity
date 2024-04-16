using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //public DisplayText displayTextAccess;
    public void LoadNextScene(string gamingSceneName)
    {
        if (!string.IsNullOrEmpty(gamingSceneName))
        {
            SceneManager.LoadScene(gamingSceneName);
        }
        else
        {
            Debug.LogError("Gaming scene name is not specified!");
        }
    }

    public void LoadProfilePage()
    {
        LoadNextScene("DisplayPDetails");
    }

    public void LoadQuestionnairePage()
    {
        LoadNextScene("Questionnaire");
    }

    public void LoadGame()
    {
        LoadNextScene("Game");
    }

    public void LoadWelcomeWindow()
    {
        LoadNextScene("WelcomePage");
    }
}