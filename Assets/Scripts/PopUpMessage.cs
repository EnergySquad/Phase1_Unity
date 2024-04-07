using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpMessage : MonoBehaviour
{
    public GameObject popUpMessage;

    public void ClickButton()
    {
        popUpMessage.SetActive(true);
    }

    public void CloseButton()
    {
        popUpMessage.SetActive(false);
    }
}
