using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameAchievements : MonoBehaviour
{
    public Text results;
    public PopUpMessage popUpScore;
    private string Achievement;

    public class Score
    {
        public string flag;
        public string score;
    }

    public void GetAchievements()
    {
        StartCoroutine(getAchievements());
    }

    public IEnumerator getAchievements()
    {
        // Load the Achievements scene
        IEnumerator getCoroutine = AuthenticationManager.GetQuestionnaireStatus("https://38cc307c-2fe1-4c2f-9187-3335b4f9cf8d.mock.pstmn.io/Flag");
        yield return StartCoroutine(getCoroutine);
        string responseBody = getCoroutine.Current as string;

        if (responseBody != null)
        {
            Score Response = JsonUtility.FromJson<Score>(responseBody);
            Debug.Log("Response=" + Response);
            string StringScore = Response.score;
            float Score;
            if (float.TryParse(StringScore, out Score))
            {
                Debug.Log("Parsed float value: " + Score);
            }
            else
            {
                Debug.LogError("Error parsing float.");
            }
            if (Score > 0.7)
            {
                popUpScore.GetComponent<PopUpMessage>().ClickButton();
                Achievement = "Early Striker";
                results.text = Achievement;
            }
        }
        else
        {
            Debug.Log("Error in authentication ");
        }
    }
}
