using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableButton : MonoBehaviour
{
    public Button buttonToDisable;

    public void DoDisableButton()
    {
        buttonToDisable.interactable = false;
    }
}
