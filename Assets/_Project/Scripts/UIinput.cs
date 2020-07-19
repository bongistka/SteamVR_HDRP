using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class UIinput : MonoBehaviour
{
    public InputField inputField;
    public MriSequence mriSequence;
    public GameObject controlPanel;
    public GameObject togglePanel;
    public Button toggleButton;
    private bool mainPanelVisible = false;

    // Start is called before the first frame update
    void Start()
    {
        inputField.placeholder.GetComponent<Text>().text = mriSequence.bedStart.ToString();
        ToggleMainPanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBedHeightFromInput(string input)
    {
        float i = float.Parse(input, CultureInfo.InvariantCulture);
        mriSequence.SetBedHeight(i);
    }

    public void ToggleMainPanel()
    {
        controlPanel.SetActive(mainPanelVisible);
        if (!mainPanelVisible)
        {
            Vector3 pos = togglePanel.GetComponent<RectTransform>().anchoredPosition;
            pos.x = -5;
            togglePanel.GetComponent<RectTransform>().anchoredPosition = pos;
            toggleButton.GetComponentInChildren<Text>().text = "<";
        }
        else
        {
            Vector3 pos = togglePanel.GetComponent<RectTransform>().anchoredPosition;
            pos.x = -195;
            togglePanel.GetComponent<RectTransform>().anchoredPosition = pos;
            toggleButton.GetComponentInChildren<Text>().text = ">";
        }
        mainPanelVisible = !mainPanelVisible;
    }
}
