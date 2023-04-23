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
    public GameObject PlayerRank;

    public Image playerImg;
    public float rankValue;

    public GameObject countText;

    void Start()
    {
        fireRate = 10.5f;
    }

 
    void Update()
    {
        ischangecolor = FindObjectOfType<ButtonManager>().changecolor;
        transform.Rotate(3f, 0, 0);
        if (Input.GetMouseButton(0))
        {
            countText.SetActive(true);
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

        if (fireTime >= nextfireRate)
        {
            if (ischangecolor)
            {
                GameObject _knife =  Instantiate(knifemat[randcolors], new Vector3(2.4f, transform.localPosition.y, transform.localPosition.z), Quaternion.identity);
                FindObjectOfType<FailScript>().Knifes.Add(_knife.gameObject.transform);

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
            else
            {
                GameObject _knife =  Instantiate(knife, new Vector3(2.4f, transform.localPosition.y, transform.localPosition.z), Quaternion.identity);
                FindObjectOfType<FailScript>().Knifes.Add(_knife.gameObject.transform);
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
        yield return new WaitForSeconds(0.55f);
        countText.SetActive(false);
    }
}
