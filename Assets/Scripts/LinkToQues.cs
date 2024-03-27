using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkToQues : MonoBehaviour
{
    public void LinkToQuestions()
    {
        // Load the Questions scene
        Application.OpenURL("https://docs.unity3d.com/ScriptReference/Application.OpenURL.html");
    }
}
