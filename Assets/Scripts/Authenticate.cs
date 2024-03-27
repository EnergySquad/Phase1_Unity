using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using UnityEditor.PackageManager.Requests;

namespace Authenticator
{
    public class Authenticate : MonoBehaviour
    {
        private string jwtToken = PlayerPrefs.GetString("JWTToken", "");
        public bool IsRequestCompleted { get; private set; }
        public bool HasError { get; private set; }
        public string ErrorMessage { get; private set; }

        public void AuthRequest(string url, string data, string method)
        {
            StartCoroutine(SendRequest(url, data, method));
        }

        private IEnumerator SendRequest(string url, string data, string method)
        {
            UnityWebRequest request;

            switch (method)
            {
                case "PUT":
                    byte[] requestData = System.Text.Encoding.UTF8.GetBytes(data);
                    request = UnityWebRequest.Put(url, requestData);
                    break;
                case "GET":
                    request = UnityWebRequest.Get(url);
                    break;
                default:
                    Debug.LogError("Unsupported request method: " + method);
                    yield break;
            }

            request.method = method;
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + jwtToken);

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                HasError = true;
                ErrorMessage = request.error;
            }
            else
            {
                IsRequestCompleted = true;
                //ProfileResponse ProfileResponse = request.downloadHandler.text;
            }
        }
        /*public T GetProfileResponse<T>()
        {
            // Deserialize the JSON response to the specified type
            return JsonUtility.FromJson<T>(request.downloadHandler.text);
        }*/
    }
}

