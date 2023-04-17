using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ButtonManager : MonoBehaviour
{
    public KnifeScript1 playerKnife;

    public KnifeScript knifePlayer2;
    public bool isaicolor;
    public bool changecolor;
    public AiScript ai;
    public Material BeforeSkybox;
    public MeshRenderer water;
    public GameObject cam, button, playButon, newButton;

    public GameObject rewardPanel, levelPanel, chestPanel, ChestlosePanel, countdownPanel, SculptPanel, knifeSkniPanel;

    public BonusKnifeScript bonusKnife;

    public UpScript up;

    public GameObject newPanel1, newPanel2;

    public GameObject arrow, oldImg, newImg;

    public alertScript alert;

    public GameObject defaultScroll, VIPScroll;

    public GameObject uiBar, Room, ShowCase;

    public Transform Dirlight;

    void Start()
    {
        BeforeSkybox = RenderSettings.skybox;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gameStart1()  //lvl 1
    {
        cam.SetActive(false);
        button.gameObject.SetActive(false);
        StartCoroutine(playButton1());
    }

    public void gameStart2()  //lvl 2
    {
        cam.SetActive(false);
        button.gameObject.SetActive(true);
        StartCoroutine(playButton2());
    }

    public void gameStart11()   // lvl 3 to lvl 5
    {
        cam.SetActive(false);
        button.gameObject.SetActive(false);
        StartCoroutine(playButton11());
    }

    public void gameStart3()  //bonus lvl
    {
        cam.SetActive(false);
        button.gameObject.SetActive(false);
        StartCoroutine(playButton1());
    }

    public void gameStart4()   // lvl 7
    {
        cam.SetActive(false);
        SculptPanel.gameObject.SetActive(false);
        button.gameObject.SetActive(true);
        StartCoroutine(playButton2());
    }

    public void gameStart5()   // lvl 8 to lvl 10
    {
        cam.SetActive(false);
        button.gameObject.SetActive(false);
        SculptPanel.gameObject.SetActive(false);
        StartCoroutine(playButton11());
    }

    public void Bonus()
    {
        cam.SetActive(false);
        button.gameObject.SetActive(false);
        StartCoroutine(playButton3());
    }

    public void noThanks()
    {
        rewardPanel.SetActive(false);
        levelPanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
    }

    public void BounsLvl()
    {
        levelPanel.SetActive(false);
        chestPanel.SetActive(true);
    }

    public void Ok()
    {
        newPanel1.SetActive(false);
    }

    public void Lose()
    {
        ChestlosePanel.SetActive(false);
        levelPanel.SetActive(true);
    }

    public void newOk()
    {
        newPanel2.SetActive(false);
        newPanel2.transform.DOScale(new Vector3(0, 0, 0), 0.1f);
        arrow.SetActive(true);
        oldImg.SetActive(false);
        newImg.SetActive(true);
    }

    public void ExtraRewardx2()
    {
        rewardPanel.SetActive(false);
        cashScript.cashValue += 1050;
        countdownPanel.SetActive(true);
    }

    public void ExtraRewardx3()
    {
        rewardPanel.SetActive(false);
        cashScript.cashValue += 1575;
        countdownPanel.SetActive(true);
    }

    public void ExtraRewardx5()
    {
        rewardPanel.SetActive(false);
        cashScript.cashValue += 2625;
        countdownPanel.SetActive(true);
    }

    public void ExtraChest()
    {
        ChestlosePanel.SetActive(false);
        countdownPanel.SetActive(true);
    }

    public void nextLvl()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void KnifeSkin()
    {
        alert.changeColor();
        knifeSkniPanel.SetActive(true);
        Dirlight.transform.rotation = Quaternion.Euler(50, -30, 0);
        ShowCase.SetActive(false);
        button.SetActive(false);

    }

    public void defaultKnife()
    {
        defaultScroll.SetActive(true);
        VIPScroll.SetActive(false);
    }

    public void VIPknife()
    {
        VIPScroll.SetActive(true);
        defaultScroll.SetActive(false);
    }

    public void consoleButton()
    {
        knifeSkniPanel.SetActive(false);
        Room.SetActive(false);
        uiBar.SetActive(true);
        cam.gameObject.SetActive(true);
        ShowCase.SetActive(false);
        Dirlight.transform.rotation = Quaternion.Euler(50, -30, 0);
        newButton.SetActive(true);
    }

    public void EnableSculptRoom()
    {
        Room.SetActive(true);
        uiBar.SetActive(false);
        cam.gameObject.SetActive(false);
        Dirlight.transform.rotation = Quaternion.Euler(50, -30, 0);
        ShowCase.SetActive(false);
        knifeSkniPanel.SetActive(false);
        newButton.SetActive(false);

    }

    public void statueRoom()
    {
        Dirlight.transform.rotation = Quaternion.Euler(160, -30, 0);
        cam.gameObject.SetActive(false);
        uiBar.SetActive(false);
        ShowCase.SetActive(true);
        Room.SetActive(false);
        knifeSkniPanel.SetActive(false);
        newButton.SetActive(false);

    }

    public void showCaseforShowcaseLvl()
    {
        knifeSkniPanel.SetActive(false);
        Room.SetActive(false);
    }

    IEnumerator playButton1()
    {
        yield return new WaitForSeconds(2f);
        playerKnife.enabled = true;
        //playButon.SetActive(true);
    }

    IEnumerator playButton11()
    {
        yield return new WaitForSeconds(2f);
        knifePlayer2.enabled = true;
        ai.enabled = true;
        //playButon.SetActive(true);
    }

    IEnumerator playButton2()
    {
        yield return new WaitForSeconds(5.5f);
        knifePlayer2.enabled = true;
        ai.enabled = true;
        //playButon.SetActive(true);
    }

    IEnumerator playButton3()
    {
        yield return new WaitForSeconds(2.25f);
        bonusKnife.enabled = true;
        up.enabled = true;
        //playButon.SetActive(true);
    }

}
