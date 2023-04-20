using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonscaler : MonoBehaviour
{
    public GameObject knife_button;
    public GameObject console_button;
    public GameObject tool_button;
    public GameObject statue_button;
    public Vector3 buttonscale, consolescale, toolscale, statuescale;
    public Vector3 scaler;

    void Start()
    {
        buttonscale = knife_button.transform.localScale;
        consolescale = console_button.transform.localScale;
        toolscale = tool_button.transform.localScale;
        statuescale = statue_button.transform.localScale;
        scaler = new Vector3(0.1f, 0.02f, 0);

    }
    public void knifebutton()
    {
        knife_button.transform.localScale += scaler;
        console_button.transform.localScale = consolescale;
        tool_button.transform.localScale = toolscale;
        statue_button.transform.localScale = statuescale;

    }
    public void consolebutton()
    {
        knife_button.transform.localScale = buttonscale;
        console_button.transform.localScale += scaler;
        tool_button.transform.localScale = toolscale;
        statue_button.transform.localScale = statuescale;
    }
    public void toolbutton()
    {
        knife_button.transform.localScale = buttonscale;
        console_button.transform.localScale = consolescale;
        tool_button.transform.localScale += scaler;
        statue_button.transform.localScale = statuescale;
    }
    public void statuebutton()
    {
        knife_button.transform.localScale = buttonscale;
        console_button.transform.localScale = consolescale;
        tool_button.transform.localScale = toolscale;
        statue_button.transform.localScale += scaler;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
