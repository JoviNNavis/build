using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class test : MonoBehaviour
{
    public GameObject cube;
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


    IEnumerator text()
    {
        cube.SetActive(true);
        yield return new WaitForSeconds(1);
        cube.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(text());
        }
    }
}
