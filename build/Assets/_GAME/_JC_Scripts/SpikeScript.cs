using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    public BonusKnifeScript knife;
   

    public Animator anim;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Knife"))
        {
            Destroy(other.gameObject);
            StartCoroutine(knifeDisable());
        }
    }

    IEnumerator knifeDisable()
    {
        knife.enabled = false;
        anim.SetBool("Fill", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("Fill", false);
        knife.enabled = true;
    }
}
