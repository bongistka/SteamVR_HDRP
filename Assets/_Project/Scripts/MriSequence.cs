﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MriSequence : MonoBehaviour
{
    public AudioSource mriSound;
    public AudioSource insertBedSound;
    public AudioSource raiseBedSound;
    //public AudioClip[] mriClips;

    public GameObject player;

    public GameObject hydraulicBed;
    public GameObject movingPart;
    public GameObject headPosition;

    [Range(0,1)]
    public float speed;

    public float bedStart;
    private float bedEnd;

    public float endPosition;
    private float startPosition;

    private List<SequenceClip> clips = new List<SequenceClip>();

    public Text sequenceDebugText;
    public Button launchSequenceButton;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = movingPart.transform.position.x;
        bedEnd = 1.041462f;
        Vector3 bedPosition = hydraulicBed.transform.position;
        bedPosition.y = bedStart;
        hydraulicBed.transform.position = bedPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSequence()
    {
        player.transform.parent = movingPart.transform;
        StartCoroutine(MoveFmriBed());
    }

    public void SetPlayerPosition()
    {
        Vector3 difference = GameObject.FindWithTag("Player").transform.position - player.transform.position;
        player.transform.position = headPosition.transform.position - difference;
    }

    public void StartScanning()
    {
        StartCoroutine(FmriSequence());
    }

    private IEnumerator MoveFmriBed()
    {
        yield return RaiseHydraulicBed();
        insertBedSound.Play();
        float finalPosition = movingPart.transform.position.x + endPosition;

        while (movingPart.transform.position.x > finalPosition)
        {
            speed = (endPosition / insertBedSound.clip.length) * Time.deltaTime;
            movingPart.transform.position += new Vector3(speed, 0, 0);
            yield return new WaitForEndOfFrame();
        }

        movingPart.transform.position = new Vector3(finalPosition, movingPart.transform.position.y, movingPart.transform.position.z);
        yield return new WaitForEndOfFrame();
        insertBedSound.Stop();
    }

    private IEnumerator RaiseHydraulicBed()
    {
        raiseBedSound.Play();
        float distance = bedEnd - hydraulicBed.transform.position.y;
        while (hydraulicBed.transform.position.y < bedEnd)
        {
            speed = (distance / raiseBedSound.clip.length) * Time.deltaTime;
            hydraulicBed.transform.position += new Vector3(0, speed, 0);
            yield return new WaitForEndOfFrame();
        }

        hydraulicBed.transform.position = new Vector3(hydraulicBed.transform.position.x, bedEnd, hydraulicBed.transform.position.z);
        yield return new WaitForEndOfFrame();
        insertBedSound.Stop();
    }

    private IEnumerator FmriSequence()
    {
        sequenceDebugText.text = "Spouštení sekvence:";
        yield return new WaitForSeconds(3);
        foreach (SequenceClip clip in clips)
        {
            if(clip.shim != null)
            {
                if(clip.shim[clip.type] != null)
                {
                    sequenceDebugText.text = "Sekvence: " + clip.shim[clip.type].name;
                    mriSound.PlayOneShot(clip.shim[clip.type]);
                    yield return new WaitForSeconds(clip.shim[clip.type].length);
                }
            }
            if (clip.mainSounds != null)
            {
                if (clip.isParadigma)
                    GetComponent<MirrorSlideshow>().StartSlideshow();
                if (clip.mainSounds[clip.type] != null)
                {
                    sequenceDebugText.text = "Sekvence: " + clip.mainSounds[clip.type].name;
                    mriSound.clip = clip.mainSounds[clip.type];
                    mriSound.loop = true;
                    mriSound.Play();
                    if (clip.duration !=0)
                    {
                        yield return new WaitForSeconds(clip.duration);
                    }
                    else
                    {
                        yield return new WaitForSeconds(clip.mainSounds[clip.type].length);
                    }
                    mriSound.Stop();
                }
            }
        }
        sequenceDebugText.text = "Sekvence skončila";
    }

    public void SetBedHeight(float i)
    {
        bedStart = i;
        Vector3 bedPosition = hydraulicBed.transform.position;
        bedPosition.y = bedStart;
        hydraulicBed.transform.position = bedPosition;
    }

    public void AddSequenceClip(SequenceClip newClip)
    {
        sequenceDebugText.text = "";
        clips.Add(newClip);
        foreach (SequenceClip clip in clips)
        {
            sequenceDebugText.text += clip.ToString();
        }
        launchSequenceButton.interactable = true;
    }

    public void ResetSequenceClip()
    {
        StopCoroutine(FmriSequence());
        sequenceDebugText.text = "Pokyny k nastavení protokolu:\n\nNejdříve vyplnte délku požadované sekvence, poté klikněte na tlačidlo. Pokud chcete začít znovu, použijte tlačidlo \"Reset\"";
        clips.Clear();
        launchSequenceButton.interactable = false;
        DisableButton[] disabledButtons = GameObject.FindObjectsOfType<DisableButton>();
        foreach(DisableButton button in disabledButtons)
            button.GetComponent<Button>().interactable = true;
        GetComponent<MirrorSlideshow>().ResetSlideshow();
    }
}
