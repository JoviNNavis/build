using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangerSkybox : MonoBehaviour
{
    public Material currSkybox, newSkybox;

    void Start()
    {
        RenderSettings.skybox = currSkybox;
        RenderSettings.skybox = newSkybox;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {

        }
    }
}
