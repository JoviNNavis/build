using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cashScript : MonoBehaviour
{
    public static int cashValue = 0;

    private Text cashText;
    
    void Start()
    {
        cashText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        cashText.text = "" + cashValue;
    }
}
