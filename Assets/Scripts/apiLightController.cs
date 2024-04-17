using System.Collections;
//using UnityEditor.ShaderGraph.Serialization;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.Networking; // Import the namespace for UnityWebRequest

public class apiLightController : MonoBehaviour
{
    // Serialized field variable for overall illuminance
    private float overallIlluminance = 1.0f; // Initialize with a default value

    // Array to hold references to all the point lights in the scene
    private Light[] pointLights;

    void Start()
    {
        // Find and store references to all the point lights in the scene
        pointLights = FindObjectsOfType<Light>();

        // Start coroutine to fetch overall illuminance from endpoint
        StartCoroutine(GetOverallIlluminance());
    }

    IEnumerator GetOverallIlluminance()
    {
        // URL of the endpoint to fetch overall illuminance
        string url = "http://localhost:8080/questions/sendResults";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Send the GET request
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = webRequest.downloadHandler.text;
                ApiResponse response = JsonUtility.FromJson<ApiResponse>(jsonResponse);
                // Parse the response and set the overall illuminance
                overallIlluminance = response.score * 1.2f;
            }
            else
            {
                Debug.LogError("Failed to fetch overall illuminance: " + webRequest.error);
            }
        }
    }

    void Update()
    {
        // Loop through each point light and adjust its intensity based on the overall illuminance
        foreach (Light light in pointLights)
        {
            // Adjust the intensity of the light based on the overall illuminance
            light.intensity = overallIlluminance;
        }
    }
}

[System.Serializable]
public class ApiResponse
{
    public float score;
    public BootOptions isFinished;
}
