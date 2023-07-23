using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using AppCentralAPI;


public class ButtonManager : MonoBehaviour
{
    private int currSceneIndex;
    [SerializeField]
   private int index;
    private int loadSceneIndex;

    public KnifeScript1 playerKnife;

    public KnifeScript knifePlayer2;
    public KnifeScriptSword knifeplayerrr;
    
    public RayBall ballRay;
    public bool ishaptic;
    public bool isaicolor;
    public bool changecolor;
    public AiScript ai;
    public Material BeforeSkybox;
    public MeshRenderer water;
    public GameObject cam, button, playButon, newButton;
    public bool isskin;
    public GameObject rewardPanel, levelPanel, failedlevelPanel, winFailPanel, failWinPanel, newLevelPanel, chestPanel, ChestlosePanel, OldcountdownPanel, NewcountdownPanel, countdownpanel_ad, SculptPanel, knifeSkniPanel;
    
    public GameObject newCountpanel2, newCountpanel3;

    public GameObject win_win_FailPanel, fail_win_FailPanel, win_fail_FailPanel, fail_fail_FailPanel;

    public GameObject f_w_w_winPanel, w_f_w_winPanel, w_w_f_winPanel, f_f_f_winPanel, 
        w_f_f_winPanel, f_f_w_winPanel, f_w_f_winPanel;

    public GameObject w_w_w_failPanel, f_w_w_failPanel, w_f_w_failPanel, w_w_f_failPanel,
        w_f_f_failPanel, f_f_w_failPanel, f_w_f_failPanel;

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
    public GameObject settingsbutton;
    public GameObject uiBar, Room, ShowCase;

    public Transform Dirlight;
    public GameObject _15;
    public AudioClip tapSound;

    public AudioSource Plysounds, Aisounds;

    public GameObject settingPanel;

    public GameObject hapticON, hapticOFF, soundON, soundOFF;

    public GameObject retryButton;

    public NewFailScript newFail;
    public Animator text;

    public Slider playerSlider;
    public Image playerImage;

    public float winValue;

    public BallSpinScript spinBall;

    public GameObject adPanel1, adPanel2, adPanel3, adPanel4, adPanel5, adPanel6, adPanel7, adPanel8;

    void Start()
    {
        index = 0;
        BeforeSkybox = RenderSettings.skybox;

        if(SceneManager.GetActiveScene().buildIndex ==6)
        {
            AppCentralGameAnalyticsEvents.SendLevelProgressionEvent(GameAnalyticsSDK.GAProgressionStatus.Start, Progresion01.bonus, Progresion02.@default, index);
        }
        else
        {
            AppCentralGameAnalyticsEvents.SendLevelProgressionEvent(GameAnalyticsSDK.GAProgressionStatus.Start, Progresion01.level, Progresion02.@default, index);

        }

        isstart = false;
        settingsbutton = GameObject.FindGameObjectWithTag("sb");
        //nextLvl();
        hapticOff();
        _15.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //FindObjectOfType<newKnifeScript>().skin();
        //else
        //{
        //    return;
        //}
    }
    public void appce()
    {
        AppCentral.StartPlay();
    }
    public void gameStart1()  //lvl 1
    {
        SoundManger.soundctrl.playClip(tapSound);
        retryButton.SetActive(true);
        cam.SetActive(false);
        settingsbutton.SetActive(false);
        button.gameObject.SetActive(false);
        appce();
        StartCoroutine(playButton1());
     //   currSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
       // PlayerPrefs.SetInt("SavedScene", currSceneIndex);
      //  GameData.SetCurrentScene(currSceneIndex);
    }

    public void gameStart2()  //lvl 2
    {
        SoundManger.soundctrl.playClip(tapSound);
        retryButton.SetActive(true);
        cam.SetActive(false);
        settingsbutton.SetActive(false);
        button.gameObject.SetActive(true);
        appce();
        StartCoroutine(playButton2());
       // currSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
       // PlayerPrefs.SetInt("SavedScene", currSceneIndex);
      //  GameData.SetCurrentScene(currSceneIndex);
    }

    public void gameStart11()   // lvl 3 to lvl 5
    {
        SoundManger.soundctrl.playClip(tapSound);
        retryButton.SetActive(true);
        settingsbutton.SetActive(false);
        cam.SetActive(false);
        button.gameObject.SetActive(false);
        appce();

        StartCoroutine(playButton11());
      //  currSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
      //  GameData.SetCurrentScene(currSceneIndex);
        //PlayerPrefs.SetInt("SavedScene", currSceneIndex);
    }

