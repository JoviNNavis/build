using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class knifeCounter : MonoBehaviour
{
    public static int knifeCountValue = 0;

    private Text knifeTxt;

    void Start()
    {
        knifeCountValue = 0;
        knifeTxt = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        knifeTxt.text = "+" + knifeCountValue;
    }
}
