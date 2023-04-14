using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Rot1 : MonoBehaviour
{
    public FailScript1 failS;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Knife"))
        {
            failS.failed();
            Destroy(this.gameObject, 0.5f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("KnifePref"))
        {
            //this.enabled = false;
            // meshCol.enabled = false;
            Destroy(this.gameObject, 0.25f);
        }
    }
}
