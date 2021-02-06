using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class RandomlyAnimate : MonoBehaviour
{
    [SerializeField] float speed = 0.5f;
    [SerializeField] float rotationSpeed = 2.0f;
    [SerializeField] CircularDrive door;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        float randomTime = Random.Range(5, 50);
        Invoke("RandomThing", randomTime);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            OpenDoor();
    }

    public void OpenDoor()
    {
        StartCoroutine(DoOpenDoor());
    }

    IEnumerator DoOpenDoor()
    {
        CancelInvoke();
        //transform.LookAt(target.transform.position);
        animator.SetBool("IsWalking", true);

        yield return new WaitForSeconds(1.75f);

        animator.SetBool("IsOpening", true);
        animator.SetBool("IsWalking", false);

        yield return new WaitForSeconds(2.0f);

        while (door.transform.rotation.eulerAngles.y < 90)
        {
            door.transform.Rotate(Vector3.up, 0.7f);
            yield return new WaitForSeconds(0.01f);
        }

        animator.SetBool("IsOpening", false);
        //float randomTime = Random.Range(5, 150);
        Invoke("RandomlyPoint", 2.0f);
    }

    void RandomThing()
    {
        float randomTime = Random.Range(5, 150);
        if(gameObject.activeSelf)
            StartCoroutine("SetLaborantHappy");

        Invoke("RandomThing", randomTime);
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
        animator.SetBool("IsHappy", true);
        float randomTime = Random.Range(1, 20);
        yield return new WaitForSeconds(randomTime);
        //Debug.Log("Laborant is randomly happy");
        animator.SetBool("IsHappy", false);
    }

    IEnumerator SetRandomPointing()
    {
        animator.SetBool("IsPointing", true);
        yield return new WaitForSeconds(1.0f);
        animator.SetBool("IsPointing", false);
    }
}