    public void gameStart3()  //bonus lvl
    {
        SoundManger.soundctrl.playClip(tapSound);
        retryButton.SetActive(true);
        cam.SetActive(false);
        button.gameObject.SetActive(false);
        appce();

        StartCoroutine(playButton1());
       // currSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
    //    PlayerPrefs.SetInt("SavedScene", currSceneIndex);
     //   GameData.SetCurrentScene(currSceneIndex);
    }

    public void gameStart4()   // lvl 7
    {
        SoundManger.soundctrl.playClip(tapSound);
        retryButton.SetActive(true);
        settingsbutton.SetActive(false);
        cam.SetActive(false);
        SculptPanel.gameObject.SetActive(false);
        button.gameObject.SetActive(true);
        appce();

        StartCoroutine(playButton2());
        isstart = true;
       // currSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        //PlayerPrefs.SetInt("SavedScene", currSceneIndex);
      //  GameData.SetCurrentScene(currSceneIndex);
    }

    public void gameStart5()   // lvl 8 to lvl 10
    {
        SoundManger.soundctrl.playClip(tapSound);
        retryButton.SetActive(true);
        cam.SetActive(false);
        settingsbutton.SetActive(false);
        button.gameObject.SetActive(false);
        SculptPanel.gameObject.SetActive(false);
        appce();

        ispresed = true;
        isstart = true;
        StartCoroutine(playButton11());
       // currSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        //PlayerPrefs.SetInt("SavedScene", currSceneIndex);
      //  GameData.SetCurrentScene(currSceneIndex);
    }

    public void Bonus()
    {
        SoundManger.soundctrl.playClip(tapSound);
        cam.SetActive(false);
        button.gameObject.SetActive(false);
        StartCoroutine(playButton3());
    }

    public void noThankslvl2()
    {
        SoundManger.soundctrl.playClip(tapSound);
        levelPanel.SetActive(true);
        Destroy(ChestlosePanel);
        Destroy(rewardPanel);
    }

    public void noThankslvl3()
    {
        SoundManger.soundctrl.playClip(tapSound);

        if(mid.lvl2Fail == true)
        {
            failWinPanel.SetActive(true);
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }

        if (mid.lvl2Fail == false)
        {
            levelPanel.SetActive(true);
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }
    }

    public void noThankslvl8()
    {
        SoundManger.soundctrl.playClip(tapSound);

        if (mid.lvl8fail == true)
        {
            failWinPanel.SetActive(true);
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }

        if (mid.lvl8fail == false)
        {
            levelPanel.SetActive(true);
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }
    }

    public void noThankslvl4()
    {
        SoundManger.soundctrl.playClip(tapSound);

        if (mid.lvl2Fail == false && mid.lvl3Fail == false && lvl4Fail.lvl4fail == false)
        {
            levelPanel.SetActive(true);            //win_win_win
            Debug.Log("WIN_WIN_WIN");
            Destroy(ChestlosePanel);            
            Destroy(rewardPanel);
        }

        if (mid.lvl2Fail == false && mid.lvl3Fail == true && lvl4Fail.lvl4fail == false)
        {
            winFailPanel.SetActive(true);         //win_fail_win
            Debug.Log("WIN_FAIL_WIN");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }

        if (mid.lvl2Fail == true && mid.lvl3Fail == false && lvl4Fail.lvl4fail == false)
        {
            failWinPanel.SetActive(true);     //fail_win_win
            Debug.Log("FAIL_WIN_WIN");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }

        if (mid.lvl2Fail == true && mid.lvl3Fail == true && lvl4Fail.lvl4fail == false)
        {
            newLevelPanel.SetActive(true);     //fail_fail_win
            Debug.Log("FAIL_FAIL_WIN");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }
    }

    public void noThankslvl9()
    {
        SoundManger.soundctrl.playClip(tapSound);

        if (mid.lvl7fail == false && mid.lvl8fail == false && mid.lvl9fail == false)
        {
            levelPanel.SetActive(true);            //win_win_win
            Debug.Log("WIN_WIN_WIN");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }

        if (mid.lvl7fail == false && mid.lvl8fail == true && mid.lvl9fail == false)
        {
            winFailPanel.SetActive(true);         //win_fail_win
            Debug.Log("WIN_FAIL_WIN");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }

        if (mid.lvl7fail == true && mid.lvl8fail == false && mid.lvl9fail == false)
        {
            failWinPanel.SetActive(true);     //fail_win_win
            Debug.Log("FAIL_WIN_WIN");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }

        if (mid.lvl7fail == true && mid.lvl8fail == true && mid.lvl9fail == false)
        {
            newLevelPanel.SetActive(true);     //fail_fail_win
            Debug.Log("FAIL_FAIL_WIN");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }
    }

