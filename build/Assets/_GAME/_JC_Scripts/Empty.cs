using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Empty : MonoBehaviour
{
    public GameObject cam1, cam2;

    void Start()
    {
        cam1.SetActive(false);
        cam2.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
