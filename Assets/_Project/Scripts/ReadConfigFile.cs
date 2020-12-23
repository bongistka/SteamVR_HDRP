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
    [SerializeField] AddSequenceClip addSequenceClip;

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
        addSequenceClip.ResetSequence();
        if (configDropdown.value != 0)
            ReadTextFile(Application.dataPath + "/../Configs/" + configDropdown.options[configDropdown.value].text);
    }

    public void ResetDropdownValue()
    {
        configDropdown.value = 0;
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
                    addSequenceClip.AddSequence(0);
            }
            if (inp_ln.Contains("#Morfologie: "))
            {
                morfologieOn = bool.Parse(inp_ln.Replace("#Morfologie: ", ""));
                if (morfologieOn)
                    addSequenceClip.AddSequence(1);
            }
            if (inp_ln.Contains("#Morfologie delka [s]: "))
            {
                addSequenceClip.SetDurationOfLast(inp_ln.Replace("#Morfologie delka [s]: ", ""));
                //morfologieClip.SetDuration(inp_ln.Replace("#Morfologie delka [s]: ", ""));
            }
            if (inp_ln.Contains("#Morfologie protokol: "))
            {
                if (inp_ln.Replace("#Morfologie protokol: ", "") == "T1")
                    addSequenceClip.SetTypeOfLast(0);
                else
                    addSequenceClip.SetTypeOfLast(1);
            }
            if (inp_ln.Contains("#Resting: "))
            {
                restingOn = bool.Parse(inp_ln.Replace("#Resting: ", ""));
                if (restingOn)
                    addSequenceClip.AddSequence(2);
            }
            if (inp_ln.Contains("#Resting delka [s]: "))
            {
                addSequenceClip.SetDurationOfLast(inp_ln.Replace("#Resting delka [s]: ", ""));
            }
            if (inp_ln.Contains("#Resting protokol: "))
            {
                if (inp_ln.Replace("#Resting protokol: ", "") == "Standardni")
                    addSequenceClip.SetTypeOfLast(0);
                else
                    addSequenceClip.SetTypeOfLast(1);
            }
            if (inp_ln.Contains("#Paradigma: "))
            {
                paradigmaOn = bool.Parse(inp_ln.Replace("#Paradigma: ", ""));
                if (paradigmaOn)
                    addSequenceClip.AddSequence(3);
            }
            if (inp_ln.Contains("#Paradigma delka [s]: "))
            {
                addSequenceClip.SetDurationOfLast(inp_ln.Replace("#Paradigma delka [s]: ", ""));
            }
            if (inp_ln.Contains("#Paradigma protokol: "))
            {
                if (inp_ln.Replace("#Paradigma protokol: ", "") == "Standardni")
                    addSequenceClip.SetTypeOfLast(0);
                else
                    addSequenceClip.SetTypeOfLast(1);
            }
            if (inp_ln.Contains("#DTI: "))
            {
                dtiOn = bool.Parse(inp_ln.Replace("#DTI: ", ""));
                if (dtiOn)
                    addSequenceClip.AddSequence(4);
            }
            if (inp_ln.Contains("#DTI delka [s]: "))
            {
                addSequenceClip.SetDurationOfLast(inp_ln.Replace("#DTI delka [s]: ", ""));
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
