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

/*using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;

public class Login : MonoBehaviour
{
    // Singleton instance
    private static Login instance;

    // API configuration
    private string apiKey = "NjVjNjA0MGY0Njc3MGQ1YzY2MTcyMmM4OjY1YzYwNDBmNDY3NzBkNWM2NjE3MjJiZQ";
    private string baseUrl = "http://20.15.114.131:8080/api";

    // Scene configuration
    public string gamingSceneName = "Ingame";

    // Token related
    public static Login Instance { get; private set; }
    public string tokenKey { get; private set; } = "JWTToken";
    //private string tokenKey = "JWTToken";

    private void Awake()
    {
        // Ensure Login is a singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
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

        using (UnityWebRequest request = new UnityWebRequest(baseUrl + "/login", "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(requestBody);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                AuthResponse authResponse = JsonUtility.FromJson<AuthResponse>(jsonResponse);
                string token = authResponse.token;
                Debug.Log("JWT Token: " + token);

                // Save token for future use
                SetToken(token);

                LoadGamingScene();
            }
            else
            {
                Debug.LogError("Error: " + request.error);
            }
        }
    }

    private void LoadGamingScene()
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

    public string GetToken()
    {
        return PlayerPrefs.GetString(tokenKey, "");
    }

    private void SetToken(string token)
    {
        PlayerPrefs.SetString(tokenKey, token);
    }

    [Serializable]
    public class AuthResponse
    {
        public string token;
    }
}
*/


/*using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    private string apiKey = "NjVjNjA0MGY0Njc3MGQ1YzY2MTcyMmM4OjY1YzYwNDBmNDY3NzBkNWM2NjE3MjJiZQ";
    private string baseUrl = "http://20.15.114.131:8080/api/login";

    private string gamingSceneName;


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
        GlobalVariablesList globalVariablesList = new GlobalVariablesList();  /////////////////////////
        globalVariablesList.SetIsPlayerDetailsUpdated("isPlayerDetailsUpdated", 0);  /////////////////////////
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
            *//*DisplayText displayText = FindObjectOfType<DisplayText>();
            if (displayText != null)
            {
                displayText.DisplayPlayer();
            }
            else
            {
                Debug.LogError("DisplayText script not found.");
            }*//*
        }
        else
        {
            Debug.LogError("Error: " + request.error);
        }
    }

    private void LoadGamingScene()
    {
        /////////////////////////////
        if (gameObject.GetComponent<GlobalVariablesList>().IsPlayerDetailsUpdated("isPlayerDetailsUpdated") == 1)  /////////////////////////
        {
            gamingSceneName = "Ingame";
        }
        else
        {
            gamingSceneName = "DisplayPDetails";
        }
        /////////////////////////////

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
}*/