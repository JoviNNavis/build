using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public float upForce;

    private Rigidbody Rb;

    void Start()
    {
        Rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            Rb.AddForce(transform.up * upForce);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Knife") || collision.gameObject.CompareTag("DKnife"))
        {
            Rb.AddForce(transform.up * upForce, ForceMode.Force);
        }     
    }
}
