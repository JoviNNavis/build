using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;

public class FailScript1 : MonoBehaviour
{
    public List<Transform> Knifes = new List<Transform>();

    public KnifeScript knife1;
 //   public GameObject puntured_ball;
    public GameObject KnifePlayer;

    public GameObject newBall;
    public Transform newBallPos;
    public GameObject puntured_ball;

    public Slider playerCrownSlider;
    public Image playerImg;

    public float lessValue;

    public AudioClip ballPunture;

    public Transform emptyObj;

    void Start()
    {
        //Knifes.Add(gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && Knifes.Count == 1)
        {
            //knifeRemove();
            StartCoroutine(knifeR1());
        }

        if (Input.GetKeyDown(KeyCode.G) && Knifes.Count == 2)
        {
            //knifeRemove();
            StartCoroutine(knifeR2());
        }


        if (Input.GetKeyDown(KeyCode.G) && Knifes.Count <= 3)
        {
            //knifeRemove();
           StartCoroutine(knifeR3());
        }

        //for(int i = 0; i < Knifes.Count; i++)
        //{
        //    var firstKnife = Knifes.
        //}
    }
  
    //private void OnTriggerEnter(Collider collision)
    //{
    //    if (collision.gameObject.tag == "Knife")
    //    {
    //        knifeCounter.knifeCountValue += 1;
    //        //          Knifes.Add(collision.transform);
    //    }

    //    if (collision.gameObject.tag == "Ball" && Knifes.Count == 1)
    //    {
    //        //   Instantiate(puntured_ball,collision.transform.position,Quaternion.Euler(0,-180,0));
    //        //
    //        Destroy(collision.gameObject, 0.1f);
    //        knife1.enabled = false;
    //        StartCoroutine(knifeR1());
    //        Debug.Log("touched");

    //    }

    //    if (collision.gameObject.tag == "Ball" && Knifes.Count == 2)
    //    {
    //        //  Instantiate(puntured_ball, collision.transform.position, Quaternion.Euler(0, -180, 0));

    //        Destroy(collision.gameObject, 0.1f);
    //        knife1.enabled = false;
    //        Debug.Log("touched");
    //        StartCoroutine(knifeR2());
    //    }

    //    if (collision.gameObject.tag == "Ball" && Knifes.Count >= 3)
    //    {
    //        //  Instantiate(puntured_ball, collision.transform.position, Quaternion.Euler(0, -180, 0));

    //        Destroy(collision.gameObject, 0.1f);
    //        knife1.enabled = false;
    //        StartCoroutine(knifeR3());
    //        Debug.Log("touched");

    //    }
    //}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Knife")
        {
            knifeCounter.knifeCountValue += 1;
            //          Knifes.Add(collision.transform);
        }

        if (collision.gameObject.tag == "Ball" && Knifes.Count == 1)
        {
            GameObject pball =   Instantiate(puntured_ball,collision.transform.position + new Vector3(0.3f, 0, 0), Quaternion.Euler(0,-0, 0));
            
            SoundManger.soundctrl.playClip(ballPunture);
            Destroy(collision.gameObject, 0.1f);
            Destroy(pball, 0.12f);
            knife1.enabled = false; 
            StartCoroutine(knifeR1());
            Debug.Log("touched");

        }

        if (collision.gameObject.tag == "Ball" && Knifes.Count == 2)
        {
            GameObject pball  = Instantiate(puntured_ball, collision.transform.position, Quaternion.Euler(0, -0, 0));
            SoundManger.soundctrl.playClip(ballPunture);
            Destroy(collision.gameObject, 0.1f);
            Destroy(pball, 0.12f);

            knife1.enabled = false;
            Debug.Log("touched");
            StartCoroutine(knifeR2());
        }