    public void noThankslvl5()
    {
        SoundManger.soundctrl.playClip(tapSound);

        if (mid.lvl2Fail == false && mid.lvl3Fail == false && lvl4Fail.lvl4fail == false && mid.lvl5fail == false)
        {
            levelPanel.SetActive(true);            //win_win_win_win
            Debug.Log("WIN_WIN_WIN_WIN");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }

        if (mid.lvl2Fail == true && mid.lvl3Fail == false && lvl4Fail.lvl4fail == false && mid.lvl5fail == false)
        {
            f_w_w_winPanel.SetActive(true);         //fail_win_win_win
            Debug.Log("FAIL_WIN_WIN_WIN");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }

        if (mid.lvl2Fail == false && mid.lvl3Fail == true && lvl4Fail.lvl4fail == false && mid.lvl5fail == false)
        {
            w_f_w_winPanel.SetActive(true);     //win_fail_win_win
            Debug.Log("FAIL_WIN_WIN");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }

        if (mid.lvl2Fail == false && mid.lvl3Fail == false && lvl4Fail.lvl4fail == true && mid.lvl5fail == false)
        {
            w_w_f_winPanel.SetActive(true);     //win_win_fail_win
            Debug.Log("WIN_WIN_FAIL_WIN");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }

        if (mid.lvl2Fail == true && mid.lvl3Fail == true && lvl4Fail.lvl4fail == true && mid.lvl5fail == false)
        {
            f_f_f_winPanel.SetActive(true);     //fail_fail_fail_win
            Debug.Log("FAIL_FAIL_FAIL_WIN");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }

        if (mid.lvl2Fail == false && mid.lvl3Fail == true && lvl4Fail.lvl4fail == true && mid.lvl5fail == false)
        {
            w_f_f_winPanel.SetActive(true);     //win_fail_fail_win
            Debug.Log("WIN_FAIL_FAIL_WIN");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }

        if (mid.lvl2Fail == true && mid.lvl3Fail == true && lvl4Fail.lvl4fail == false && mid.lvl5fail == false)
        {
            f_f_w_winPanel.SetActive(true);     //fail_fail_win_win
            Debug.Log("FAIL_FAIL_WIN_WIN");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }

        if (mid.lvl2Fail == true && mid.lvl3Fail == false && lvl4Fail.lvl4fail == true && mid.lvl5fail == false)
        {
            f_w_f_winPanel.SetActive(true);     //fail_win_fail_win
            Debug.Log("FAIL_WIN_FAIL_WIN");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }
    }

    public void noThankslvl10()
    {
        SoundManger.soundctrl.playClip(tapSound);

        if (mid.lvl7fail == false && mid.lvl8fail == false && mid.lvl9fail == false && mid.lvl10fail == false)
        {
            levelPanel.SetActive(true);            //win_win_win_win
            Debug.Log("WIN_WIN_WIN_WIN");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }

        if (mid.lvl7fail == true && mid.lvl8fail == false && mid.lvl9fail == false && mid.lvl10fail == false)
        {
            f_w_w_winPanel.SetActive(true);         //fail_win_win_win
            Debug.Log("FAIL_WIN_WIN_WIN");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }

        if (mid.lvl7fail == false && mid.lvl8fail == true && mid.lvl9fail == false && mid.lvl10fail == false)
        {
            w_f_w_winPanel.SetActive(true);     //win_fail_win_win
            Debug.Log("FAIL_WIN_WIN");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }

        if (mid.lvl7fail == false && mid.lvl8fail == false && mid.lvl9fail == true && mid.lvl10fail == false)
        {
            w_w_f_winPanel.SetActive(true);     //win_win_fail_win
            Debug.Log("WIN_WIN_FAIL_WIN");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }

        if (mid.lvl7fail == true && mid.lvl8fail == true && mid.lvl9fail == true && mid.lvl10fail == false)
        {
            f_f_f_winPanel.SetActive(true);     //fail_fail_fail_win
            Debug.Log("FAIL_FAIL_FAIL_WIN");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }

        if (mid.lvl7fail == false && mid.lvl8fail == true && mid.lvl9fail == true && mid.lvl10fail == false)
        {
            w_f_f_winPanel.SetActive(true);     //win_fail_fail_win
            Debug.Log("WIN_FAIL_FAIL_WIN");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }

        if (mid.lvl7fail == true && mid.lvl8fail == true && mid.lvl9fail == false && mid.lvl10fail == false)
        {
            f_f_w_winPanel.SetActive(true);     //fail_fail_win_win
            Debug.Log("FAIL_FAIL_WIN_WIN");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }

        if (mid.lvl7fail == true && mid.lvl8fail == false && mid.lvl9fail == true && mid.lvl10fail == false)
        {
            f_w_f_winPanel.SetActive(true);     //fail_win_fail_win
            Debug.Log("FAIL_WIN_FAIL_WIN");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }
    }

