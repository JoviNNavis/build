using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScript : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            collision.transform.position = new Vector3(0.8f, transform.position.y, 0);
            collision.rigidbody.isKinematic = true;
          //  Destroy(collision.rigidbody);
        }

      
    }
}
