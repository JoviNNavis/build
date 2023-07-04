using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class KnifeScriptSword : MonoBehaviour
{
    private bool isTouch;
    public bool level1;
    private bool isRelease = false;
    public bool ischangecolor;
    public float fireRate;

    [SerializeField] private float fireTime;
    [SerializeField] private float nextfireRate;
    public int totalblocks;
    public GameObject sword, newSword, powerupSword;
    public Slider playerCrownSlider;
    int rand;
    int randcolors;
    public Transform newBallPos;
    private GameObject _knife;
    public Image playerImg;
    public bool playerchangecolor;
    public float fillValue;
    public GameObject combo;
    public TMPro.TextMeshProUGUI _text;
    public GameObject counterText;

    public AudioClip knifeThrow;

    public bool NewEffect;
    public float animtime;
    public NewFailScript failNew;

    void Start()
    {
        
    }


    void Update()
    {
        Animator anim = FindObjectOfType<ButtonManager>().text;
        ischangecolor = FindObjectOfType<ButtonManager>().changecolor;
       
        transform.Rotate(3f, 0, 0);
        playerchangecolor = FindObjectOfType<ButtonManager>().changecolor;
        if (Input.GetMouseButtonDown(1))
        {
            counterText.SetActive(true);
        }
        else if(Input.GetMouseButtonUp(1))
        {
            StartCoroutine(txtDisable());
        }
        if (Input.GetMouseButton(1))
        {
            Shooting();
         


        }

        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            animtime += Time.deltaTime;
            animtime = 0;
            if (touch.phase == TouchPhase.Began)
            {



                isTouch = true;
               

                
            }
            if (touch.phase == TouchPhase.Ended)
            {
                StartCoroutine(txtDisable());
                isTouch = false;
            }

            if (isTouch)
            {
                Shooting();
            }
        }
        
    }

    void Shooting()
    {
            failNew.timer += Time.deltaTime;

        fireTime += Time.deltaTime;
        nextfireRate = 1 / fireRate;
        counterText.SetActive(true);
        if (fireTime >= nextfireRate)
        {
            failNew.timer += Time.deltaTime;
            SoundManger.soundctrl.playClip(knifeThrow);
            if (ischangecolor)
                {
                NewEffect = false;
                _knife = Instantiate(powerupSword, new Vector3(2.4f, transform.localPosition.y, transform.localPosition.z), Quaternion.identity);
                    FindObjectOfType<NewFailScript>().Knifes.Add(_knife.gameObject.transform);
                knifeCounter.knifeCountValue += 1;
                randcolors++;
                    transform.position += new Vector3(0, 0.7f, 0);
                    newBallPos.transform.position += new Vector3(0, 0.7f, 0);
                    transform.rotation = Quaternion.Euler(90, -180, 0);
                    playerImg.fillAmount += fillValue;
                    playerCrownSlider.value += fillValue;
                    //PlayerRank.transform.position += new Vector3(rankValue, 0, 0);
                    fireTime = 0;
                  
                }

            if(NewEffect && !ischangecolor)
            {
                GameObject _knife = Instantiate(newSword, new Vector3(2.4f, transform.localPosition.y, transform.localPosition.z), Quaternion.identity);
              
                combo.SetActive(false);
                FindObjectOfType<NewFailScript>().Knifes.Add(_knife.gameObject.transform);
                knifeCounter.knifeCountValue += 1;
                transform.position += new Vector3(0, 0.7f, 0);
                newBallPos.transform.position += new Vector3(0, 0.7f, 0);
                transform.rotation = Quaternion.Euler(90, -180, 0);
                playerImg.fillAmount += fillValue;
                playerCrownSlider.value += fillValue;
                //PlayerRank.transform.position += new Vector3(rankValue, 0, 0);
                fireTime = 0;
              
            }
                if(!NewEffect && !ischangecolor)
                {
                
                GameObject _knife = Instantiate(sword, new Vector3(2.4f, transform.localPosition.y, transform.localPosition.z), Quaternion.identity);
                combo.SetActive(false);
                FindObjectOfType<NewFailScript>().Knifes.Add(_knife.gameObject.transform);
                knifeCounter.knifeCountValue += 1;
                transform.position += new Vector3(0, 0.7f, 0);
                    newBallPos.transform.position += new Vector3(0, 0.7f, 0);
                    transform.rotation = Quaternion.Euler(90, -180, 0);
                    playerImg.fillAmount += fillValue;
                    playerCrownSlider.value += fillValue;
                //PlayerRank.transform.position += new Vector3(rankValue, 0, 0);
                fireTime = 0;
                }
            
          
        }
    }

    IEnumerator txtDisable()
    {

        
        yield return new WaitForSeconds(1.75f);
        failNew.timer = 0;
        counterText.SetActive(false);
    
      
    }

}
