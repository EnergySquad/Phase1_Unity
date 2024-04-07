using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public static class AuthenticationManager
{
    public static IEnumerator Authenticate(string baseUrl, string requestBody)
    {
        var request = new UnityWebRequest(baseUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(requestBody);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonResponse = request.downloadHandler.text;
            yield return jsonResponse;
        }
        else
        {
            Debug.LogError("Error: " + request.error);
        }
    }

    public static IEnumerator GetProfile(string apiUrl, string jwtToken)
    {
        UnityWebRequest request = UnityWebRequest.Get(apiUrl);
        request.SetRequestHeader("Authorization", "Bearer " + jwtToken);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            if (request.responseCode == 200)
            {
                string responseBody = request.downloadHandler.text;
                yield return responseBody;
            }
        }
        else
        {
            Debug.LogError("Error fetching profile information: " + request.error);
        }
    }



    public static IEnumerator LinkToQues(string apiUrl)
    {
        UnityWebRequest request = UnityWebRequest.Get(apiUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            if (request.responseCode == 200)
            {
                string responseBody = request.downloadHandler.text;
                yield return responseBody;
            }
        }
        else
        {
            Debug.LogError("Error fetching profile information: " + request.error);
        }
    }



}
