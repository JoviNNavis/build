using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class alertScript : MonoBehaviour
{
    private Image img;

    void Start()
    {
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeColor()
    {
        Color Changecolor = new Color(255, 255, 255, 0);
        img.material.color = Changecolor;
    }
}