    public void Restart()
    {
        SoundManger.soundctrl.playClip(tapSound);
        AppCentral.SetLevelStartID(currSceneIndex);

        SceneManager.LoadScene(Application.loadedLevelName);
    }

    public void BounsLvl()
    {
        SoundManger.soundctrl.playClip(tapSound);
    //    levelPanel.SetActive(false);
        Destroy(levelPanel);

        chestPanel.SetActive(true);
    }
    public void plus15()
    {
        FindObjectOfType<KnifeScript>().plus15();
        StartCoroutine(panelDis());
        playerSlider.value += winValue;
        playerImage.fillAmount += winValue;
        countdownpanel_ad.SetActive(true);
        _15.SetActive(true);
    }
    public void Ok()
    {
        SoundManger.soundctrl.playClip(tapSound);
        newPanel1.SetActive(false);
    }

    public void loselvl2()
    {
        SoundManger.soundctrl.playClip(tapSound);
        mid.lvl2Fail = true;
        failedlevelPanel.SetActive(true);
        Destroy(ChestlosePanel);
    }

    public void loselvl7()
    {
        SoundManger.soundctrl.playClip(tapSound);
        mid.lvl7fail = true;
        failedlevelPanel.SetActive(true);
        Destroy(ChestlosePanel);
    }

    public void Loselvl3()
    {
        SoundManger.soundctrl.playClip(tapSound);

        if(mid.lvl2Fail == true && mid.lvl3Fail == false)
        {
            failWinPanel.SetActive(true);
            Destroy(ChestlosePanel);
        }
        
        if(mid.lvl2Fail == true && mid.lvl3Fail == true)
        {
            failedlevelPanel.SetActive(true);
            Destroy(ChestlosePanel);
        }

        if (mid.lvl2Fail == false && mid.lvl3Fail == true)
        {
            winFailPanel.SetActive(true);
            Destroy(ChestlosePanel);
        }
    }

    public void Loselvl8()
    {
        SoundManger.soundctrl.playClip(tapSound);

        if (mid.lvl7fail == true && mid.lvl8fail == false)
        {
            failWinPanel.SetActive(true);
            Destroy(ChestlosePanel);
        }

        if (mid.lvl7fail == true && mid.lvl8fail == true)
        {
            failedlevelPanel.SetActive(true);
            Destroy(ChestlosePanel);
        }

        if (mid.lvl2Fail == false && mid.lvl3Fail == true)
        {
            winFailPanel.SetActive(true);
            Destroy(ChestlosePanel);
        }
    }

    public void Loselvl4()
    {
        SoundManger.soundctrl.playClip(tapSound);

        if (mid.lvl2Fail == false && mid.lvl3Fail == false && lvl4Fail.lvl4fail == true)
        {
            win_win_FailPanel.SetActive(true);      //win_win_fail
            Debug.Log("WIN_WIN_FAIL");
            Destroy(ChestlosePanel);
        }

        if (mid.lvl2Fail == true && mid.lvl3Fail == false && lvl4Fail.lvl4fail == true)
        {
            fail_win_FailPanel.SetActive(true);     //fail_win_fail
            Debug.Log("FAIL_WIN_FAIL");
            Destroy(ChestlosePanel);
        }

        if (mid.lvl2Fail == false && mid.lvl3Fail == true && lvl4Fail.lvl4fail == true)
        {
            win_fail_FailPanel.SetActive(true);           //win_fail_fail
            Debug.Log("WIN_FAIL_FAIL");
            Destroy(ChestlosePanel);
        }

        if (mid.lvl2Fail == true && mid.lvl3Fail == true && lvl4Fail.lvl4fail == true)
        {
            fail_fail_FailPanel.SetActive(true);           //fail_fail_fail
            Debug.Log("FAIL_FAIL_FAIL");
            Destroy(ChestlosePanel);
        }
    }

