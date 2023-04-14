using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AiknifePrefab : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;

   // public JellyMesh jelly;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0.4f, 0, 0) * speed * Time.deltaTime;
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Block") || other.gameObject.CompareTag("Finish"))
        {
            transform.position = new Vector3(0.581f, transform.localPosition.y, transform.localPosition.z);

           StartCoroutine(disable());
            this.enabled = false;
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Game Over");
        }
    }

    IEnumerator disable()
    {
        yield return new WaitForSeconds(3f);
       // jelly.enabled = false;
    }
}
