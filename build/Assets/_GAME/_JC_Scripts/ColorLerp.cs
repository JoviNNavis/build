using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorLerp : MonoBehaviour
{
    public Text txt;

    //MeshRenderer renderer;

    [SerializeField] [Range(0, 10)] float lerpTime;

    [SerializeField] Color[] lerpColor;

    int colorIndex = 0;
    int len;

    float t = 0;

    void Start()
    {
        //renderer = GetComponent<MeshRenderer>();
        //txt = GetComponent<Text>();
        len = lerpColor.Length;
    }

    // Update is called once per frame
    void Update()
    {
        txt.material.color = Color.Lerp(txt.material.color, lerpColor[colorIndex], lerpTime * Time.deltaTime);

        //renderer.material.color = Color.Lerp(renderer.material.color, lerpColor[colorIndex], lerpTime * Time.deltaTime);

        t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);
        if(t > 0.9f)
        {
            t = 0;
            colorIndex++;
            colorIndex = (colorIndex >= lerpColor.Length) ? 0 : colorIndex;
        }
    }
}
