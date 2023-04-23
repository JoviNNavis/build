using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class animScript1 : MonoBehaviour
{
    public Animator anim;

    public float waitTime;
    public float rotz;
    void Start()
    {
        waitTime = 0.55f;


    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Block"))
        {
            StartCoroutine(kinfebend());

        }
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ball") || collision.gameObject.CompareTag("AiBall"))
        {
            //    StartCoroutine(kinfebend());
            StartCoroutine(animPlay());
        }
    }
    private void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ball") || collision.gameObject.CompareTag("AiBall"))
        {

            //  StartCoroutine(animPlay());
        }
    }
    IEnumerator kinfebend()
    {
       


            anim.SetBool("spawn", true);

            yield return new WaitForSeconds(0.45f);

            anim.SetBool("spawn", false);

        
    }

    IEnumerator animPlay()
    {

        anim.SetBool("bounce", true);
        yield return new WaitForSeconds(waitTime);
        anim.SetBool("bounce", false);


    }
}