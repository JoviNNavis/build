using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class animScript : MonoBehaviour
{
    public Animator anim;
    public Animator anim2;
    public GameObject knife, sword;
    public GameObject kniferot, swordrot;
    public float waitTime;
    public float rotz;
    void Start()
    {
        waitTime = 0.65f;
     if(mid.isskin)
        {
            knife.SetActive(false);
            kniferot.SetActive(false);
            sword.SetActive(true);
            swordrot.SetActive(true);
        }
        else
        {
            knife.SetActive(true);
            kniferot.SetActive(true);
            sword.SetActive(false);
            swordrot.SetActive(false);
        }

       
    }

    // Update is called once per frame
    void Update()
    {
        if (mid.isskin)
        {
            knife.SetActive(false);
            kniferot.SetActive(false);
            sword.SetActive(true);
            swordrot.SetActive(true);
        }
        else
        {
            knife.SetActive(true);
            kniferot.SetActive(true);
            sword.SetActive(false);
            swordrot.SetActive(false);
        }
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
      
        if(collision.gameObject.CompareTag("Ball")||collision.gameObject.CompareTag("AiBall"))
        {
          
          //  StartCoroutine(animPlay());
        }
    }
    IEnumerator kinfebend()
    {
        if (mid.isskin == false)
        {


            anim.SetBool("spawn", true);

            yield return new WaitForSeconds(0.45f);

            anim.SetBool("spawn", false);
        }
        else
        {

            anim2.SetBool("spawn", true);

            yield return new WaitForSeconds(0.45f);

            anim2.SetBool("spawn", false);
        }

    }

    IEnumerator animPlay()
    {

        if (mid.isskin == false)
        { 
         anim.SetBool("bounce", true);
        yield return new WaitForSeconds(waitTime);
        anim.SetBool("bounce", false);
        }
        else
        {
            anim2.SetBool("bounce", true);
            yield return new WaitForSeconds(waitTime);
            anim2.SetBool("bounce", false);
        }
    }
}
