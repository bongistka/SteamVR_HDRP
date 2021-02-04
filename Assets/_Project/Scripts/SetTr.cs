using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTr : MonoBehaviour
{
    void OnEnable()
    {
        InputField inputField = GetComponent<InputField>();
        MirrorSlideshow MRI_Sequence = FindObjectOfType<MirrorSlideshow>();
        inputField.onEndEdit.AddListener(delegate { MRI_Sequence.SetPauseLengthFromInput(inputField.text); });
        if (MRI_Sequence.trSet)
            inputField.text = MRI_Sequence.secondsToWait.ToString();
    }
}
