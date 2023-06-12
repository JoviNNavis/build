using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;
using MoreMountains.NiceVibrations;

//using Lofelt.NiceVibrations;
public class NewFailScript : MonoBehaviour
{
    public List<Transform> Knifes = new List<Transform>();

    public KnifeScript knife1;
 //   public GameObject puntured_ball;
    public GameObject KnifePlayer;

    public KnifeScriptSword swordKnife;
    public Material skybox;
    public GameObject combo;
    public GameObject newBall;
    public Transform newBallPos;
    public GameObject puntured_ball;

    public Slider playerCrownSlider;
    public Image playerImg;

    public float lessValue;
    public int cou;
    public AudioClip ballPunture;

    public Transform emptyObj;

    public bool isSkinEnabled;

    public float winValue;

    public BoxCollider winCudeCollider;

    void Start()
    {
        //Knifes.Add(gameObject.transform);
        skybox = RenderSettings.skybox;

    }

    // Update is called once per frame
    void Update()
    {
        cou = knifeCounter.knifeCountValue;
        if(isSkinEnabled)
        {
            if (Knifes.Count >= winValue)
            {
                winCudeCollider.enabled = true;
            }
            else
            {
                winCudeCollider.enabled = false;
            }
        }

        //if(Knifes.Count >= winValue)
        //{
        //   winCudeCollider.enabled = true;
        //}
        //else
        //{
        //    winCudeCollider.enabled = false;
        //}

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


        if (Input.GetKeyDown(KeyCode.G) && Knifes.Count >= 3)
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
            if (FindObjectOfType<ButtonManager>().ishaptic)
            {
                MMVibrationManager.Haptic(HapticTypes.SoftImpact);
                Debug.LogWarning("greenknife");
            }
            //          Knifes.Add(collision.transform);
        }

        if (collision.gameObject.tag == "Ball" && Knifes.Count == 1)
        {
            GameObject pball =   Instantiate(puntured_ball,collision.transform.position + new Vector3(0.3f, 0, 0), Quaternion.Euler(0,-0, 0));
            
            SoundManger.soundctrl.playClip(ballPunture);
            //knife1.enabled = false;
            RenderSettings.skybox = skybox;
            RenderSettings.fogColor = FindObjectOfType<ColorScript>().fog;
            combo.SetActive(false);

            swordKnife.enabled = false;
            Destroy(collision.gameObject, 0.1f);
            Destroy(pball, 0.12f);
            StartCoroutine(knifeR1());

        }

        if (collision.gameObject.tag == "Ball" && Knifes.Count == 2)
        {
            GameObject pball  = Instantiate(puntured_ball, collision.transform.position, Quaternion.Euler(0, -0, 0));
            SoundManger.soundctrl.playClip(ballPunture);
            //knife1.enabled = false;
            RenderSettings.skybox = skybox;
            RenderSettings.fogColor = FindObjectOfType<ColorScript>().fog;
            combo.SetActive(false);

            swordKnife.enabled = false;
            Destroy(collision.gameObject, 0.1f);
            Destroy(pball, 0.12f);
            StartCoroutine(knifeR2());
        }

        if (collision.gameObject.tag == "Ball" && Knifes.Count >= 3)
        {
            GameObject pball = Instantiate(puntured_ball, collision.transform.position, Quaternion.Euler(0, -0, 0));
            SoundManger.soundctrl.playClip(ballPunture);
            //knife1.enabled = false;
            RenderSettings.skybox = skybox;
            RenderSettings.fogColor = FindObjectOfType<ColorScript>().fog;
            combo.SetActive(false);

            swordKnife.enabled = false;
            Destroy(collision.gameObject, 0.1f);
            Destroy(pball, 0.12f);
            StartCoroutine(knifeR3());

        }
    }
    IEnumerator knifeR1()
    {
        yield return new WaitForSeconds(0.2f);
        Knifes.ElementAt(Knifes.Count - 1).DOMoveX(1, 0.08f, false).OnComplete(knifeRemove);
        yield return new WaitForSeconds(0.5f);
        //knife1.enabled = true;
        swordKnife.enabled = true;
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
        yield return new WaitForSeconds(0.5f);
        //knife1.enabled = true;
        swordKnife.enabled = true;
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
        yield return new WaitForSeconds(0.5f);
       // knife1.enabled = true;
        swordKnife.enabled = true;
        GameObject oldBall = Instantiate(newBall, newBallPos.position, Quaternion.identity);
        oldBall.transform.SetParent(emptyObj, true);
    }

    IEnumerator knifeR4()
    {
        yield return new WaitForSeconds(0.2f);
        Knifes.ElementAt(Knifes.Count - 1).DOMoveX(1, 0.03f, false).OnComplete(knifeRemove);
        playerImg.fillAmount -= lessValue;
        playerCrownSlider.value -= lessValue;
        KnifePlayer.transform.position -= new Vector3(0, 0.7f, 0);
        yield return new WaitForSeconds(0.1f);
        Knifes.ElementAt(Knifes.Count - 1).DOMoveX(1, 0.03f, false).OnComplete(knifeRemove);
        playerImg.fillAmount -= lessValue;
        playerCrownSlider.value -= lessValue;
        KnifePlayer.transform.position -= new Vector3(0, 0.7f, 0);
        yield return new WaitForSeconds(0.05f);
        Knifes.ElementAt(Knifes.Count - 1).DOMoveX(1, 0.01f, false);
        Knifes.ElementAt(Knifes.Count - 1).gameObject.SetActive(false);
        Knifes.RemoveAt(Knifes.Count - 1);
        playerImg.fillAmount -= lessValue;
        playerCrownSlider.value -= lessValue;
        KnifePlayer.transform.position -= new Vector3(0, 0.7f, 0);
        yield return new WaitForSeconds(0.5f);
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
