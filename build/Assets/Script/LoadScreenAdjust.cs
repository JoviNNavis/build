using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScreenAdjust : MonoBehaviour
{
    public RectTransform loadingPanel;

    public float values;

    //public float valueX1, valueY1;  //iphone se

    //public float valueX2, valueY2;  //iphone se 2gen

    //public float valueX3, valueY3;  //iphone mini, 11, 11 pro, 11 pro max, 12 pro, 12 pro max 

    //public float valueX4, valueY4;  //ipad pro 9.7

    //public float valueX5, valueY5;  //ipad 8th gen

    //public float valueX6, valueY6;  //ipad 4th gen

    //public float valueX7, valueY7;  //ipad pro 10.5

    //public float valueX8, valueY8;  //ipad pro 11 

    //public float valueX9, valueY9;  //ipad 12.9 


    private void Start()
    {

    }

    private void Update()
    {
        Debug.Log(Camera.main.aspect);

        //resChange();

        #region
        //if(Camera.main.aspect >= 0.4615385f && Camera.main.aspect <= 0.462203f)   //iphone 11, 12, etc..,
        //{
        //    loadingPanel.transform.localScale = new Vector3(1.78f, 2.17f, 1);
        //}

        //else if (Camera.main.aspect >= 0.5622189f && Camera.main.aspect <= 0.5633803f)  // iphone se 1gen se 2gen, 8plus
        //{
        //    loadingPanel.transform.localScale = new Vector3(1.78f, 1.78f, 1);
        //}
        //if (Camera.main.aspect == 0.5633803f || Camera.main.aspect == 0.5622189f || Camera.main.aspect == 0.5625f)
        //{
        //    loadingPanel.transform.localScale = new Vector3(1.78f, 1.78f, 1);
        //}

        //else if(Camera.main.aspect == 0.4620536f)
        //{
        //    loadingPanel.transform.localScale = new Vector3(1.78f, 2.17f, 1);
        //}

        //else if(Camera.main.aspect == 0.75f)
        //{
        //    loadingPanel.transform.localScale = new Vector3(1.78f, 1.35f, 1);
        //}

        //else if(Camera.main.aspect == 0.6949152f)
        //{
        //    loadingPanel.transform.localScale = new Vector3(1.78f, 1.45f, 1);
        //}
        #endregion

        if(Camera.main.aspect == 0.5633803f)
        {
            Debug.Log("iphone se 1gen");
            loadingPanel.transform.localScale = new Vector3(1.78f, 1.78f, 1);
        }

        else if (Camera.main.aspect == 0.5622189f)
        {
            Debug.Log("iphone se 2gen");
            loadingPanel.transform.localScale = new Vector3(1.78f, 1.78f, 1);
        }

        else if(Camera.main.aspect == 0.5625f)
        {
            Debug.Log("iphone 8 plus");
            loadingPanel.transform.localScale = new Vector3(1.78f, 1.78f, 1);
        }

        else if (Camera.main.aspect == 0.4620536f)
        {
            Debug.Log("iphone 11 and 11 pro max");
            loadingPanel.transform.localScale = new Vector3(1.78f, 2.17f, 1);
        }

        else if (Camera.main.aspect == 0.4615385f)
        {
            Debug.Log("iphone 12 mini");
            loadingPanel.transform.localScale = new Vector3(1.78f, 2.17f, 1);
        }

        else if (Camera.main.aspect == 0.4618227f)
        {
            Debug.Log("iphone 11 pro");
            loadingPanel.transform.localScale = new Vector3(1.78f, 2.17f, 1);
        }

        else if (Camera.main.aspect == 0.4620853f)
        {
            Debug.Log("iphone 12 pro");
            loadingPanel.transform.localScale = new Vector3(1.78f, 2.17f, 1);
        }

        else if (Camera.main.aspect == 0.462203f)
        {
            Debug.Log("iphone 12 pro max");
            loadingPanel.transform.localScale = new Vector3(1.78f, 2.17f, 1);
        }

        else if (Camera.main.aspect == 0.75f)
        {
            Debug.Log("Ipad 9.7 and 8th gen and pro 10.5");
            loadingPanel.transform.localScale = new Vector3(1.78f, 1.35f, 1);
        }

        else if (Camera.main.aspect == 0.749634f)
        {
            Debug.Log("Ipad pro 12.9");
            loadingPanel.transform.localScale = new Vector3(1.78f, 1.35f, 1);
        }

        else if (Camera.main.aspect == 0.6949152f)
        {
            Debug.Log("Ipad air 4th gen");
            loadingPanel.transform.localScale = new Vector3(1.78f, 1.45f, 1);
        }

        else if (Camera.main.aspect == 0.6984925f)
        {
            Debug.Log("Ipad pro 11");
            loadingPanel.transform.localScale = new Vector3(1.78f, 1.45f, 1);
        }
    }

    void resChange()
    {
        switch(Camera.main.aspect)
        {
            case 0.5633803f:
                Debug.Log("iphone se 1gen");
                loadingPanel.transform.localScale = new Vector3(1.78f, 1.78f, 1);
                break;

            case 0.5622189f:
                Debug.Log("iphone se 2gen");
                loadingPanel.transform.localScale = new Vector3(1.78f, 1.78f, 1);
                break;

            case 0.4620536f:
                Debug.Log("iphone 11 and 11 pro max");
                loadingPanel.transform.localScale = new Vector3(1.78f, 2.17f, 1);
                break;

            case 0.5625f:
                Debug.Log("iphone 8 plus");
                loadingPanel.transform.localScale = new Vector3(1.78f, 1.78f, 1);
                break;

            case 0.4615385f:
                Debug.Log("iphone 12 mini");
                loadingPanel.transform.localScale = new Vector3(1.78f, 2.17f, 1);
                break;

            case 0.4618227f:
                Debug.Log("iphone 11 pro");
                loadingPanel.transform.localScale = new Vector3(1.78f, 2.17f, 1);
                break;

            case 0.4620853f:
                Debug.Log("iphone 12 pro");
                loadingPanel.transform.localScale = new Vector3(1.78f, 2.17f, 1);
                break;

            case 0.462203f:
                Debug.Log("iphone 12 pro max");
                loadingPanel.transform.localScale = new Vector3(1.78f, 2.17f, 1);
                break;

            case 0.75f:
                Debug.Log("Ipad 9.7 and 8th gen and pro 10.5");
                loadingPanel.transform.localScale = new Vector3(1.78f, 1.35f, 1);
                break;

            case 0.6949152f:
                Debug.Log("Ipad air 4th gen");
                loadingPanel.transform.localScale = new Vector3(1.78f, 1.45f, 1);
                break;

            case 0.6984925f:
                Debug.Log("Ipad pro 11");
                loadingPanel.transform.localScale = new Vector3(1.78f, 1.45f, 1);
                break;

            case 0.749634f:
                Debug.Log("Ipad pro 12.9");
                loadingPanel.transform.localScale = new Vector3(1.78f, 1.35f, 1);
                break;

            default:
                Debug.Log("Default");
                loadingPanel.transform.localScale = new Vector3(1.78f, 1.78f, 1);
                break;
        }
    }
}
