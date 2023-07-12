using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Starf1 : MonoBehaviour
{
    public MeshCollider meshCol;
    public MeshRenderer spike;
    public FailScript1 failS;
    public GameObject after;
    public Starf1 sript;
    public bool inpowermode;
   public bool touched;
    void Start()
    {
        after.SetActive(false);
        sript = this;
        touched = false;
        meshCol = GetComponent<MeshCollider>();
        spike = GetComponent<MeshRenderer>();
      touched =  false;
      //  StartCoroutine(starf());

    }

  
    void Update()
    {

        touched = FindObjectOfType<ParentSpikesript>().spikehit;

        inpowermode = FindObjectOfType<KnifeScript>().ischangecolor;
    }
    void changecolor()
    {
       

    }
    private void OnTriggerEnter(Collider other)
    {
        
            if (other.CompareTag("Knife"))
            {
            //if (FindObjectOfType<SpikeBallScript>().powerup_mode || FindObjectOfType<NewBallScript>().powerup_mode)
            //    {
            //       meshCol.enabled = false; 
            //   }
            //if (FindObjectOfType<SpikeBallScript>().powerup_mode == false || FindObjectOfType<NewBallScript>().powerup_mode == false)
            //{
            //    failS.failed();
            //    meshCol.enabled = false;
            //    spike.enabled = false;
            //}


            if(inpowermode)
             {

                meshCol.enabled = false;
                after.SetActive(true);
            }
        else  if (!inpowermode)
            {
                FindObjectOfType<FailScript1>().failed();
                meshCol.enabled = false;
                spike.enabled = false;
                after.SetActive(true);

            }

        }
        

        
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Knife"))
        {
           
         
        
           
            touched = true;
        }
    }

   
   
}
