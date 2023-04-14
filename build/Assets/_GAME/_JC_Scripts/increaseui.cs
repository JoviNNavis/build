using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class increaseui : MonoBehaviour
{
    public GameObject[] uibuttons;
    public Vector3 skinscale, consolescale, sculptingscale, showcasingscale;
    void Start()
    {
        uibuttons = GameObject.FindGameObjectsWithTag("uibuttons");
        skinscale = new Vector3(uibuttons[0].transform.localScale.x, uibuttons[0].transform.localScale.y, uibuttons[0].transform.localScale.z);
        consolescale = new Vector3(uibuttons[1].transform.localScale.x, uibuttons[1].transform.localScale.y, uibuttons[1].transform.localScale.z);
        sculptingscale = new Vector3(uibuttons[2].transform.localScale.x, uibuttons[2].transform.localScale.y, uibuttons[2].transform.localScale.z);
        showcasingscale = new Vector3(uibuttons[3].transform.localScale.x, uibuttons[3].transform.localScale.y, uibuttons[3].transform.localScale.z);

    }
    public void skinbutton()
    {
        uibuttons[0].transform.localScale += new Vector3(0.15f, 0.15f, 0.15f);
        uibuttons[1].transform.localScale = consolescale;
        uibuttons[2].transform.localScale = sculptingscale;
        uibuttons[3].transform.localScale = showcasingscale;



    }
    public void console()
    {
        uibuttons[0].transform.localScale = skinscale;
        uibuttons[1].transform.localScale += new Vector3(0.15f, 0.15f, 0.15f);
        uibuttons[2].transform.localScale = sculptingscale;
        uibuttons[3].transform.localScale = showcasingscale;
    }

    public void sculpting()
    {
        uibuttons[0].transform.localScale = skinscale;
        uibuttons[1].transform.localScale = consolescale;
        uibuttons[2].transform.localScale += new Vector3(0.15f, 0.15f, 0.15f);
        uibuttons[3].transform.localScale = showcasingscale;
    }
    public void showcasing()
    {
        uibuttons[0].transform.localScale = skinscale;
        uibuttons[1].transform.localScale = consolescale;
        uibuttons[2].transform.localScale = sculptingscale;
        uibuttons[3].transform.localScale += new Vector3(0.15f, 0.15f, 0.15f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
