using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ButtonManager : MonoBehaviour
{
   public KnifeScript1 playerKnife;

    public KnifeScript knifePlayer2;
    public KnifeScriptSword knifeplayerrr;
    public bool isaicolor;
    public bool changecolor;
    public AiScript ai;
    public Material BeforeSkybox;
    public MeshRenderer water;
    public GameObject cam, button, playButon, newButton;
    public bool isskin;
    public GameObject rewardPanel, levelPanel, chestPanel, ChestlosePanel, countdownPanel,countdownpanel_ad, SculptPanel, knifeSkniPanel;
    bool ispresed;
    public BonusKnifeScript bonusKnife;
    public GameObject pressbutton;
    public UpScript up;
    public bool isbelowlevel5;
    public GameObject newPanel1, newPanel2;
    public bool isstart;
    
    public GameObject arrow, oldImg, newImg;

    public alertScript alert;

    public GameObject defaultScroll, VIPScroll;

    public GameObject uiBar, Room, ShowCase;

    public Transform Dirlight;

    public AudioClip tapSound;

    void Start()
    {
        BeforeSkybox = RenderSettings.skybox;
        isstart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isbelowlevel5)
        {
            if (isstart)
            {
                if (mid.isskin)
                {
                    knifePlayer2.enabled = false;
                    knifeplayerrr.enabled = true;

                }
                else
                {
                    knifeplayerrr.enabled = false;
                    knifePlayer2.enabled = true;
                }
            }
        }
        else
        {
            return;
        }
    }

    public void gameStart1()  //lvl 1
    {
        SoundManger.soundctrl.playClip(tapSound);
        cam.SetActive(false);
        button.gameObject.SetActive(false);
        StartCoroutine(playButton1());
    }

    public void gameStart2()  //lvl 2
    {
        SoundManger.soundctrl.playClip(tapSound);
        cam.SetActive(false);
        button.gameObject.SetActive(true);
        StartCoroutine(playButton2());
    }

    public void gameStart11()   // lvl 3 to lvl 5
    {
        SoundManger.soundctrl.playClip(tapSound);
        cam.SetActive(false);
        button.gameObject.SetActive(false);
        StartCoroutine(playButton11());
    }

    public void gameStart3()  //bonus lvl
    {
        SoundManger.soundctrl.playClip(tapSound);
        cam.SetActive(false);
        button.gameObject.SetActive(false);
        StartCoroutine(playButton1());
    }

    public void gameStart4()   // lvl 7
    {
        SoundManger.soundctrl.playClip(tapSound);
        cam.SetActive(false);
        SculptPanel.gameObject.SetActive(false);
        button.gameObject.SetActive(true);
        StartCoroutine(playButton2());
        isstart = true;
    }

    public void gameStart5()   // lvl 8 to lvl 10
    {
        SoundManger.soundctrl.playClip(tapSound);
        cam.SetActive(false);
        button.gameObject.SetActive(false);
        SculptPanel.gameObject.SetActive(false);
        ispresed = true;
        isstart = true;

        StartCoroutine(playButton11());
    }

    public void Bonus()
    {
        SoundManger.soundctrl.playClip(tapSound);
        cam.SetActive(false);
        button.gameObject.SetActive(false);
        StartCoroutine(playButton3());
    }

    public void noThanks()
    {
        SoundManger.soundctrl.playClip(tapSound);
        rewardPanel.SetActive(false);
        levelPanel.SetActive(true);
        ChestlosePanel.SetActive(false);
    }

    public void Restart()
    {
        SoundManger.soundctrl.playClip(tapSound);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
    }

    public void BounsLvl()
    {
        SoundManger.soundctrl.playClip(tapSound);
        levelPanel.SetActive(false);
        chestPanel.SetActive(true);
    }

    public void Ok()
    {
        SoundManger.soundctrl.playClip(tapSound);
        newPanel1.SetActive(false);
    }

    public void Lose()
    {
        SoundManger.soundctrl.playClip(tapSound);
        ChestlosePanel.SetActive(false);
        levelPanel.SetActive(true);
    }

    public void newOk()
    {
        SoundManger.soundctrl.playClip(tapSound);
        newPanel2.SetActive(false);
        newPanel2.transform.DOScale(new Vector3(0, 0, 0), 0.1f);
        arrow.SetActive(true);
        oldImg.SetActive(false);
        newImg.SetActive(true);
    }

    public void ExtraRewardx2()
    {
        SoundManger.soundctrl.playClip(tapSound);
        rewardPanel.SetActive(false);
        cashScript.cashValue += 1050;
        countdownPanel.SetActive(true);
    }

    public void ExtraRewardx3()
    {
        SoundManger.soundctrl.playClip(tapSound);
        rewardPanel.SetActive(false);
        cashScript.cashValue += 1575;
        countdownPanel.SetActive(true);
    }

    public void ExtraRewardx5()
    {
        SoundManger.soundctrl.playClip(tapSound);
        rewardPanel.SetActive(false);
        cashScript.cashValue += 2625;
        countdownPanel.SetActive(true);
    }

    public void ExtraChest()
    {
        SoundManger.soundctrl.playClip(tapSound);
        ChestlosePanel.SetActive(false);
        countdownPanel.SetActive(true);
    }

    public void nextLvl()
    {
        SoundManger.soundctrl.playClip(tapSound);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void KnifeSkin()
    {
        SoundManger.soundctrl.playClip(tapSound);
        alert.changeColor();
        knifeSkniPanel.SetActive(true);
        Dirlight.transform.rotation = Quaternion.Euler(50, -30, 0);
        ShowCase.SetActive(false);
        button.SetActive(false);

    }

    public void defaultKnife()
    {
        SoundManger.soundctrl.playClip(tapSound);
        defaultScroll.SetActive(true);
        VIPScroll.SetActive(false);
    }

    public void VIPknife()
    {
        SoundManger.soundctrl.playClip(tapSound);
        VIPScroll.SetActive(true);
        defaultScroll.SetActive(false);
    }

    public void consoleButton()
    {
        SoundManger.soundctrl.playClip(tapSound);
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
        SoundManger.soundctrl.playClip(tapSound);
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
        SoundManger.soundctrl.playClip(tapSound);
        Dirlight.transform.rotation = Quaternion.Euler(160, -30, 0);
        cam.gameObject.SetActive(false);
        uiBar.SetActive(false);
        ShowCase.SetActive(true);
        Room.SetActive(false);
        knifeSkniPanel.SetActive(false);
        newButton.SetActive(false);
       // SceneManager.LoadScene(8);
    }
    public void knieskin(bool isclicked)
    {
        mid.isskin = isclicked;

        if (mid.isskin)
        {
            countdownpanel_ad.SetActive(true);

        }
        else
        {
            countdownpanel_ad.SetActive(false);
        }
       
    }

    
    public void showCaseforShowcaseLvl()
    {
        SoundManger.soundctrl.playClip(tapSound);
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
        ispresed = true;
        yield return new WaitForSeconds(2f);
        if (!isbelowlevel5)
        {


            if (mid.isskin)
            {
                knifePlayer2.enabled = false;
                knifeplayerrr.enabled = true;

            }
            else
            {
                knifeplayerrr.enabled = false;
                knifePlayer2.enabled = true;
            }

        }
        else
        {
            knifePlayer2.enabled = true;
        }
        ai.enabled = true;
   
    }

    IEnumerator playButton2()
    {
        yield return new WaitForSeconds(5.5f);
        if (!isbelowlevel5)
        {


            if (mid.isskin)
            {
                knifePlayer2.enabled = false;
                knifeplayerrr.enabled = true;

            }
            else
            {
                knifeplayerrr.enabled = false;
                knifePlayer2.enabled = true;
            }

        }
        else
        {
            knifePlayer2.enabled = true;
        }


        ai.enabled = true;
       
    }

    IEnumerator playButton3()
    {
        yield return new WaitForSeconds(2.25f);
        bonusKnife.enabled = true;
        up.enabled = true;
        //playButon.SetActive(true);
    }

}
