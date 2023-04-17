using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExtraReward : MonoBehaviour
{
    public float countValue;
    
    public TextMeshProUGUI count1, count2;

    public GameObject glow, thisPanel, newPanel;

    void Start()
    {
        countValue = 5;
    }

    // Update is called once per frame
    void Update()
    {
        glow.transform.Rotate(0, 0, 0.5f);

        count1.text = "" + countValue.ToString("0");
        count2.text = "" + countValue.ToString("0");

        countValue -= 1 * Time.deltaTime;

        if(countValue <= 0)
        {
            countValue = 5;
            thisPanel.SetActive(false);
            newPanel.SetActive(true);
        }
    }
}