    public void Loselvl9()
    {
        SoundManger.soundctrl.playClip(tapSound);

        if (mid.lvl7fail == false && mid.lvl8fail == false && mid.lvl9fail == true)
        {
            win_win_FailPanel.SetActive(true);      //win_win_fail
            Debug.Log("WIN_WIN_FAIL");
            Destroy(ChestlosePanel);
        }

        if (mid.lvl7fail == true && mid.lvl8fail == false && mid.lvl9fail == true)
        {
            fail_win_FailPanel.SetActive(true);     //fail_win_fail
            Debug.Log("FAIL_WIN_FAIL");
            Destroy(ChestlosePanel);
        }

        if (mid.lvl7fail == false && mid.lvl8fail == true && mid.lvl9fail == true)
        {
            win_fail_FailPanel.SetActive(true);           //win_fail_fail
            Debug.Log("WIN_FAIL_FAIL");
            Destroy(ChestlosePanel);
        }

        if (mid.lvl7fail == true && mid.lvl8fail == true && mid.lvl9fail == true)
        {
            fail_fail_FailPanel.SetActive(true);           //fail_fail_fail
            Debug.Log("FAIL_FAIL_FAIL");
            Destroy(ChestlosePanel);
        }
    }

    public void Loselvl5()
    {
        SoundManger.soundctrl.playClip(tapSound);

        if (mid.lvl2Fail == false && mid.lvl3Fail == false && lvl4Fail.lvl4fail == false && mid.lvl5fail == true)
        {
            w_w_w_failPanel.SetActive(true);            //win_win_win_fail
            Debug.Log("WIN_WIN_WIN_FAIL");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }

        if (mid.lvl2Fail == true && mid.lvl3Fail == false && lvl4Fail.lvl4fail == false && mid.lvl5fail == true)
        {
            f_w_w_failPanel.SetActive(true);         //fail_win_win_fail
            Debug.Log("FAIL_WIN_WIN_FAIL");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }

        if (mid.lvl2Fail == false && mid.lvl3Fail == true && lvl4Fail.lvl4fail == false && mid.lvl5fail == true)
        {
            w_f_w_failPanel.SetActive(true);     //win_fail_win_fail
            Debug.Log("FAIL_WIN_WIN_FAIL");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }

        if (mid.lvl2Fail == false && mid.lvl3Fail == false && lvl4Fail.lvl4fail == true && mid.lvl5fail == true)
        {
            w_w_f_failPanel.SetActive(true);     //win_win_fail_fail
            Debug.Log("WIN_WIN_FAIL_FAIL");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }

        if (mid.lvl2Fail == true && mid.lvl3Fail == true && lvl4Fail.lvl4fail == true && mid.lvl5fail == true)
        {
            failedlevelPanel.SetActive(true);     //fail_fail_fail_fail
            Debug.Log("FAIL_FAIL_FAIL_FAIL");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }

        if (mid.lvl2Fail == false && mid.lvl3Fail == true && lvl4Fail.lvl4fail == true && mid.lvl5fail == true)
        {
            w_f_f_failPanel.SetActive(true);     //win_fail_fail_fail
            Debug.Log("WIN_FAIL_FAIL_FAIL");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }

        if (mid.lvl2Fail == true && mid.lvl3Fail == true && lvl4Fail.lvl4fail == false && mid.lvl5fail == true)
        {
            f_f_w_failPanel.SetActive(true);     //fail_fail_win_fail
            Debug.Log("FAIL_FAIL_WIN_WIN");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }

        if (mid.lvl2Fail == true && mid.lvl3Fail == false && lvl4Fail.lvl4fail == true && mid.lvl5fail == true)
        {
            f_w_f_failPanel.SetActive(true);     //fail_win_fail_fail
            Debug.Log("FAIL_WIN_FAIL_FAIL");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }
    }

