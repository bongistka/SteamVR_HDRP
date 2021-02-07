﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class AnimationController : MonoBehaviour
{
    [SerializeField] float distance = 1.6f;
    [Tooltip("Degrees per second")]
    [SerializeField] float doorRotationSpeed = 0.7f;
    [SerializeField] float waitBeforeOpenTheDoor = 2.0f;
    [SerializeField] CircularDrive door;
    [SerializeField] Button doorButton;
    [SerializeField] Toggle[] bodySwitchButtons;
    private bool isOpening;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        float randomTime = Random.Range(5, 50);
        Invoke("RandomlyMove", randomTime);
    }

    public void OpenDoor()
    {
        if(isActiveAndEnabled)
            StartCoroutine(DoOpenDoor());
    }

    IEnumerator DoOpenDoor()
    {
        CancelInvoke();
        isOpening = true;
        doorButton.interactable = false;
        foreach (Toggle toggle in bodySwitchButtons)
            toggle.interactable = false;
        
        var currentPosition = transform.position;
        animator.SetBool("IsWalking", true);

        while (Vector3.Distance(currentPosition, transform.position) < distance)
            yield return new WaitForEndOfFrame();

        animator.SetBool("IsOpening", true);
        animator.SetBool("IsWalking", false);

        yield return new WaitForSeconds(waitBeforeOpenTheDoor);

        while (door.transform.rotation.eulerAngles.y < 90)
        {
            door.transform.Rotate(Vector3.up, doorRotationSpeed);
            yield return new WaitForSeconds(0.01f);
        }

        animator.SetBool("IsOpening", false);
        isOpening = false;
        Invoke("RandomlyPoint", 2.0f);
    }

    void RandomlyMove()
    {
        if (!isOpening)
        {
            float randomTime = Random.Range(5, 150);
            if (gameObject.activeSelf)
                StartCoroutine("SetLaborantHappy");

            Invoke("RandomlyMove", randomTime);
        }
    }

    void RandomlyPoint()
    {
        float randomTime = Random.Range(5, 150);
        if (gameObject.activeSelf)
            StartCoroutine("SetRandomPointing");

        Invoke("RandomlyPoint", randomTime);
    }

    IEnumerator SetLaborantHappy()
    {
        if (!isOpening)
        {
            animator.SetBool("IsHappy", true);
            float randomTime = Random.Range(1, 20);
            yield return new WaitForSeconds(randomTime);
            animator.SetBool("IsHappy", false);
        }
    }

    IEnumerator SetRandomPointing()
    {
        animator.SetBool("IsPointing", true);
        yield return new WaitForSeconds(1.0f);
        animator.SetBool("IsPointing", false);
    }
}
