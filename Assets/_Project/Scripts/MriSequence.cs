using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MriSequence : MonoBehaviour
{
    public AudioSource mriSound;
    public AudioSource hydraulicSound;
    public AudioClip[] mriClips;

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

    // Start is called before the first frame update
    void Start()
    {
        startPosition = movingPart.transform.position.x;
        bedEnd = hydraulicBed.transform.position.y;
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
        StartCoroutine(FmriSequence(0));
    }

    private IEnumerator MoveFmriBed()
    {
        yield return RaiseHydraulicBed();
        hydraulicSound.Play();
        float finalPosition = movingPart.transform.position.x + endPosition;

        while (movingPart.transform.position.x > finalPosition)
        {
            speed = (endPosition / hydraulicSound.clip.length) * Time.deltaTime;
            movingPart.transform.position += new Vector3(speed, 0, 0);
            yield return new WaitForEndOfFrame();
        }

        movingPart.transform.position = new Vector3(finalPosition, movingPart.transform.position.y, movingPart.transform.position.z);
        yield return new WaitForEndOfFrame();
        hydraulicSound.Stop();
    }

    private IEnumerator RaiseHydraulicBed()
    {
        hydraulicSound.Play();
        float distance = bedEnd - hydraulicBed.transform.position.y;
        while (hydraulicBed.transform.position.y < bedEnd)
        {
            speed = (distance / hydraulicSound.clip.length) * Time.deltaTime;
            hydraulicBed.transform.position += new Vector3(0, speed, 0);
            yield return new WaitForEndOfFrame();
        }

        hydraulicBed.transform.position = new Vector3(hydraulicBed.transform.position.x, bedEnd, hydraulicBed.transform.position.z);
        yield return new WaitForEndOfFrame();
        hydraulicSound.Stop();
    }

    private IEnumerator FmriSequence(int i)
    {
        mriSound.PlayOneShot(mriClips[i]);
        yield return new WaitForSeconds(mriClips[i].length);
    }
}