    public void Loselvl10()
    {
        SoundManger.soundctrl.playClip(tapSound);

        if (mid.lvl7fail == false && mid.lvl8fail == false && mid.lvl9fail == false && mid.lvl10fail == true)
        {
            w_w_w_failPanel.SetActive(true);            //win_win_win_fail
            Debug.Log("WIN_WIN_WIN_FAIL");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }

        if (mid.lvl7fail == true && mid.lvl8fail == false && mid.lvl9fail == false && mid.lvl10fail == true)
        {
            f_w_w_failPanel.SetActive(true);         //fail_win_win_fail
            Debug.Log("FAIL_WIN_WIN_FAIL");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }

        if (mid.lvl7fail == false && mid.lvl8fail == true && mid.lvl9fail == false && mid.lvl10fail == true)
        {
            w_f_w_failPanel.SetActive(true);     //win_fail_win_fail
            Debug.Log("FAIL_WIN_WIN_FAIL");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }

        if (mid.lvl7fail == false && mid.lvl8fail == false && mid.lvl9fail == true && mid.lvl10fail == true)
        {
            w_w_f_failPanel.SetActive(true);     //win_win_fail_fail
            Debug.Log("WIN_WIN_FAIL_FAIL");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }

        if (mid.lvl7fail == true && mid.lvl8fail == true && mid.lvl9fail == true && mid.lvl10fail == true)
        {
            failedlevelPanel.SetActive(true);     //fail_fail_fail_fail
            Debug.Log("FAIL_FAIL_FAIL_FAIL");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }

        if (mid.lvl7fail == false && mid.lvl8fail == true && mid.lvl9fail == true && mid.lvl10fail == true)
        {
            w_f_f_failPanel.SetActive(true);     //win_fail_fail_fail
            Debug.Log("WIN_FAIL_FAIL_FAIL");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }

        if (mid.lvl7fail == true && mid.lvl8fail == true && mid.lvl9fail == false && mid.lvl10fail == true)
        {
            f_f_w_failPanel.SetActive(true);     //fail_fail_win_fail
            Debug.Log("FAIL_FAIL_WIN_WIN");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }

        if (mid.lvl7fail == true && mid.lvl8fail == false && mid.lvl9fail == true && mid.lvl10fail == true)
        {
            f_w_f_failPanel.SetActive(true);     //fail_win_fail_fail
            Debug.Log("FAIL_WIN_FAIL_FAIL");
            Destroy(ChestlosePanel);
            Destroy(rewardPanel);
        }
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
        SoundManger.soundctrl.playClip(tapSound);
        rewardPanel.SetActive(false);
        cashScript.cashValue += 1050;
        OldcountdownPanel.SetActive(true);
        GameData.SetCoins(cashScript.cashValue);
        
    }

    public void ExtraRewardx3()
    {
        SoundManger.soundctrl.playClip(tapSound);
        rewardPanel.SetActive(false);
        cashScript.cashValue += 1575;
        OldcountdownPanel.SetActive(true);
        GameData.SetCoins(cashScript.cashValue);
    }

    public void ExtraRewardx5()
    {
        SoundManger.soundctrl.playClip(tapSound);
        rewardPanel.SetActive(false);
        cashScript.cashValue += 2625;
        OldcountdownPanel.SetActive(true);
        GameData.SetCoins(cashScript.cashValue);
    }

    public void ExtraChest()
    {
        SoundManger.soundctrl.playClip(tapSound);
        OldcountdownPanel.SetActive(true);
        Destroy(ChestlosePanel);
        
    }

    public void ExtraChestlvl3()
    {
        SoundManger.soundctrl.playClip(tapSound);

        if(mid.lvl2Fail == true)
        {
            NewcountdownPanel.SetActive(true);
            Destroy(ChestlosePanel);
        }
        else
        {
            OldcountdownPanel.SetActive(true);
            Destroy(ChestlosePanel);
        }
    }

    public void ExtraChestlvl8()
    {
        SoundManger.soundctrl.playClip(tapSound);

        if (mid.lvl8fail == true)
        {
            NewcountdownPanel.SetActive(true);
            Destroy(ChestlosePanel);
        }
        else
        {
            OldcountdownPanel.SetActive(true);
            Destroy(ChestlosePanel);
        }
    }

    public void ExtraChestlvl4()
    {
        SoundManger.soundctrl.playClip(tapSound);

        if(mid.lvl2Fail == false && mid.lvl3Fail == false)
        {
            OldcountdownPanel.SetActive(true);    //win_win
            Destroy(ChestlosePanel);
        }
        if(mid.lvl2Fail == false && mid.lvl3Fail == true)
        {
            NewcountdownPanel.SetActive(true);   //win_fail
            Destroy(ChestlosePanel);
        }
        if (mid.lvl2Fail == true && mid.lvl3Fail == false)
        {
            newCountpanel2.SetActive(true);    //fail_win
            Destroy(ChestlosePanel);
        }
        if (mid.lvl2Fail == true && mid.lvl3Fail == true)
        {
            newCountpanel3.SetActive(true);    //fail_fail
            Destroy(ChestlosePanel);
        }
    }

    public void ExtraChestlvl9()
    {
        SoundManger.soundctrl.playClip(tapSound);

        if (mid.lvl7fail == false && mid.lvl8fail == false)
        {
            OldcountdownPanel.SetActive(true);    //win_win
            Destroy(ChestlosePanel);
        }
        if (mid.lvl7fail == false && mid.lvl8fail == true)
        {
            NewcountdownPanel.SetActive(true);   //win_fail
            Destroy(ChestlosePanel);
        }
        if (mid.lvl7fail == true && mid.lvl8fail == false)
        {
            newCountpanel2.SetActive(true);    //fail_win
            Destroy(ChestlosePanel);
        }
        if (mid.lvl7fail == true && mid.lvl8fail == true)
        {
            newCountpanel3.SetActive(true);    //fail_fail
            Destroy(ChestlosePanel);
        }
    }

