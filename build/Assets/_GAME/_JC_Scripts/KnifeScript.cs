using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class KnifeScript : MonoBehaviour
{
    private bool isTouch;
    public bool level1;
    private bool isRelease = false;
    public bool ischangecolor;
    public float fireRate;
    public GameObject[] knifemat;
    [SerializeField] private float fireTime;
    [SerializeField] private float nextfireRate;
    public int totalblocks;
    public GameObject knife, newKnife;
    public Slider playerCrownSlider;
    int rand;
    int randcolors;
    public Transform newBallPos;
    private GameObject _knife;
    public Image playerImg;
    public bool playerchangecolor;
    public float fillValue;
    public GameObject combo;
    public GameObject counterText;
    public  int combovalue;
    public AudioClip knifeThrow;
    public TMPro.TextMeshProUGUI _text;

    public FailScript1 fail1;
    public float animtime;
    public bool NewEffect;

    void Start()
    {
        combovalue = 1;
    }


    void Update()
    {
       
        Animator anim = FindObjectOfType<ButtonManager>().text;
        ischangecolor = FindObjectOfType<ButtonManager>().changecolor;
        rand = Random.Range(0, knifemat.Length);
        transform.Rotate(3f, 0, 0);
        playerchangecolor = FindObjectOfType<ButtonManager>().changecolor;
       
        if (Input.GetMouseButtonDown(1))
        {
            counterText.SetActive(true);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            StartCoroutine(txtDisable());
        }
        if (Input.GetMouseButton(1))
        {
            Shooting();
           
            //bool ispressed = true;

            //if (ispressed){
            //    animtime += Time.deltaTime;
            //}
            //if (animtime >= 3)
            //{
            //    anim.enabled = false;
            //    animtime = 0;
            //}
            //else if(animtime < 3)
            //{
            //    anim.enabled = false;
            //}

           
 
        }
       

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            animtime += Time.deltaTime;
            animtime = 0;
            if (touch.phase == TouchPhase.Began)
            {
              
                isTouch = true;
                //if (animtime >= 3)
                //{
                //    anim.enabled = false;
                //    animtime = 0;
                //}
                //else if (animtime < 3)
                //{
                //    anim.enabled = false;
                //}

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
       
        fireTime += Time.deltaTime;
        nextfireRate = 1 / fireRate;
        counterText.SetActive(true);
        if (fireTime >= nextfireRate)
        {
            fail1.timer += Time.deltaTime;
            SoundManger.soundctrl.playClip(knifeThrow);
            if (ischangecolor)
                {

                NewEffect = false;
                _knife = Instantiate(knifemat[randcolors], new Vector3(2.4f, transform.localPosition.y, transform.localPosition.z), Quaternion.identity);
                    FindObjectOfType<FailScript1>().Knifes.Add(_knife.gameObject.transform);
                //      knifeCounter.knifeCountValue = FindObjectOfType<FailScript1>().Knifes.Count;
                      knifeCounter.knifeCountValue += 1;

                randcolors++;
                    transform.position += new Vector3(0, 0.7f, 0);
                    newBallPos.transform.position += new Vector3(0, 0.7f, 0);
                    transform.rotation = Quaternion.Euler(90, -180, 0);
                    playerImg.fillAmount += fillValue;
                    playerCrownSlider.value += fillValue;
                fireRate = 15;
    
                    fireTime = 0;
                    if (randcolors >= knifemat.Length)
                    {
                        randcolors = 0;
                    }
                }

            if(NewEffect && !ischangecolor)
            {
                combo.SetActive(false);

                GameObject _knife = Instantiate(newKnife, new Vector3(2.4f, transform.localPosition.y, transform.localPosition.z), Quaternion.identity);

                FindObjectOfType<FailScript1>().Knifes.Add(_knife.gameObject.transform);

                fireRate = 14;

                // knifeCounter.knifeCountValue = FindObjectOfType<FailScript1>().Knifes.Count;
                knifeCounter.knifeCountValue += 1;

                combovalue = 1;
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
                combo.SetActive(false);
                
                GameObject _knife = Instantiate(knife, new Vector3(2.4f, transform.localPosition.y, transform.localPosition.z), Quaternion.identity);
                
                FindObjectOfType<FailScript1>().Knifes.Add(_knife.gameObject.transform);
              
                fireRate = 14;

                // knifeCounter.knifeCountValue = FindObjectOfType<FailScript1>().Knifes.Count;
                knifeCounter.knifeCountValue += 1;


                combovalue = 1;
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
        fail1.timer = 0;
       counterText.SetActive(false);
    }

}
