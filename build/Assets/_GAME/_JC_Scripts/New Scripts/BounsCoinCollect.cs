using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BounsCoinCollect : MonoBehaviour
{
    public static int coinValue = 0;

    private Text coinText;

    void Start()
    {
        coinText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = "" + coinValue;
    }
}