    public void ExtraChestlvl5()
    {
        SoundManger.soundctrl.playClip(tapSound);

        if (mid.lvl2Fail == false && mid.lvl3Fail == false && lvl4Fail.lvl4fail == false && mid.lvl5fail == false)
        {
            adPanel1.SetActive(true);    //fail_fail
            Destroy(ChestlosePanel);
        }

        if (mid.lvl2Fail == true && mid.lvl3Fail == false && lvl4Fail.lvl4fail == false && mid.lvl5fail == false)
        {
            adPanel2.SetActive(true);    //fail_fail
            Destroy(ChestlosePanel);
        }

        if (mid.lvl2Fail == false && mid.lvl3Fail == true && lvl4Fail.lvl4fail == false && mid.lvl5fail == false)
        {
            adPanel3.SetActive(true);    //fail_fail
            Destroy(ChestlosePanel);
        }

        if (mid.lvl2Fail == false && mid.lvl3Fail == false && lvl4Fail.lvl4fail == true && mid.lvl5fail == false)
        {
            adPanel4.SetActive(true);    //fail_fail
            Destroy(ChestlosePanel);
        }

        if (mid.lvl2Fail == true && mid.lvl3Fail == true && lvl4Fail.lvl4fail == true && mid.lvl5fail == false)
        {
            adPanel5.SetActive(true);    //fail_fail
            Destroy(ChestlosePanel);
        }

        if (mid.lvl2Fail == false && mid.lvl3Fail == true && lvl4Fail.lvl4fail == true && mid.lvl5fail == false)
        {
            adPanel6.SetActive(true);    //fail_fail
            Destroy(ChestlosePanel);
        }

        if (mid.lvl2Fail == true && mid.lvl3Fail == true && lvl4Fail.lvl4fail == false && mid.lvl5fail == false)
        {
            adPanel7.SetActive(true);    //fail_fail
            Destroy(ChestlosePanel);
        }

        if (mid.lvl2Fail == true && mid.lvl3Fail == false && lvl4Fail.lvl4fail == true && mid.lvl5fail == false)
        {
            adPanel8.SetActive(true);    //fail_fail
            Destroy(ChestlosePanel);
        }
    }

    public void ExtraChestlvl10()
    {
        SoundManger.soundctrl.playClip(tapSound);

        if (mid.lvl7fail == false && mid.lvl8fail == false && mid.lvl9fail == false && mid.lvl10fail == false)
        {
            adPanel1.SetActive(true);    //fail_fail
            Destroy(ChestlosePanel);
        }

        if (mid.lvl7fail == true && mid.lvl8fail == false && mid.lvl9fail == false && mid.lvl10fail == false)
        {
            adPanel2.SetActive(true);    //fail_fail
            Destroy(ChestlosePanel);
        }

        if (mid.lvl7fail == false && mid.lvl8fail == true && mid.lvl9fail == false && mid.lvl10fail == false)
        {
            adPanel3.SetActive(true);    //fail_fail
            Destroy(ChestlosePanel);
        }

        if (mid.lvl7fail == false && mid.lvl8fail == false && mid.lvl9fail == true && mid.lvl10fail == false)
        {
            adPanel4.SetActive(true);    //fail_fail
            Destroy(ChestlosePanel);
        }

        if (mid.lvl7fail == true && mid.lvl8fail == true && mid.lvl9fail == true && mid.lvl10fail == false)
        {
            adPanel5.SetActive(true);    //fail_fail
            Destroy(ChestlosePanel);
        }

        if (mid.lvl7fail == false && mid.lvl8fail == true && mid.lvl9fail == true && mid.lvl10fail == false)
        {
            adPanel6.SetActive(true);    //fail_fail
            Destroy(ChestlosePanel);
        }

        if (mid.lvl7fail == true && mid.lvl8fail == true && mid.lvl9fail == false && mid.lvl10fail == false)
        {
            adPanel7.SetActive(true);    //fail_fail
            Destroy(ChestlosePanel);
        }

        if (mid.lvl7fail == true && mid.lvl8fail == false && mid.lvl9fail == true && mid.lvl10fail == false)
        {
            adPanel8.SetActive(true);    //fail_fail
            Destroy(ChestlosePanel);
        }
    }

