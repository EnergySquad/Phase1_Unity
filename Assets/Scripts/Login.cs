using System.Collections;
using UnityEngine;

public class Login : MonoBehaviour
{
    private const string apiKey = "NjVjNjA0MGY0Njc3MGQ1YzY2MTcyMmM4OjY1YzYwNDBmNDY3NzBkNWM2NjE3MjJiZQ";
    private const string baseUrl = "http://20.15.114.131:8080/api/login";
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
        Debug.Log("authCoroutine: " + authCoroutine);
        string jsonResponse = authCoroutine.Current as string;

        if (jsonResponse != null)
        {
            Debug.Log("Received JSON response: " + jsonResponse);
            AuthResponse authResponse = JsonUtility.FromJson<AuthResponse>(jsonResponse);
            string token = authResponse.token;
            PlayerPrefs.SetString(TokenKey, token);

            //Check the Player's profile details are complete or not
            PDetailesComplete pdetailesComplete = gameObject.AddComponent<PDetailesComplete>();
            IEnumerator playerDetailsCoroutine = pdetailesComplete.AuthenticateAndGetProfile();
            yield return StartCoroutine(playerDetailsCoroutine);
            Debug.Log("playerDetailsCoroutine: " + playerDetailsCoroutine);
            bool IsPlayerDetailsComplete = (bool)playerDetailsCoroutine.Current;

            Debug.Log("IsPlayerDetailsComplete: " + IsPlayerDetailsComplete);

            //If the player details are complete, load the Welcome page else load the Profile page
            if (IsPlayerDetailsComplete)
            {
                sceneLoader.GetComponent<SceneLoader>().LoadNextScene("Ingame");
            }
            else
            {
                sceneLoader.GetComponent<SceneLoader>().LoadProfilePage();
            }


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
