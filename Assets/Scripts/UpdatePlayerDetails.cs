/*using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class UpdatePlayerDetails : MonoBehaviour
{
    public Text responseText;

    //private string jwtToken;
    private string url = "http://20.15.114.131:8080/api/user/profile/update";

    //void Start()
    //{
        // Get the JWT token from PlayerPrefs
        //jwtToken = PlayerPrefs.GetString("JWTToken", "");

      //  StartCoroutine(UpdateProfile());
    //}
    public void UpdateProfileDetails()
    {
        InputText inputText = GetComponent<InputText>();
        if (inputText != null)
        {
            List<string> inputList = inputText.InputList;
            Debug.Log("inputList=" + inputList);

            // Use inputList to construct the JSON request body and update the profile
            StartCoroutine(UpdateProfile(inputList));
        }
        else
        {
            Debug.LogError("InputText component not found!");
        }
    }

    private IEnumerator UpdateProfile(List<string> inputList)
    {
        
        string jwtToken = PlayerPrefs.GetString("JWTToken", "");
        // Create the JSON request body with the field to update and its new value
        string requestBody = "{\"firstname\": \"" + inputList[0] + "\", " +
                          "\"lastname\": \"" + inputList[1] + "\", " +
                          "\"nic\": \"" + inputList[2] + "\", " +
                          "\"phoneNumber\": \"" + inputList[3] + "\", " +
                          "\"email\": \"" + inputList[4] + "\", " +
                          "\"profilePictureUrl\": \"" + inputList[5] + "\"}";

        Debug.Log("requestBody=" + requestBody);

        // Create a PUT request with the API endpoint and add the JWT token to the header
        UnityWebRequest request = new UnityWebRequest(url, "PUT");
        request.SetRequestHeader("Authorization", "Bearer " + jwtToken);
        request.SetRequestHeader("Content-Type", "application/json");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(requestBody);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        // Send the request
        yield return request.SendWebRequest();

        // Check for errors and handle the response
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("***************updated successfully!********************");
            responseText.text = "Profile field updated successfully!";
        }
        else
        {
            Debug.LogError("Error updating profile field: " + request.error);
            responseText.text = "Error updating profile field: " + request.error;
        }
    }
}*/

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

            displayText.GetComponent<DisplayText>().GetProfileInformation();

            Debug.Log("Profile update successful!");
        }
    }
}
