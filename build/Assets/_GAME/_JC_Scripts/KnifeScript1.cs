using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class KnifeScript1 : MonoBehaviour
{
    private bool isTouch;

    private bool isRelease = false;
    public bool ischangecolor;
    public float fireRate;
    public GameObject[] knifemat;
    [SerializeField] private float fireTime;
    [SerializeField] private float nextfireRate;
    int randcolors;
    public Transform newBallPos;
    public GameObject knife;
    public GameObject newknife;
    public GameObject PlayerRank;
    public GameObject combo;
    public Image playerImg;
    public float rankValue;

    public GameObject countText;
    public TMPro.TextMeshProUGUI _text;
    public AudioClip hit;

    public FailScript failScript;
    public float animtime;
    public bool newEffect;

    void Start()
    {
        fireRate = 10.5f;
    }

 
    void Update()
    {
        ischangecolor = FindObjectOfType<ButtonManager>().changecolor;
        Animator anim = FindObjectOfType<ButtonManager>().text;
        transform.Rotate(3f, 0, 0);

       
        if (Input.GetMouseButtonDown(0))
        {
            countText.SetActive(true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(txtDisable());
        }
        if (Input.GetMouseButton(0))
        {

            //animtime += Time.deltaTime;
            //animtime = 0;
            //if (animtime >= 3)
            //{
            //    anim.enabled = false;
            //    animtime = 0;
            //}
            //else if (animtime < 3)
            //{
            //    anim.enabled = false;
            //}

            failScript.timer += Time.deltaTime;
          
            countText.SetActive(true);
            Shooting();
        }


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
        failScript.timer += Time.deltaTime;
        fireTime += Time.deltaTime;
        nextfireRate = 1 / fireRate;

        if (fireTime >= nextfireRate)
        {
            failScript.timer += Time.deltaTime;
            SoundManger.soundctrl.playClip(hit);
            if (ischangecolor)
            {
                newEffect = false;
                GameObject _knife =  Instantiate(knifemat[randcolors], new Vector3(2.4f, transform.localPosition.y, transform.localPosition.z), Quaternion.identity);
                FindObjectOfType<FailScript>().Knifes.Add(_knife.gameObject.transform);
                knifeCounter.knifeCountValue += 1;

                randcolors++;
                newBallPos.transform.position += new Vector3(0, 0.7f, 0);
                transform.position += new Vector3(0, 0.7f, 0);
                transform.rotation = Quaternion.Euler(90, -180, 0);
                playerImg.fillAmount += rankValue;
                fireTime = 0;
                if (randcolors >= knifemat.Length)
                {
                    randcolors = 0;
                }
            }

            if (newEffect && !ischangecolor)
            {
                GameObject _knife = Instantiate(newknife, new Vector3(2.4f, transform.localPosition.y, transform.localPosition.z), Quaternion.identity);
                FindObjectOfType<FailScript>().Knifes.Add(_knife.gameObject.transform);
                knifeCounter.knifeCountValue += 1;

                newBallPos.transform.position += new Vector3(0, 0.7f, 0);
                transform.position += new Vector3(0, 0.7f, 0);
                transform.rotation = Quaternion.Euler(90, -180, 0);
                playerImg.fillAmount += rankValue;
                fireTime = 0;
            }

            if(!ischangecolor && !newEffect)
            {
                GameObject _knife =  Instantiate(knife, new Vector3(2.4f, transform.localPosition.y, transform.localPosition.z), Quaternion.identity);
                FindObjectOfType<FailScript>().Knifes.Add(_knife.gameObject.transform);
                knifeCounter.knifeCountValue += 1;

                newBallPos.transform.position += new Vector3(0, 0.7f, 0);
                transform.position += new Vector3(0, 0.7f, 0);
                transform.rotation = Quaternion.Euler(90, -180, 0);
                playerImg.fillAmount += rankValue;
                fireTime = 0;
            }
        }
    }

    IEnumerator txtDisable()
    {
        yield return new WaitForSeconds(2.5f);
        failScript.timer = 0;
        countText.SetActive(false);
    }
}
