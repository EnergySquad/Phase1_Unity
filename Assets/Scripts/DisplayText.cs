using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour
{
    private string apiUrl = "http://20.15.114.131:8080/api/user/profile/view";
    public Text Name;
    public Text Lastname;
    public Text NIC;
    public Text PhoneNumber;
    public Text Email;
    public Text ProfilePictureUrl;

    [Serializable]
    public class UserProfile
    {
        public string firstname;
        public string lastname;
        public string username;
        public string nic;
        public string phoneNumber;
        public string email;
        public string profilePictureUrl;
    }

    [Serializable]
    public class ProfileResponse
    {
        public UserProfile user;
    }

    public void GetProfileInformation()
    {
        StartCoroutine(GetProfileRequest());
    }

    private IEnumerator GetProfileRequest()
    {
        string jwtToken = PlayerPrefs.GetString("JWTToken", "");
        Debug.Log("tokenInTheDisplay=" + jwtToken);

        UnityWebRequest request = UnityWebRequest.Get(apiUrl);

        // Add Authorization header with JWT token
        request.SetRequestHeader("Authorization", "Bearer " + jwtToken);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            if (request.responseCode == 200) // Check if the response code is OK
            {
                string responseBody = request.downloadHandler.text;
                //Debug.Log("Profile Information: " + responseBody);
                // Deserialize the JSON response
                ProfileResponse profileResponse = JsonUtility.FromJson<ProfileResponse>(request.downloadHandler.text);

                // Display player details
                Debug.Log("Name: " + profileResponse.user.firstname + " " + profileResponse.user.lastname);
                Debug.Log("Username: " + profileResponse.user.username);
                Debug.Log("NIC: " + profileResponse.user.nic);
                Debug.Log("Phone Number: " + profileResponse.user.phoneNumber);
                Debug.Log("Email: " + profileResponse.user.email);
                Debug.Log("Profile Picture URL: " + profileResponse.user.profilePictureUrl);

                Name.text = profileResponse.user.firstname ;
                Lastname.text = profileResponse.user.lastname;
                NIC.text = profileResponse.user.nic;
                PhoneNumber.text = profileResponse.user.phoneNumber;
                Email.text = profileResponse.user.email;
                ProfilePictureUrl.text = profileResponse.user.profilePictureUrl;



            }
            else
            {
                Debug.LogError("Error fetching profile information. Response code: " + request.responseCode);
            }
        }
        else
        {
            Debug.LogError("Error fetching profile information: " + request.error);
        }
    }
}