using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popupScript : MonoBehaviour
{
    public GameObject popBg, winTxt, shootbutton;
    public GameObject cam1, cam2;
    public GameObject stack;
    public BallSpinScript balls;
    bool isbolw;
    public newKnifeScript newKnife;
    public newKnifeScript1 newknife2;

    void Start()
    {
        popBg.SetActive(true);
        shootbutton.SetActive(false);
        StartCoroutine(win());
    }

    // Update is called once per frame
    void Update()
    {
        isbolw = FindObjectOfType<ButtonManager>().isbelowlevel5;
    }

    IEnumerator win()
    {
        yield return new WaitForSeconds(0.5f);
        winTxt.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        popBg.SetActive(false);
        winTxt.SetActive(false);
        cam1.SetActive(false);
        cam2.SetActive(true);
        yield return new WaitForSeconds(2f);
        if (isbolw)
        {
            newknife2.enabled = true;
        }
        else
        {
            newKnife.enabled = true;
        }
      
        stack.SetActive(true);
        yield return new WaitForSeconds(0.75f);
        balls.enabled = true;
    }
}
