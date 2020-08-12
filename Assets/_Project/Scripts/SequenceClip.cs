using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class SequenceClip : MonoBehaviour
{
    public AudioClip[] shim;
    public AudioClip[] mainSounds;
    public int type = 0;
    public float duration = 0.0f;

    public bool isParadigma;

    private MriSequence mriSequence;

    private void Start()
    {
        mriSequence = GameObject.FindWithTag("MriSequence").GetComponent<MriSequence>();
    }

    public void SetDuration(string durationString)
    {
        duration = float.Parse(durationString, CultureInfo.InvariantCulture);
    }

    public void SetType(int i)
    {
        type = i;
    }

    public void AddClipToProtocol()
    {
        mriSequence.AddSequenceClip(this);
    }

    public override string ToString()
    {
        string outputString = "sequence: ";
        outputString += shim[type] != null ? shim[type].name : "no shim";
        outputString += "; ";
        outputString += mainSounds[type] != null ? mainSounds[type].name : "please pick a type";
        outputString += "; duration: ";
        outputString += duration > 0 ? duration : mainSounds[type].length;
        outputString += "\n\n";
        return outputString;
    }
}
