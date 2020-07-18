using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class UIinput : MonoBehaviour
{
    public InputField inputField;
    public MriSequence mriSequence;

    // Start is called before the first frame update
    void Start()
    {
        inputField.placeholder.GetComponent<Text>().text = mriSequence.bedStart.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBedHeightFromInput(string input)
    {
        float i = float.Parse(input.Replace(".", ","));
        mriSequence.SetBedHeight(i);
    }
}
