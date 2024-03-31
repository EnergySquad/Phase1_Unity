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

    public void DisplayPlayerDetails()
    {
        StartCoroutine(AuthenticateAndGetProfile());
    }


    private IEnumerator AuthenticateAndGetProfile()
    {
        string jwtToken = PlayerPrefs.GetString("JWTToken", "");
        Debug.Log("tokenInTheDisplay=" + jwtToken);

        IEnumerator getCoroutine = AuthenticationManager.GetProfile(apiUrl, jwtToken);
        yield return StartCoroutine(getCoroutine);
        string responseBody = getCoroutine.Current as string;

        if (responseBody != null)
        {
            ProfileResponse profileResponse = JsonUtility.FromJson<ProfileResponse>(responseBody);
            Name.text = profileResponse.user.firstname;
            Lastname.text = profileResponse.user.lastname;
            NIC.text = profileResponse.user.nic;
            PhoneNumber.text = profileResponse.user.phoneNumber;
            Email.text = profileResponse.user.email;
            ProfilePictureUrl.text = profileResponse.user.profilePictureUrl;

            Debug.Log("Name: " + profileResponse.user.firstname + " " + profileResponse.user.lastname);
            Debug.Log("Username: " + profileResponse.user.username);
            Debug.Log("NIC: " + profileResponse.user.nic);
            Debug.Log("Phone Number: " + profileResponse.user.phoneNumber);
            Debug.Log("Email: " + profileResponse.user.email);
            Debug.Log("Profile Picture URL: " + profileResponse.user.profilePictureUrl);
        }
        else
        {
            Debug.LogError("Error fetching profile information.");
        }
    }
}