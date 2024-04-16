using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void ExitTheGame()
    {
        QuitGame();
    }

    public void QuitGame()
    {
        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
    }
}
