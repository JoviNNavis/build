using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class ResolutionAdjuster : MonoBehaviour
{
    public RectTransform RewardPanel, nxtLvlPanel, LostPanel, selectionPanel, ChestLostPanel;

    public float valueX1, valueY1;  //Default

    public float valueX2, valueY2;  //16:9

    public float valueX3, valueY3;  //4:3

    public float valueX4, valueY4;

    private void Start()
    {
        
    }

    private void Update()
    {
        Debug.Log(Camera.main.aspect);

        if(Camera.main.aspect < 0.5f)
        {
            Debug.Log("Default");

            RewardPanel.transform.position = new Vector3(valueX1, valueY1, 0);
            RewardPanel.transform.localScale = new Vector3(1, 1f, 0);
            
            nxtLvlPanel.transform.position = new Vector3(valueX1, valueY1, 0);
            nxtLvlPanel.transform.localScale = new Vector3(1, 1f, 0);

            LostPanel.transform.position = new Vector3(valueX1, valueY1, 0);
            LostPanel.transform.localScale = new Vector3(1, 1f, 0);

            selectionPanel.transform.position = new Vector3(valueX1, valueY1, 0);
            selectionPanel.transform.localScale = new Vector3(1, 1f, 0);

            ChestLostPanel.transform.position = new Vector3(valueX1, valueY1, 0);
            ChestLostPanel.transform.localScale = new Vector3(1, 1f, 0);
        }

        if(Camera.main.aspect >= 0.5f && Camera.main.aspect <= 0.7f)
        {
            Debug.Log("16:9");

            RewardPanel.transform.position = new Vector3(valueX2, valueY2, 0);
            RewardPanel.transform.localScale = new Vector3(1, 1f, 0);
            
            nxtLvlPanel.transform.position = new Vector3(valueX2, valueY2, 0);
            nxtLvlPanel.transform.localScale = new Vector3(1, 1f, 0);

            LostPanel.transform.position = new Vector3(valueX2, valueY2, 0);
            LostPanel.transform.localScale = new Vector3(1, 1f, 0);

            selectionPanel.transform.position = new Vector3(valueX2, valueY2, 0);
            selectionPanel.transform.localScale = new Vector3(1, 1f, 0);

            ChestLostPanel.transform.position = new Vector3(valueX2, valueY2, 0);
            ChestLostPanel.transform.localScale = new Vector3(1, 1f, 0);

        }

        if (Camera.main.aspect > 0.7f)
        {
            Debug.Log("4:3");

            RewardPanel.transform.position = new Vector3(valueX3, valueY3, 0);
            RewardPanel.transform.localScale = new Vector3(1, 0.9f, 0);
            
            nxtLvlPanel.transform.position = new Vector3(valueX3, valueY3, 0);
            nxtLvlPanel.transform.localScale = new Vector3(1, 0.9f, 0);

            LostPanel.transform.position = new Vector3(valueX3, valueY3, 0);
            LostPanel.transform.localScale = new Vector3(1, 0.9f, 0);

            selectionPanel.transform.position = new Vector3(valueX3, valueY3, 0);
            selectionPanel.transform.localScale = new Vector3(1, 0.9f, 0);

            ChestLostPanel.transform.position = new Vector3(valueX4, valueY4, 0);
            ChestLostPanel.transform.localScale = new Vector3(1, 0.9f, 0);
        }
    }
}