        if (collision.gameObject.tag == "Ball" && Knifes.Count >= 3)
        {
            GameObject pball = Instantiate(puntured_ball, collision.transform.position, Quaternion.Euler(0, -0, 0));
            SoundManger.soundctrl.playClip(ballPunture);
            Destroy(collision.gameObject, 0.1f);
            Destroy(pball, 0.12f);

            knife1.enabled = false;
            StartCoroutine(knifeR3());
            Debug.Log("touched");

        }
    }
    IEnumerator knifeR1()
    {
        yield return new WaitForSeconds(0.2f);
        Knifes.ElementAt(Knifes.Count - 1).DOMoveX(1, 0.08f, false).OnComplete(knifeRemove);
        yield return new WaitForSeconds(0.1f);
        knife1.enabled = true;
        GameObject oldBall =  Instantiate(newBall, newBallPos.position, Quaternion.identity);
        oldBall.transform.SetParent(emptyObj, true);
    }

    IEnumerator knifeR2()
    {
        yield return new WaitForSeconds(0.2f);
        Knifes.ElementAt(Knifes.Count - 1).DOMoveX(1, 0.08f, false).OnComplete(knifeRemove);
        playerImg.fillAmount -= lessValue;
        playerCrownSlider.value -= lessValue;
        KnifePlayer.transform.position -= new Vector3(0, 0.7f, 0);
        newBallPos.transform.position -= new Vector3(0, 0.7f, 0);
        yield return new WaitForSeconds(0.1f);
        Knifes.ElementAt(Knifes.Count - 1).DOMoveX(1, 0.08f, false).OnComplete(knifeRemove);
        playerImg.fillAmount -= lessValue;
        playerCrownSlider.value -= lessValue;
        KnifePlayer.transform.position -= new Vector3(0, 0.7f, 0);
        newBallPos.transform.position -= new Vector3(0, 0.7f, 0);
        yield return new WaitForSeconds(0.1f);
        knife1.enabled = true;
        GameObject oldBall = Instantiate(newBall, newBallPos.position, Quaternion.identity);
        oldBall.transform.SetParent(emptyObj, true);
    }

    IEnumerator knifeR3()
    {
        yield return new WaitForSeconds(0.2f);
        Knifes.ElementAt(Knifes.Count - 1).DOMoveX(1, 0.08f, false).OnComplete(knifeRemove);
        playerImg.fillAmount -= lessValue;
        playerCrownSlider.value -= lessValue;
        KnifePlayer.transform.position -= new Vector3(0, 0.7f, 0);
        newBallPos.transform.position -= new Vector3(0, 0.7f, 0);
        yield return new WaitForSeconds(0.1f);
        Knifes.ElementAt(Knifes.Count - 1).DOMoveX(1, 0.08f, false).OnComplete(knifeRemove);
        playerImg.fillAmount -= lessValue;
        playerCrownSlider.value -= lessValue;
        KnifePlayer.transform.position -= new Vector3(0, 0.7f, 0);
        newBallPos.transform.position -= new Vector3(0, 0.7f, 0);
        yield return new WaitForSeconds(0.1f);
        Knifes.ElementAt(Knifes.Count - 1).DOMoveX(1, 0.08f, false).OnComplete(knifeRemove);
        playerImg.fillAmount -= lessValue;
        playerCrownSlider.value -= lessValue;
        KnifePlayer.transform.position -= new Vector3(0, 0.7f, 0);
        newBallPos.transform.position -= new Vector3(0, 0.7f, 0);
        yield return new WaitForSeconds(0.2f);
        knife1.enabled = true;
        GameObject oldBall = Instantiate(newBall, newBallPos.position, Quaternion.identity);
        oldBall.transform.SetParent(emptyObj, true);
    }

    IEnumerator knifeR4()
    {
        yield return new WaitForSeconds(0.2f);
        Debug.Log("1");
        Knifes.ElementAt(Knifes.Count - 1).DOMoveX(1, 0.03f, false).OnComplete(knifeRemove);
        playerImg.fillAmount -= lessValue;
        playerCrownSlider.value -= lessValue;
        KnifePlayer.transform.position -= new Vector3(0, 0.7f, 0);
        yield return new WaitForSeconds(0.1f);
        Debug.Log("2");
        Knifes.ElementAt(Knifes.Count - 1).DOMoveX(1, 0.03f, false).OnComplete(knifeRemove);
        playerImg.fillAmount -= lessValue;
        playerCrownSlider.value -= lessValue;
        KnifePlayer.transform.position -= new Vector3(0, 0.7f, 0);
        yield return new WaitForSeconds(0.05f);
        Knifes.ElementAt(Knifes.Count - 1).DOMoveX(1, 0.01f, false);
        Knifes.ElementAt(Knifes.Count - 1).gameObject.SetActive(false);
        Knifes.RemoveAt(Knifes.Count - 1);
        Debug.Log("3");
        playerImg.fillAmount -= lessValue;
        playerCrownSlider.value -= lessValue;
        KnifePlayer.transform.position -= new Vector3(0, 0.7f, 0);
        yield return new WaitForSeconds(0.3f);
        knife1.enabled = true;
    }

    public void knifeRemove()
    {
        Knifes.ElementAt(Knifes.Count - 1).gameObject.SetActive(false);
        Knifes.RemoveAt(Knifes.Count - 1);
    }

    public void failed()
    {
        StartCoroutine(knifeR4());
    }
}
