using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballpowerup : MonoBehaviour
{

 
    public float time;

    void Start()
    {
        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            time = 0;
        }
        if (collision.gameObject.CompareTag("AiBall"))
        {
            time = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Stop")
        {

           
        }
        if (other.gameObject.tag == "Spike" )
        {

        // FindObjectOfType<FailScript1>(). knife1.enabled = false;
          //  StartCoroutine(FindObjectOfType<FailScript1>().SpikeR3());
            Debug.Log("BallHit");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Block") || other.gameObject.CompareTag("Finish"))
        {
            time += Time.deltaTime;
        }

    }

     
void Update()
    {
        
    }
}
