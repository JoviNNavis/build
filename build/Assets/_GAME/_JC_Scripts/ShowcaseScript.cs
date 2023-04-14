using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowcaseScript : MonoBehaviour
{
    public GameObject oldImg, newImg, arrow;

    public GameObject bearModel, cube, pillar, smoke, img1, img2, button, img;

    public GameObject panelNew;

    void Start()
    {
        StartCoroutine(startGame());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            bear();
        }
    }

    IEnumerator startGame()
    {
        yield return new WaitForSeconds(1.5f);
        oldImg.SetActive(false);
        newImg.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        arrow.SetActive(true);
    }

    public void bear()
    {
        newImg.SetActive(false);
        arrow.SetActive(false);
        bearModel.SetActive(true);
        smoke.SetActive(true);
        cube.SetActive(true);
        pillar.SetActive(true);
        img1.SetActive(false);
        img2.SetActive(true);
        StartCoroutine(bearDisable());
    }

    public void close()
    {
        panelNew.SetActive(false);
       
    }

    IEnumerator bearDisable()
    {
        yield return new WaitForSeconds(2f);
        button.SetActive(false);
        img.SetActive(false);
        yield return new WaitForSeconds(1f);
        panelNew.SetActive(true);
    }

}
