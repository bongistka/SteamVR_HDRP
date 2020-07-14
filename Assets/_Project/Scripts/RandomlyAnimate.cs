using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomlyAnimate : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        float randomTime = Random.Range(5, 50);
        Invoke("RandomThing", randomTime);
    }

    void RandomThing()
    {
        float randomTime = Random.Range(5, 150);

        StartCoroutine("SetLaborantHappy");

        Invoke("RandomThing", randomTime);
    }

    IEnumerator SetLaborantHappy()
    {
        animator.SetBool("IsHappy", true);
        float randomTime = Random.Range(1, 20);
        yield return new WaitForSeconds(randomTime);
        //Debug.Log("Laborant is randomly happy");
        animator.SetBool("IsHappy", false);
    }
}
