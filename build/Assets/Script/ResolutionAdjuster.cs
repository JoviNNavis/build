using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class ResolutionAdjuster : MonoBehaviour
{

    public bool lvl1;

    public RectTransform winPanelLvl1;

    public GameObject blueBg, nxtButton;

    public RectTransform RewardPanel, nxtLvlPanel, LostPanel, selectionPanel, ChestLostPanel;

    public float promax, se, ipad;


    private void Start()
    {
        promax = 1;
        se = 0.9f;
        ipad = 0.85f;
    }

    private void Update()
    {
       // Debug.Log(Camera.main.aspect);
        screenadjuster();
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
        #region   commenting

        /* if(lvl1)
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

         }*/
        #endregion
    }
    public void screenadjuster()
    {
        if((Camera.main.aspect >0.45)&&(Camera.main.aspect < 0.47))
        {
            RewardPanel.transform.localScale = new Vector3(promax, promax, promax);
            nxtLvlPanel.transform.localScale = new Vector3(promax, promax, promax);
            ChestLostPanel.transform.localScale = new Vector3(promax, promax, promax);
            selectionPanel.transform.localScale = new Vector3(promax, promax, promax);
            LostPanel.transform.localScale = new Vector3(promax, promax, promax);


            RewardPanel.transform.localPosition = new Vector3(0, 0, 0);
            nxtLvlPanel.transform.localPosition = new Vector3(0, 0, 0);
            ChestLostPanel.transform.localPosition = new Vector3(0, 0, 0);
            selectionPanel.transform.localPosition = new Vector3(0, 0, 0);
            LostPanel.transform.localPosition = new Vector3(0, 0, 0);


        }
        if ((Camera.main.aspect > 0.55) && (Camera.main.aspect < 0.57))
        {
          // Debug.LogError("se all versions,8 plus");
            RewardPanel.transform.localScale = new Vector3(se, se, se);
            nxtLvlPanel.transform.localScale = new Vector3(se, se, se);
            ChestLostPanel.transform.localScale = new Vector3(se, se, se);
            selectionPanel.transform.localScale = new Vector3(se, se, se);
            LostPanel.transform.localScale = new Vector3(se, se, se);


            RewardPanel.transform.localPosition = new Vector3(0, 0, 0);
            nxtLvlPanel.transform.localPosition = new Vector3(0, 0, 0);
            ChestLostPanel.transform.localPosition = new Vector3(0, 0, 0);
            selectionPanel.transform.localPosition = new Vector3(0, 0, 0);
            LostPanel.transform.localPosition = new Vector3(0, 0, 0);
        }
        if ((Camera.main.aspect > 0.68) && (Camera.main.aspect < 0.70))
        {
          //  Debug.LogError("ipad 4th gen , 11 inche3s");
            RewardPanel.transform.localScale = new Vector3(ipad, ipad, ipad);
            nxtLvlPanel.transform.localScale = new Vector3(ipad, ipad, ipad);
            ChestLostPanel.transform.localScale = new Vector3(ipad, ipad, ipad);
            selectionPanel.transform.localScale = new Vector3(ipad, ipad, ipad);
            LostPanel.transform.localScale = new Vector3(ipad, ipad, ipad);


            RewardPanel.transform.localPosition = new Vector3(0, 0, 0);
            nxtLvlPanel.transform.localPosition = new Vector3(0, 0, 0);
            ChestLostPanel.transform.localPosition = new Vector3(0, 0, 0);
            selectionPanel.transform.localPosition = new Vector3(0, 0, 0);
            LostPanel.transform.localPosition = new Vector3(0, 0, 0);
        }
        if (Camera.main.aspect >= 0.74)
        {
          //  Debug.LogError("ipad 4th gen , 11 inche3s");
            RewardPanel.transform.localScale = new Vector3(ipad, ipad, ipad);
            nxtLvlPanel.transform.localScale = new Vector3(ipad, ipad, ipad);
            ChestLostPanel.transform.localScale = new Vector3(ipad, ipad, ipad);
            selectionPanel.transform.localScale = new Vector3(ipad, ipad, ipad);
            LostPanel.transform.localScale = new Vector3(ipad, ipad, ipad);


            RewardPanel.transform.localPosition = new Vector3(0, 0, 0);
            nxtLvlPanel.transform.localPosition = new Vector3(0, 0, 0);
            ChestLostPanel.transform.localPosition = new Vector3(0, 0, 0);
            selectionPanel.transform.localPosition = new Vector3(0, 0, 0);
            LostPanel.transform.localPosition = new Vector3(0, 0, 0);
       //     Debug.LogError("ipad all versions");

        }

    }
    private void FixedUpdate()
    {

    }
}
