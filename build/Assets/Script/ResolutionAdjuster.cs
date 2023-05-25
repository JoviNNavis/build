using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class ResolutionAdjuster : MonoBehaviour
{

    public bool lvl1;

    public RectTransform winPanelLvl1;

    public GameObject blueBg, nxtButton;

    public float currvalue1, currvalue2;

    public float newvalue1, newvalue2;
    
    public RectTransform RewardPanel, nxtLvlPanel, LostPanel, selectionPanel, ChestLostPanel;

    public float valueX1, valueY1;  //iphone se

    public float valueX2, valueY2;  //iphone se 2gen

    public float valueX3, valueY3;  //iphone mini, 11, 11 pro, 11 pro max, 12 pro, 12 pro max 

    public float valueX4, valueY4;  //ipad pro 9.7

    public float valueX5, valueY5;  //ipad 8th gen

    public float valueX6, valueY6;  //ipad 4th gen

    public float valueX7, valueY7;  //ipad pro 10.5

    public float valueX8, valueY8;  //ipad pro 11 

    public float valueX9, valueY9;  //ipad 12.9 


    private void Start()
    {
        
    }

    private void Update()
    {
        //Debug.Log(Camera.main.aspect);

        #region
        //if(Camera.main.aspect < 0.5f)
        //{
        //    Debug.Log("Default");

        //    RewardPanel.transform.position = new Vector3(valueX1, valueY1, 0);
        //    RewardPanel.transform.localScale = new Vector3(1, 1f, 0);

        //    nxtLvlPanel.transform.position = new Vector3(valueX1, valueY1, 0);
        //    nxtLvlPanel.transform.localScale = new Vector3(1, 1f, 0);

        //    LostPanel.transform.position = new Vector3(valueX1, valueY1, 0);
        //    LostPanel.transform.localScale = new Vector3(1, 1f, 0);

        //    selectionPanel.transform.position = new Vector3(valueX1, valueY1, 0);
        //    selectionPanel.transform.localScale = new Vector3(1, 1f, 0);

        //    ChestLostPanel.transform.position = new Vector3(valueX1, valueY1, 0);
        //    ChestLostPanel.transform.localScale = new Vector3(1, 1f, 0);
        //}

        //if(Camera.main.aspect >= 0.5f && Camera.main.aspect <= 0.7f)
        //{
        //    Debug.Log("16:9");

        //    RewardPanel.transform.position = new Vector3(valueX2, valueY2, 0);
        //    RewardPanel.transform.localScale = new Vector3(1, 1f, 0);

        //    nxtLvlPanel.transform.position = new Vector3(valueX2, valueY2, 0);
        //    nxtLvlPanel.transform.localScale = new Vector3(1, 1f, 0);

        //    LostPanel.transform.position = new Vector3(valueX2, valueY2, 0);
        //    LostPanel.transform.localScale = new Vector3(1, 1f, 0);

        //    selectionPanel.transform.position = new Vector3(valueX2, valueY2, 0);
        //    selectionPanel.transform.localScale = new Vector3(1, 1f, 0);

        //    ChestLostPanel.transform.position = new Vector3(valueX2, valueY2, 0);
        //    ChestLostPanel.transform.localScale = new Vector3(1, 1f, 0);

        //}

        //if (Camera.main.aspect > 0.7f)
        //{
        //    Debug.Log("4:3");

        //    RewardPanel.transform.position = new Vector3(valueX3, valueY3, 0);
        //    RewardPanel.transform.localScale = new Vector3(1, 0.9f, 0);

        //    nxtLvlPanel.transform.position = new Vector3(valueX3, valueY3, 0);
        //    nxtLvlPanel.transform.localScale = new Vector3(1, 0.9f, 0);

        //    LostPanel.transform.position = new Vector3(valueX3, valueY3, 0);
        //    LostPanel.transform.localScale = new Vector3(1, 0.9f, 0);

        //    selectionPanel.transform.position = new Vector3(valueX3, valueY3, 0);
        //    selectionPanel.transform.localScale = new Vector3(1, 0.9f, 0);

        //    ChestLostPanel.transform.position = new Vector3(valueX4, valueY4, 0);
        //    ChestLostPanel.transform.localScale = new Vector3(1, 0.9f, 0);
        //}
        #endregion

        if(lvl1)
        {
            if(Camera.main.aspect == 0.75f || Camera.main.aspect == 0.749634f  || Camera.main.aspect == 0.6949152f || Camera.main.aspect == 0.6984925f)
            {
                winPanelLvl1.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
                blueBg.transform.localPosition = new Vector3(transform.localPosition.x, newvalue1, transform.localPosition.z);
                nxtButton.transform.localPosition = new Vector3(transform.localPosition.x, newvalue2, transform.localPosition.z);
            }

            if(Camera.main.aspect == 0.5633803f || Camera.main.aspect == 0.5622189f || Camera.main.aspect == 0.5625f)
            {
                winPanelLvl1.transform.localScale = new Vector3(0.85f, 0.85f, 0.85f);
            }

            //else if (Camera.main.aspect == 0.4620536f || Camera.main.aspect == 0.4615385f || Camera.main.aspect == 0.4618227f || Camera.main.aspect == 0.4620853f || Camera.main.aspect == 0.462203f)
            //{
            //    winPanelLvl1.transform.localScale = new Vector3(1f, 1f, 1f);
            //    blueBg.transform.localPosition = new Vector3(transform.localPosition.x, currvalue1, transform.localPosition.z);
            //    nxtButton.transform.localPosition = new Vector3(transform.localPosition.x, currvalue2, transform.localPosition.z);

            //}

            //else
            //{
            //    winPanelLvl1.transform.localScale = new Vector3(1f, 1f, 1f);
            //    blueBg.transform.localPosition = new Vector3(transform.localPosition.x, currvalue1, transform.localPosition.z);
            //    nxtButton.transform.localPosition = new Vector3(transform.localPosition.x, currvalue2, transform.localPosition.z);
            //}

        }
    }
}
