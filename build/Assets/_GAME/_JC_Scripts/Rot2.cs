using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rot2 : MonoBehaviour
{
    public AiFailScript aiFail;

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
        if (other.CompareTag("AiKnife"))
        {
            aiFail.failed();
            Destroy(this.gameObject, 0.5f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("AiPref"))
        {
            Destroy(this.gameObject, 0.25f);
        }
    }
}
