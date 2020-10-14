using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ReadConfigFile : MonoBehaviour
{
    public MriSequence mriSequence;
    public MirrorSlideshow mirrorSlideshow;

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
        ReadTextFile(Application.dataPath + "/../Configs/" + "default.txt");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReadTextFile(string file_path)
    {
        StreamReader inp_stm = new StreamReader(file_path, System.Text.Encoding.UTF8);

        while (!inp_stm.EndOfStream)
        {
            string inp_ln = inp_stm.ReadLine();
            if (inp_ln.Contains("#BedHeight: "))
            {
                float bedHeight = float.Parse(inp_ln.Replace("#BedHeight: ", ""));
                if(bedHeight < 1.041462)
                {
                    mriSequence.SetBedHeight(bedHeight);
                }
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
                    restingClip.AddClipToProtocol();
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
                    paradigmaClip.AddClipToProtocol();
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
        }
        inp_stm.Close();
    }
}
