using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swapictures : MonoBehaviour
{
 public GameObject pic1,pic2;
    void Start()
    {
        
    }


    void Update()
    {
        if (mid.isskin)
        {
            pic1.SetActive(false);
            pic2.SetActive(true);
        
        }
        else
        {
            pic1.SetActive(true);
            pic2.SetActive(false);
        
        }
    }
}
