using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorScript : MonoBehaviour
{
    public bool belowlevel5;
    public bool spikelevel;
    //public Color before_color1, before_color2;
    //public Color after_color1, after_color2;
    public Skybox original;
    public Color up_color_level5, down_color_level5;
    public Color up_color_level1, down_color_level1;
    public Material up, down;
    public Material spikemat;
    public Color beforecolor, aftercolor;
    [SerializeField]
    public Color fog;
    [SerializeField]
    public Color after_fog;

    void Start()
    {

        if (belowlevel5)
        {
            fog = new Color32(153, 242, 255, 200);
        }
        else
        {
            fog = new Color32(207, 207, 207, 255);
        }
       
        after_fog = new Color32(165, 148, 255, 206);
        up_color_level5 = new Color32(185, 99, 34, 255);
        down_color_level5 = new Color32(223, 144, 82, 255);
        up_color_level1 = new Color32(180, 180, 180, 255);
        down_color_level1 = new Color32(178, 178, 178, 255);
        beforecolor = new Color32(255, 0, 0, 180);
        aftercolor = new Color32(168, 255, 133, 180);
        spikemat.color = beforecolor;
        spikelevel = false;
        RenderSettings.fogColor = fog;
    }

   
    void Update()
    {
        if (belowlevel5)
        {
            up.color = up_color_level1;
            down.color = down_color_level1;
        }
        else
        {
            up.color = up_color_level5;
            down.color = down_color_level5;
        }
    }
}
