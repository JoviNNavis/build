using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayBall : MonoBehaviour
{
    public Transform rayPos;

    public float rayRange;

    public AiScript aiKnife;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;
        Debug.DrawRay(rayPos.position, rayPos.forward * rayRange, Color.green);

        if (Physics.Raycast(rayPos.position, rayPos.forward, out hit, rayRange))
        {
            AiBallpowerup Ai = hit.transform.GetComponent<AiBallpowerup>();
            if (Ai != null)
            {
                Debug.Log("throw");
                aiKnife.enabled = true;
            }

        }
        else
        {
            Debug.Log("Stop");
            aiKnife.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("AiMain"))
        {
            AiScript ai;
            if (other.TryGetComponent(out ai))
            {
                aiKnife = ai;
            }
        }
    }
}
