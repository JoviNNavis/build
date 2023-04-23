using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swapbuttons : MonoBehaviour
{
 public GameObject buttons,button;
    void Start()
    {
        
    }


    void Update()
    {
        if (mid.isskin)
        {
            buttons.SetActive(true);
            button.SetActive(false);


        }
        else
        {

            buttons.SetActive(false);
            button.SetActive(true);

        }
    }
}