    public void nextLvl()
    {
        SoundManger.soundctrl.playClip(tapSound);
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        AppCentral.SetLevelStartID(SceneManager.GetActiveScene().buildIndex + 1);
        AppCentral.OnLevelComplete();
        /* loadSceneIndex = PlayerPrefs.GetInt("SavedScene");
        
        if (loadSceneIndex > 0)
            SceneManager.LoadScene(loadSceneIndex);
        else
            return;*/
        index += 1;
        GameData.SetCurrentScene(GameData.GetCurrentScene() + 1);
        //if (GameData.GetCurrentScene() >= 11)
        //{
        //    GameData.SetCurrentScene(1);
        //}
        Application.LoadLevel("Lvl " + GameData.GetCurrentScene());
    }

    public void lvlUp()
    {
      
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void KnifeSkinshoase()
    {
        
       
        knifeSkniPanel.SetActive(true);
       
    }
    public void KnifeSkin()
    {
       
        SoundManger.soundctrl.playClip(tapSound);
        alert.changeColor();
        knifeSkniPanel.SetActive(true);
        Dirlight.transform.rotation = Quaternion.Euler(50, -30, 0);
        ShowCase.SetActive(false);
        button.SetActive(false);
        RenderSettings.fog = false;
        Plysounds.volume = 0;
        Aisounds.volume = 0;
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
        SculptPanel.SetActive(true);
        knifeSkniPanel.SetActive(false);
        Room.SetActive(false);
        RenderSettings.fog = true;
        uiBar.SetActive(true);
        cam.gameObject.SetActive(true);
        ShowCase.SetActive(false);
        Dirlight.transform.rotation = Quaternion.Euler(50, -30, 0);
        newButton.SetActive(true);
        Plysounds.volume = 1;
        Aisounds.volume = .5f;
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
        Plysounds.volume = 0;
        RenderSettings.fog = false;
        Aisounds.volume = 0;
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
        RenderSettings.fog = false;
        newButton.SetActive(false);
        Plysounds.volume = 0;
        Aisounds.volume = 0;
        // SceneManager.LoadScene(8);
    }
    public void knieskin(bool isclicked)
    {
        mid.isskin = isclicked;
        newFail.isSkinEnabled = true;
        if (mid.isskin)
        {
            StartCoroutine(panelDis());
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

    public void SettingsButtonOpen()
    {
        settingPanel.SetActive(true);
    }

    public void SettingsButtonClose()
    {
        settingPanel.SetActive(false);
    }

    public void hapticOn()
    {
        ishaptic = false;
        hapticON.SetActive(false);
        hapticOFF.SetActive(true);
    }

    public void hapticOff()
    {
        ishaptic = true;
        hapticOFF.SetActive(false);
        hapticON.SetActive(true);
    }

    public void soundcOn()
    {
        Plysounds.volume = 0;
        Aisounds.volume = 0;
        soundON.SetActive(false);
        soundOFF.SetActive(true);
    }

    public void soundOff()
    {
        Plysounds.volume = 1;
        Aisounds.volume = 0.5f;
        soundOFF.SetActive(false);
        soundON.SetActive(true);
    }

    public void bonusDouble2()
    {
        SoundManger.soundctrl.playClip(tapSound);
        rewardPanel.SetActive(false);
        cashScript.cashValue += BounsCoinCollect.coinValue * 2;
        OldcountdownPanel.SetActive(true);
        GameData.SetCoins(cashScript.cashValue);

    }

    public void bonusDouble3()
    {
        SoundManger.soundctrl.playClip(tapSound);
        rewardPanel.SetActive(false);
        cashScript.cashValue += BounsCoinCollect.coinValue * 3;
        OldcountdownPanel.SetActive(true);
        GameData.SetCoins(cashScript.cashValue);
    }

    public void bonusDouble5()
    {
        SoundManger.soundctrl.playClip(tapSound);
        rewardPanel.SetActive(false);
        cashScript.cashValue += BounsCoinCollect.coinValue * 5;
        OldcountdownPanel.SetActive(true);
        GameData.SetCoins(cashScript.cashValue);
    }

    public void bonusNext()
    {
        SoundManger.soundctrl.playClip(tapSound);
        cashScript.cashValue += 500;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameData.SetCoins(cashScript.cashValue);
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
        ballRay.enabled = true;
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
        ispresed = true;
        yield return new WaitForSeconds(5f);
        ballRay.enabled = true;
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

    //    knifePlayer2.enabled = true;
        ai.enabled = true;
       
    }

    IEnumerator playButton3()
    {
        yield return new WaitForSeconds(1.5f);
        bonusKnife.enabled = true;
        up.enabled = true;  
        //playButon.SetActive(true);
    }

    IEnumerator panelDis()
    {
        SculptPanel.SetActive(false);
        yield return new WaitForSeconds(5);
        SculptPanel.SetActive(true);
    }

}
