/*using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using UnityEngine.UI;
using Unity.IO.LowLevel.Unsafe;

public class PlayerProfileDisplay : MonoBehaviour
{
    string apiUrl = "http://20.15.114.131:8080/api/user/profile/view";

    //string jwtToken = "eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJvdmVyc2lnaHRfZzMiLCJpYXQiOjE3MTA1NzExMTMsImV4cCI6MTcxMDYwNzExM30.MG3fTRQqsrCQZm0gstbE6A338i_DO3jeSbv28sNC66pdDxOHkXAIIkbN1V0K8nt2Bl92in3pA0TZzS-5VRlfhA"; // Assign your JWT token here

    public Text Name;
    public Text Username;
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

    void Start()
    {
        StartCoroutine(GetPlayerProfile());
    }

    IEnumerator GetPlayerProfile()
    {
        UnityWebRequest request = UnityWebRequest.Get(apiUrl);
        string jwtToken = PlayerPrefs.GetString("JWTToken", "");
        Debug.Log("tokenInTheDisplay="+jwtToken);

        // Add JWT token to request headers
        request.SetRequestHeader("Authorization", "Bearer " + jwtToken);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            // Deserialize the JSON response
            ProfileResponse profileResponse = JsonUtility.FromJson<ProfileResponse>(request.downloadHandler.text);

            // Display player details
            Debug.Log("Name: " + profileResponse.user.firstname + " " + profileResponse.user.lastname);
            Debug.Log("Username: " + profileResponse.user.username);
            Debug.Log("NIC: " + profileResponse.user.nic);
            Debug.Log("Phone Number: " + profileResponse.user.phoneNumber);
            Debug.Log("Email: " + profileResponse.user.email);
            Debug.Log("Profile Picture URL: " + profileResponse.user.profilePictureUrl);

            Name.text = "Name: " + profileResponse.user.firstname + " " + profileResponse.user.lastname;
            Username.text = "Username: " + profileResponse.user.username;
            NIC.text = "NIC: " + profileResponse.user.nic;
            PhoneNumber.text = "Phone Number: " + profileResponse.user.phoneNumber;
            Email.text = "Email: " + profileResponse.user.email;
            ProfilePictureUrl.text = "Profile Picture URL: " + profileResponse.user.profilePictureUrl;
        }
    }

}
*/


using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;
using UnityEngine.UI;
//using Authenticator;

public class DisplayText : MonoBehaviour
{
    private string apiUrl = "http://20.15.114.131:8080/api/user/profile/view";
    //private string jwtToken = "eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJvdmVyc2lnaHRfZzMiLCJpYXQiOjE3MTA2NTA0NTYsImV4cCI6MTcxMDY4NjQ1Nn0.-dxC_JUKSNSPvzcOh0Y0q5sAOWIiZTsG-fJeB-0T4JLfTg7QPvZDT7ONpAh0FCbhxOdmG7Z_ivO8kQLzYJdCfA";
    public Text Name;
    public Text Lastname;
    public Text NIC;
    public Text PhoneNumber;
    public Text Email;
    public Text ProfilePictureUrl;
    //Authenticator.Äuthenticate authentication;

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

                /*Name.text = "Name: " + profileResponse.user.firstname + " " + profileResponse.user.lastname;
                Username.text = "Username: " + profileResponse.user.username;
                NIC.text = "NIC: " + profileResponse.user.nic;
                PhoneNumber.text = "Phone Number: " + profileResponse.user.phoneNumber;
                Email.text = "Email: " + profileResponse.user.email;
                ProfilePictureUrl.text = "Profile Picture URL: " + profileResponse.user.profilePictureUrl;
*/
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


/*using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;
using UnityEngine.UI;
using Authenticator; // Import the Authenticator namespace

public class DisplayText : MonoBehaviour
{
    private string apiUrl = "http://20.15.114.131:8080/api/user/profile/view";
    public Text Name;
    public Text Username;
    public Text NIC;
    public Text PhoneNumber;
    public Text Email;
    public Text ProfilePictureUrl;

    public void GetProfileInformation()
    {
        Authenticate authenticator = new Authenticate(); // Create an instance of Authenticate class

        authenticator.AuthRequest(apiUrl, "", "GET"); // Call the AuthRequest function from Authenticate

        StartCoroutine(GetProfileResponse(authenticator)); // Start coroutine to handle profile response
    }

    private IEnumerator GetProfileResponse(Authenticate authenticator)
    {
        while (!authenticator.IsRequestCompleted) // Wait until the request is completed
        {
            yield return null;
        }

        if (authenticator.HasError) // Check if there was an error in the request
        {
            Debug.LogError("Error fetching profile information: " + authenticator.ErrorMessage);
            yield break;
        }

        // Get the profile response from Authenticate
        ProfileResponse profileResponse = authenticator.GetProfileResponse<ProfileResponse>();

        // Display player details
        Name.text = "Name: " + profileResponse.user.firstname + " " + profileResponse.user.lastname;
        Username.text = "Username: " + profileResponse.user.username;
        NIC.text = "NIC: " + profileResponse.user.nic;
        PhoneNumber.text = "Phone Number: " + profileResponse.user.phoneNumber;
        Email.text = "Email: " + profileResponse.user.email;
        ProfilePictureUrl.text = "Profile Picture URL: " + profileResponse.user.profilePictureUrl;
    }
}
*/