using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowcaseScript : MonoBehaviour
{
    public GameObject oldImg, newImg, arrow;

    public GameObject bearModel, cube, pillar, smoke, img1, img2, button, img;
    public GameObject blackscreen;
    public GameObject countdownpanel;
    public GameObject panelNew;
    public GameObject downbar;
    public Button _bear;

    public GameObject settingsPanel;

    public GameObject hapticON, hapticOFF;

    public GameObject soundON, soundOFF;
    void Start()
    {
        downbar.SetActive(false);
        StartCoroutine(startGame());
        _bear.interactable = false;
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
        downbar.SetActive(true);
        cube.SetActive(true);
        pillar.SetActive(true);
        img1.SetActive(false);
       
        img2.SetActive(true);
        StartCoroutine(bearDisable());
    }
   public void nextswordlevel(bool isclicked)
    {

       mid.isskin = isclicked;
        StartCoroutine(countdownpnl());
    }
    public void _pillar()
    {
        smoke.SetActive(true);
        cube.SetActive(true);
        smoke.SetActive(true);
        pillar.SetActive(true);
        img1.SetActive(false);
        _bear.interactable = true;

    }
    public void close()
    {
        panelNew.SetActive(false);
        blackscreen.SetActive(false);
    }

    public void openSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void closeSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void hapticon()
    {
        hapticOFF.SetActive(false);
        hapticON.SetActive(true);
    }

    public void hapticoff()
    {
        hapticOFF.SetActive(true);
        hapticON.SetActive(false);
    }

    public void soundcon()
    {
        soundOFF.SetActive(false);
        soundON.SetActive(true);
    }

    public void soundcoff()
    {
        soundOFF.SetActive(true);
        soundON.SetActive(false);
    }

    IEnumerator bearDisable()
    {
        yield return new WaitForSeconds(2f);
        button.SetActive(false);
        img.SetActive(false);
        yield return new WaitForSeconds(1f);
        panelNew.SetActive(true);
    }
    IEnumerator countdownpnl()
    {
        countdownpanel.SetActive(true);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

}
