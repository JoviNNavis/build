using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissedKnife : MonoBehaviour
{
    public static int knifeValue = 0;

    public StackCounterScript stackCounter;

    Text knifeText;

    void Start()
    {
        knifeText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        knifeText.text = " " + knifeValue + " " + "Knife";

        knifeValue = stackCounter.stacks.Count;
    }
}
