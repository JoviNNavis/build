using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpScript : MonoBehaviour
{
    public Transform coinPos;

    public float upValue;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, upValue, 0) * Time.deltaTime;
        coinPos.transform.position += new Vector3(0, upValue, 0) * Time.deltaTime;
    }
}
