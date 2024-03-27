//correct one:-
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    private string apiKey = "NjVjNjA0MGY0Njc3MGQ1YzY2MTcyMmM4OjY1YzYwNDBmNDY3NzBkNWM2NjE3MjJiZQ";
    private string baseUrl = "http://20.15.114.131:8080/api/login";

    private string gamingSceneName = "DisplayPDetails";


    //private string TokenKey;
    public static Login Instance { get; private set; }
    public string TokenKey { get; private set; } = "JWTToken";

    private void Awake()
    {
        // Ensure there's only one instance of Login
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AuthenticatePlayer()
    {
       StartCoroutine(AuthenticatePlayerCoroutine());
    }


    private IEnumerator AuthenticatePlayerCoroutine()
    {
        string requestBody = "{\"apiKey\": \"" + apiKey + "\"}";

        var request = new UnityWebRequest(baseUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(requestBody);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        Debug.Log("Response Code: " + request.responseCode);

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonResponse = request.downloadHandler.text;
            AuthResponse authResponse = JsonUtility.FromJson<AuthResponse>(jsonResponse);
            string token = authResponse.token;
            Debug.Log("JWT Token: " + token);

            // Save token for future use
            PlayerPrefs.SetString(TokenKey, token);

            LoadGamingScene();

        }
        else
        {
            Debug.LogError("Error: " + request.error);
        }
    }

    private void LoadGamingScene()
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

    public string GetSavedToken()
    {
        return PlayerPrefs.GetString(TokenKey, "");
    }

    [System.Serializable]
    public class AuthResponse
    {
        public string token;
    }
}