using System.Collections;
using UnityEngine;

public class Login : MonoBehaviour
{
    private const string apiKey = "NjVjNjA0MGY0Njc3MGQ1YzY2MTcyMmM4OjY1YzYwNDBmNDY3NzBkNWM2NjE3MjJiZQ";
    private const string baseUrl = "http://20.15.114.131:8080/api/login";
    private const string gamingSceneName = "DisplayPDetails";
    public SceneLoader sceneLoader;

    public static Login Instance { get; private set; }
    public static string TokenKey { get; private set; } = "JWTToken";

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
        //Call the Authenticate method from AuthenticationManager
        IEnumerator authCoroutine = AuthenticationManager.Authenticate(baseUrl, requestBody);
        yield return StartCoroutine(authCoroutine);
        string jsonResponse = authCoroutine.Current as string;

        if (jsonResponse != null)
        {
            Debug.Log("Received JSON response: " + jsonResponse);
            AuthResponse authResponse = JsonUtility.FromJson<AuthResponse>(jsonResponse);
            string token = authResponse.token;

            // Save token for future use
            PlayerPrefs.SetString(TokenKey, token);

            // Load the Next scene
            sceneLoader.GetComponent<SceneLoader>().LoadNextScene(gamingSceneName);

        }
        else
        {
            Debug.LogError("Failed to authenticate player!");
        }

    }


    [System.Serializable]
    public class AuthResponse
    {
        public string token;
    }
}