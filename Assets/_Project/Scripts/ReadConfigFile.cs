using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ReadConfigFile : MonoBehaviour
{
    public MriSequence mriSequence;
    public MirrorSlideshow mirrorSlideshow;
    public InputField trInputField;
    public InputField bedHeightField;

    public Dropdown configDropdown;

    public SequenceClip lokalizerClip;
    public SequenceClip morfologieClip;
    public SequenceClip restingClip;
    public SequenceClip paradigmaClip;
    public SequenceClip dtiClip;

    private bool morfologieOn;
    private bool restingOn;
    private bool paradigmaOn;
    private bool dtiOn;

    // Start is called before the first frame update
    void Start()
    {
        InitializeConfigMenu();
        //ReadTextFile(Application.dataPath + "/../Configs/" + "default.txt");
    }

    void InitializeConfigMenu()
    {
        string[] filePaths = Directory.GetFiles(Application.dataPath + "/../Configs/", "*.txt");
        foreach (string c in filePaths)
        {
            string fileName = System.IO.Path.GetFileName(c);
            configDropdown.options.Add(new Dropdown.OptionData() { text = fileName });
        }
    }

    public void ReadMenu()
    {
        if(configDropdown.options[configDropdown.value].text!= "Vyberte konfiguraci")
            ReadTextFile(Application.dataPath + "/../Configs/" + configDropdown.options[configDropdown.value].text);
    }

    void ReadTextFile(string file_path)
    {
        StreamReader inp_stm = new StreamReader(file_path, System.Text.Encoding.UTF8);

        while (!inp_stm.EndOfStream)
        {
            string inp_ln = inp_stm.ReadLine();
            if (inp_ln.Contains("#BedHeight: "))
            {
                bedHeightField.text = inp_ln.Replace("#BedHeight: ", "");
                float.TryParse(bedHeightField.text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out float bedHeight);
                
                if(bedHeight < 1.041462)
                {
                    mriSequence.SetBedHeight(bedHeight);
                }
            }
            if (inp_ln.Contains("#TR: "))
            {
                trInputField.text = inp_ln.Replace("#TR: ", "");
                mirrorSlideshow.SetPauseLengthFromInput(trInputField.text);
            }
            if (inp_ln.Contains("#FolderName: "))
            {
                mirrorSlideshow.folderName = inp_ln.Replace("#FolderName: ", "");
            }
            if (inp_ln.Contains("#Lokalizer: "))
            {
                bool isOn = bool.Parse(inp_ln.Replace("#Lokalizer: ", ""));
                if(isOn)
                    lokalizerClip.AddClipToProtocol();
            }
            if (inp_ln.Contains("#Morfologie: "))
            {
                morfologieOn = bool.Parse(inp_ln.Replace("#Morfologie: ", ""));
            }
            if (inp_ln.Contains("#Morfologie delka [s]: "))
            {
                morfologieClip.SetDuration(inp_ln.Replace("#Morfologie delka [s]: ", ""));
            }
            if (inp_ln.Contains("#Morfologie protokol: "))
            {
                if (inp_ln.Replace("#Morfologie protokol: ", "") == "T1")
                    morfologieClip.SetType(0);
                else
                    morfologieClip.SetType(1);
                if (morfologieOn)
                    morfologieClip.AddClipToProtocol();
            }
            if (inp_ln.Contains("#Resting: "))
            {
                restingOn = bool.Parse(inp_ln.Replace("#Resting: ", ""));
            }
            if (inp_ln.Contains("#Resting delka [s]: "))
            {
                restingClip.SetDuration(inp_ln.Replace("#Resting delka [s]: ", ""));
            }
            if (inp_ln.Contains("#Resting protokol: "))
            {
                if (inp_ln.Replace("#Resting protokol: ", "") == "Standardni")
                    restingClip.SetType(0);
                else
                    restingClip.SetType(1);
                if (restingOn)
                {
                    restingClip.AddClipToProtocol();
                    paradigmaClip.GetComponent<Button>().interactable = false;
                }
            }
            if (inp_ln.Contains("#Paradigma: "))
            {
                paradigmaOn = bool.Parse(inp_ln.Replace("#Paradigma: ", ""));
            }
            if (inp_ln.Contains("#Paradigma delka [s]: "))
            {
                paradigmaClip.SetDuration(inp_ln.Replace("#Paradigma delka [s]: ", ""));
            }
            if (inp_ln.Contains("#Paradigma protokol: "))
            {
                if (inp_ln.Replace("#Paradigma protokol: ", "") == "Standardni")
                    paradigmaClip.SetType(0);
                else
                    paradigmaClip.SetType(1);
                if (paradigmaOn)
                {
                    paradigmaClip.AddClipToProtocol();
                    restingClip.GetComponent<Button>().interactable = false;
                }
            }
            if (inp_ln.Contains("#DTI: "))
            {
                dtiOn = bool.Parse(inp_ln.Replace("#DTI: ", ""));
            }
            if (inp_ln.Contains("#DTI delka [s]: "))
            {
                dtiClip.SetDuration(inp_ln.Replace("#DTI delka [s]: ", ""));
                if (dtiOn)
                    dtiClip.AddClipToProtocol();
            }
            if (inp_ln.Contains("#Zrcadlo: "))
            {
                bool isOn = bool.Parse(inp_ln.Replace("#Zrcadlo: ", ""));
                mirrorSlideshow.SetMirror(isOn);
            }
        }
        inp_stm.Close();
    }
}
