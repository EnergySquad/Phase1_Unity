/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputText : MonoBehaviour
{
    string inputDetail;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadInput(string input)
    {
        inputDetail = input;
        Debug.Log(inputDetail);
    }
}
*/

/*using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

public class InputText : MonoBehaviour
{
    public InputField firstNameField;
    public InputField lastNameField;
    public InputField nicField;
    public InputField phoneNumberField;
    public InputField emailField;
    public InputField profilePictureUrlField;

    private string jwtToken;

    void Start()
    {
        // Get the JWT token from PlayerPrefs
        jwtToken = PlayerPrefs.GetString("JWTToken", "");
        StartCoroutine(UpdateProfile());
    }

    private IEnumerator UpdateProfile()
    {
        // Construct JSON data
        string jsonData = "{\"firstname\": \"" + firstNameField.text + "\", " +
                          "\"lastname\": \"" + lastNameField.text + "\", " +
                          "\"nic\": \"" + nicField.text + "\", " +
                          "\"phoneNumber\": \"" + phoneNumberField.text + "\", " +
                          "\"email\": \"" + emailField.text + "\", " +
                          "\"profilePictureUrl\": \"" + profilePictureUrlField.text + "\"}";

        // Create the request
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://20.15.114.131:8080/api/user/profile/update");
        request.Method = "PUT";
        request.ContentType = "application/json";
        request.Headers.Add("Authorization", "Bearer " + jwtToken);

        // Write JSON data to request body
        using (StreamWriter streamWriter = new StreamWriter(request.GetRequestStream()))
        {
            streamWriter.Write(jsonData);
            streamWriter.Flush();
            streamWriter.Close();
        }

        // Get the response
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();

        Debug.Log("Update Profile Response: " + jsonResponse);
    }
}
*/

/*using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class InputText : MonoBehaviour
{
    public InputField firstNameField;
    public InputField lastNameField;
    public InputField nicField;
    public InputField phoneNumberField;
    public InputField emailField;
    public InputField profilePictureUrlField;

    //void Start()
    //{
        // Get the JWT token from PlayerPrefs
        //jwtToken = PlayerPrefs.GetString("JWTToken", "");

      //  StartCoroutine(UpdateProfile());
    //}
    public List<string> InputList { get; private set; } = new List<string>();
    //List<string> InputList = new List<string>();

    public void StoreInputs()
    {
        InputList.Add(firstNameField.text);
        InputList.Add(lastNameField.text);
        InputList.Add(nicField.text);
        InputList.Add(phoneNumberField.text);
        InputList.Add(emailField.text);
        InputList.Add(profilePictureUrlField.text);

        //call to DisInputDetails method
        DisInputDetails();

    }

    //public void UpdateProfileDetails()
    //{
      //  StartCoroutine(UpdateProfile());
    //}

    public List<string> DisInputDetails() 
    {
        return InputList; 
    }
}
*/

//correct code:-
/*using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InputText : MonoBehaviour
{
    public List<string> InputList { get; private set; } = new List<string>();

    public void StoreInputs()
    {
        InputList.Clear();
        InputField[] inputFields = GetComponentsInChildren<InputField>();

        foreach (InputField inputField in inputFields)
        {
            InputList.Add(inputField.text);
        }

        // Call to DisplayInputDetails method
        DisplayInputDetails();
    }

    public void DisplayInputDetails()
    {
        for (int i = 0; i < InputList.Count; i++)
        {
            Debug.Log("Input " + i + ": " + InputList[i]);
        }
    }
}*/


using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InputText : MonoBehaviour
{
    public List<string> InputList { get; private set; } = new List<string>();

    public DisplayText displayText; // Reference to the DisplayText script
    public UpdatePlayerDetails updatePlayerDetails; // Reference to the UpdatePlayerDetails script

    public void StoreInputs()
    {
        InputList.Clear();
        InputField[] inputFields = GetComponentsInChildren<InputField>();

        foreach (InputField inputField in inputFields)
        {
            if (string.IsNullOrEmpty(inputField.text))
            {
                Debug.Log("Input field("+ inputField.name + ")is empty. Taking value from DisplayText script.");
                // Get the input field name and take the relevant value from DisplayText.cs
                switch (inputField.name)
                {
                    case "FirstNameInputField":
                        InputList.Add(displayText.Name.text);
                        break;
                    case "SecondNameInputField":
                        InputList.Add(displayText.Lastname.text);
                        break;
                    case "NICInputField":
                        InputList.Add(displayText.NIC.text);
                        break;
                    case "PhoneNumberInputField":
                        InputList.Add(displayText.PhoneNumber.text);
                        break;
                    case "EmailInputField":
                        InputList.Add(displayText.Email.text);
                        break;
                    case "ProfilePictureUrlInputField":
                        InputList.Add(displayText.ProfilePictureUrl.text);
                        break;
                    default:
                        // Handle other input fields if needed
                        break;
                }
            }
            else
            {
                InputList.Add(inputField.text);
            }
        }

        // Call to DisplayInputDetails method
        DisplayInputDetails();

        // Call the UpdatePlayerDetails script to update the player details
        updatePlayerDetails.GetComponent<UpdatePlayerDetails>().UploadDetails();
    }

    public void DisplayInputDetails()
    {
        for (int i = 0; i < InputList.Count; i++)
        {
            Debug.Log("Input " + i + ": " + InputList[i]);
        }
    }

    public void ClearInputFields()
    {
        InputField[] inputFields = GetComponentsInChildren<InputField>();
        foreach (InputField inputField in inputFields)
        {
            inputField.text = "";
        }
    }
}
