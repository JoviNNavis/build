using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusCoinDouble : MonoBehaviour
{
    public static int bonusValue;
    public Text[] coinText;

    public BounsCoinCollect bonusCoin;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coinText[0].text = "+" + BounsCoinCollect.coinValue * 2;
        coinText[1].text = "+" + BounsCoinCollect.coinValue * 3;
        coinText[2].text = "+" + BounsCoinCollect.coinValue * 5;
    }
}
