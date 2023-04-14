using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiBallScript : MonoBehaviour
{
    public AiScript ai;

    public float rayRange;
    public Transform rayPos;

    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(rayPos.position, rayPos.forward * rayRange, Color.red);
        if (Physics.Raycast(rayPos.position, rayPos.forward, out hit, rayRange))
        {
            ai.enabled = false;
            Empty plainKnife = GetComponent<Empty>();
            AiknifePrefab aiKnife = GetComponent<AiknifePrefab>();
            if (aiKnife != null || plainKnife != null)
            {
                ai.enabled = false;

                Debug.Log("TRUE");
            }
            //Debug.Log("TRUE");
            //if(aiKnife == null)
            //{
            //    Debug.Log("FALSE");
            //    ai.enabled = true;
            //}
        }
        else
        {
            Debug.Log("False");
            ai.enabled = true;
        }
    }
}
