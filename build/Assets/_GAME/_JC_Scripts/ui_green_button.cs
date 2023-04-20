using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class ui_green_button : MonoBehaviour
{
    float time;
    int coins;
    int totalcoins;
    public Image greenbar;
    public TMP_Text _text;
        public TMP_Text total_coins;

    void Start()
    {
        greenbar.fillAmount = 0;  
    }
  public  void addcoins()
    {
      
        totalcoins += 60;
        total_coins.text = totalcoins.ToString();
    }

    void Update()
    {


        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    totalcoins += coins;
        //    total_coins.text = totalcoins.ToString();
        //}
        greenbar.DOFillAmount(1, 300);

        time += Time.deltaTime;
        _text.text = coins.ToString();
        if (time >= 10)
        {
            coins += 30;
            time = 0;
        }
    }
}
