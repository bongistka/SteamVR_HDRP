using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MriSequence : MonoBehaviour
{
    public AudioSource mriSound;
    public AudioClip[] mriClips;
    public Collider mriCollider;
    public GameObject movingPart;

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FmriSequence(0));
            StartCoroutine(MoveFmriBed());
        }
    }

    private IEnumerator MoveFmriBed()
    {
        while (movingPart.transform.position.x > endPosition)
        {
            movingPart.transform.position -= new Vector3(speed, 0, 0);
            yield return new WaitForEndOfFrame();
        }

        movingPart.transform.position = new Vector3(speed, movingPart.transform.position.y, movingPart.transform.position.z);
        yield return new WaitForEndOfFrame();
    }

    private IEnumerator FmriSequence(int i)
    {
        mriSound.PlayOneShot(mriClips[i]);
        yield return new WaitForSeconds(mriClips[i].length);
    }
}
