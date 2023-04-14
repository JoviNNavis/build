using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterKnifeScript : MonoBehaviour
{
    public static int knifeCountValue = 0;

    private Text knifeTxt;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        knifeTxt.text = "+" + knifeCountValue;
    }
}
