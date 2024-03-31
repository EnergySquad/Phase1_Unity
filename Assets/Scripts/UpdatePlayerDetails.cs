//correct one:-
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UpdatePlayerDetails : MonoBehaviour
{
    public string gamingSceneName = "Ingame"; // Name of the gaming scene to load
    public InputText inputTextScript; // Reference to your InputText script
    private string url = "http://20.15.114.131:8080/api/user/profile/update";
    public DisplayText displayText; 

    public void UploadDetails()
    {
        StartCoroutine(Upload());
    }

    IEnumerator Upload()
    {
        string jwtToken = PlayerPrefs.GetString("JWTToken", "");
        // Get the input details from InputText script
        List<string> inputDetails = inputTextScript.InputList;

        // Construct JSON data using input details
        string jsonData = "{\"firstname\": \"" + inputDetails[0] + "\", " +
                          "\"lastname\": \"" + inputDetails[1] + "\", " +
                          "\"nic\": \"" + inputDetails[2] + "\", " +
                          "\"phoneNumber\": \"" + inputDetails[3] + "\", " +
                          "\"email\": \"" + inputDetails[4] + "\", " +
                          "\"profilePictureUrl\": \"" + inputDetails[5] + "\"}";

        Debug.Log("jsonData=" + jsonData);

        // Convert JSON data to byte array
        byte[] myData = System.Text.Encoding.UTF8.GetBytes(jsonData);

        // Create UnityWebRequest for PUT request
        UnityWebRequest request = UnityWebRequest.Put(url, myData);
        request.method = UnityWebRequest.kHttpVerbPUT;
        //www.method = UnityWebRequest.kHttpVerbPUT;
        request.SetRequestHeader("accept", "*/*");
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + jwtToken);
        
        // Send the request
        yield return request.SendWebRequest();

        // Check if the request was successful
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else 
        {
            //clear the current input fields
            inputTextScript.ClearInputFields();

            displayText.GetComponent<DisplayText>().DisplayPlayerDetails();

            Debug.Log("Profile update successful!");
        }
    }
}
