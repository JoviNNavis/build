using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class test : MonoBehaviour
{

    void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            Debug.LogWarning("Bsll");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.DORotate(new Vector3(0, 0, -6), 0.25f, RotateMode.FastBeyond360);
        }
    }
}
