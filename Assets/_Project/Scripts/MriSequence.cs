using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MriSequence : MonoBehaviour
{
    public AudioSource mriSound;
    public AudioClip[] mriClips;
    public GameObject player;
    public GameObject movingPart;
    public GameObject headPosition;

    [Range(0,1)]
    public float speed;

    public float endPosition;
    private float startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = movingPart.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSequence()
    {
        
        player.transform.parent = movingPart.transform;
        StartCoroutine(FmriSequence(0));
        StartCoroutine(MoveFmriBed());
    }

    public void SetPlayerPosition()
    {
        //Quaternion rotDif = GameObject.FindWithTag("Player").transform.rotation * Quaternion.Inverse(player.transform.rotation);
        //player.transform.rotation = Quaternion.AngleAxis(90,Vector3.up) * rotDif * player.transform.rotation;
        Vector3 difference = GameObject.FindWithTag("Player").transform.position - player.transform.position;
        player.transform.position = headPosition.transform.position - difference;
    }

    private IEnumerator MoveFmriBed()
    {
        float finalPosition = movingPart.transform.position.x + endPosition;
        while (movingPart.transform.position.x > finalPosition)
        {
            movingPart.transform.position -= new Vector3(speed, 0, 0);
            yield return new WaitForEndOfFrame();
        }

        movingPart.transform.position = new Vector3(finalPosition, movingPart.transform.position.y, movingPart.transform.position.z);
        yield return new WaitForEndOfFrame();
    }

    private IEnumerator FmriSequence(int i)
    {
        mriSound.PlayOneShot(mriClips[i]);
        yield return new WaitForSeconds(mriClips[i].length);
    }
}
