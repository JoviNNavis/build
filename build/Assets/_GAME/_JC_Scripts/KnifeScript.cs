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
  public  bool ischangecolor;
    public float fireRate;
    public GameObject[] knifemat;
    [SerializeField] private float fireTime;
    [SerializeField] private float nextfireRate;
    public int totalblocks;
    public GameObject knife;
    public Slider playerCrownSlider;
    int rand;
    int randcolors;
    public Transform newBallPos;
    private GameObject _knife;
    public Image playerImg;
    public bool playerchangecolor;
    public float fillValue;

    public GameObject counterText;

    public AudioClip knifeThrow;

    void Start()
    {
        
    }


    void Update()
    {
       
        ischangecolor = FindObjectOfType<ButtonManager>().changecolor;
        rand = Random.Range(0, knifemat.Length);
        transform.Rotate(3f, 0, 0);
        playerchangecolor = FindObjectOfType<ButtonManager>().changecolor;
        if (Input.GetMouseButton(1))
        {
            Shooting();
            
        }
        else
        {
            StartCoroutine(txtDisable());
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

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
       
        fireTime += Time.deltaTime;
        nextfireRate = 1 / fireRate;
        counterText.SetActive(true);
        if (fireTime >= nextfireRate)
        {
            SoundManger.soundctrl.playClip(knifeThrow);
            if (ischangecolor)
                {
                
                _knife = Instantiate(knifemat[randcolors], new Vector3(2.4f, transform.localPosition.y, transform.localPosition.z), Quaternion.identity);
                    FindObjectOfType<FailScript1>().Knifes.Add(_knife.gameObject.transform);
                    randcolors++;
                    transform.position += new Vector3(0, 0.7f, 0);
                    newBallPos.transform.position += new Vector3(0, 0.7f, 0);
                    transform.rotation = Quaternion.Euler(90, -180, 0);
                    playerImg.fillAmount += fillValue;
                    playerCrownSlider.value += fillValue;
                fireRate = 15;
                    //PlayerRank.transform.position += new Vector3(rankValue, 0, 0);
                    fireTime = 0;
                    if (randcolors >= knifemat.Length)
                    {
                        randcolors = 0;
                    }
                }
                else
                {
                
                GameObject _knife = Instantiate(knife, new Vector3(2.4f, transform.localPosition.y, transform.localPosition.z), Quaternion.identity);

                    FindObjectOfType<FailScript1>().Knifes.Add(_knife.gameObject.transform);
                fireRate = 14;

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
        yield return new WaitForSeconds(0.55f);
        counterText.SetActive(false);
    }

}
