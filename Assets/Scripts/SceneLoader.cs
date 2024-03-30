using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene(string gamingSceneName)
    {
        if (!string.IsNullOrEmpty(gamingSceneName))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(gamingSceneName);
        }
        else
        {
            Debug.LogError("Gaming scene name is not specified!");
        }
    }
}