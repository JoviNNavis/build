using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController1 : MonoSingleton1<PlayerController1>
{


    //public List<GameObject> collectibles=new List<GameObject>();
    public List<GameObject> collectibles;
    //public GameObject EndMonstr;
    //public GameObject cinemachineVirtualCamera;
    //public Animator animator;

    public GameObject EndMonstr1;
    public GameObject cinemachineVirtualCamera1;
    public Animator animator1;
    public GameObject blood;
    public GameObject blood2;


    //public Rigidbody playerRb;

    // public float jumpForce = 10f;
    // public int increment;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GameFinish"))
        {
            for(int i=0; i<collectibles.Count;i++)
            {
                //cinemachineVirtualCamera.SetActive(true);
                //animator.SetBool("Horror", true);
                collectibles[i].SetActive(false);
                //EndMonstr.SetActive(true);

                cinemachineVirtualCamera1.SetActive(true);
                animator1.SetBool("Shake", true);
                cmpltetask();

                EndMonstr1.SetActive(true);


            }

            //collectibles[increment].GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //increment++;
            //playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //Debug.Log("Jump here");
        }
    }

    private void cmpltetask()
    {
        StartCoroutine(WaitFinish());
    }
    private IEnumerator WaitFinish()
    {
        yield return new WaitForSeconds(2.0f);
        blood.SetActive(true);
        blood2.SetActive(true);



    }


























}

