using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using MoreMountains.NiceVibrations;

using DG.Tweening;

public class FailScript : MonoBehaviour
{
    public List<Transform> Knifes = new List<Transform>();

    public KnifeScript1 knife1;
    public Text counter;
    public GameObject KnifePlayer;
    public Material skybox;
    public GameObject newBall;
    public Transform newBallPos;
    public GameObject puntured_ball;
    public GameObject combo;
    public Image playerImg;

    public AudioClip ballPunture;

    public float winValue;

    public BoxCollider winCubeCollider;

    public float timer;

    public KnifeScript1 knifeScript;

    void Start()
    {
        //Knifes.Add(gameObject.transform);
        skybox = RenderSettings.skybox;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(timer);

        if(timer >= 0.15f && knifeScript.ischangecolor == false)
        {
            knifeScript.newEffect = true;
        }
        else
        {
            knifeScript.newEffect = false;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
   
        }

        if(Knifes.Count >= winValue)
        {
            winCubeCollider.enabled = true;
        }
        else
        {
            winCubeCollider.enabled = false;
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

    //    }
    //    if (collision.gameObject.tag == "Ball" && Knifes.Count == 1)
    //    {
    //        //  Instantiate(puntured_ball, collision.transform.position, Quaternion.Euler(0, -180, 0));

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
    //        StartCoroutine(knifeR2());
    //        Debug.Log("touched");

    //    }

    //    if (collision.gameObject.tag == "Ball" && Knifes.Count >= 3)
    //    {
    //        //   Instantiate(puntured_ball, collision.transform.position, Quaternion.Euler(0, -180, 0));

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
            if (FindObjectOfType<ButtonManager>().ishaptic)
            {
                MMVibrationManager.Haptic(HapticTypes.SoftImpact);
            }

        }
        if (collision.gameObject.tag == "Ball" && Knifes.Count == 1)
        {
            SoundManger.soundctrl.playClip(ballPunture);
            GameObject pball =   Instantiate(puntured_ball, collision.transform.position+ new Vector3(0.3f, 0, 0), Quaternion.Euler(0, -270, 0));
            combo.SetActive(false);

            Destroy(collision.gameObject, 0.2f);
            Destroy(pball, 0.22f);
            

            RenderSettings.skybox = skybox;
            RenderSettings.fogColor = FindObjectOfType<ColorScript>().fog;
            knife1.enabled = false;
            StartCoroutine(knifeR1());
        }
        if (collision.gameObject.tag == "Ball" && Knifes.Count == 2)
        {
            SoundManger.soundctrl.playClip(ballPunture);
            GameObject pball = Instantiate(puntured_ball, collision.transform.position+new Vector3(0.3f,0,0), Quaternion.Euler(0, -270, 0));
            combo.SetActive(false);

            Destroy(collision.gameObject, 0.2f);
           Destroy(pball, 0.22f);
         
            RenderSettings.skybox = skybox;
            RenderSettings.fogColor = FindObjectOfType<ColorScript>().fog;
            knife1.enabled = false;
            StartCoroutine(knifeR2());

        }

        if (collision.gameObject.tag == "Ball" && Knifes.Count >= 3)
        {
            SoundManger.soundctrl.playClip(ballPunture);
            GameObject pball = Instantiate(puntured_ball, collision.transform.position+new Vector3(0.3f,0,0), Quaternion.Euler(0, -0270, 0));

            Destroy(collision.gameObject, 0.2f);
            Destroy(pball, 0.22f);
            combo.SetActive(false);


            RenderSettings.skybox = skybox;
            RenderSettings.fogColor = FindObjectOfType<ColorScript>().fog;
            knife1.enabled = false;
            StartCoroutine(knifeR3());

        }
    }

    IEnumerator knifeR1()
    {
        yield return new WaitForSeconds(0.2f);
        Knifes.ElementAt(Knifes.Count - 1).DOMoveX(1, 0.08f, false).OnComplete(knifeRemove);
        yield return new WaitForSeconds(0.1f);
        knife1.enabled = true;
        Instantiate(newBall, newBallPos.position, Quaternion.identity);
    }

    IEnumerator knifeR2()
    {
        yield return new WaitForSeconds(0.2f);
        Knifes.ElementAt(Knifes.Count - 1).DOMoveX(1, 0.08f, false).OnComplete(knifeRemove);
        playerImg.fillAmount -= 0.025f;
        KnifePlayer.transform.position -= new Vector3(0, 0.7f, 0);
        newBallPos.transform.position -= new Vector3(0, 0.7f, 0);
        yield return new WaitForSeconds(0.1f);
        Knifes.ElementAt(Knifes.Count - 1).DOMoveX(1, 0.08f, false).OnComplete(knifeRemove);
        playerImg.fillAmount -= 0.025f;
        KnifePlayer.transform.position -= new Vector3(0, 0.7f, 0);
        newBallPos.transform.position -= new Vector3(0, 0.7f, 0);
        yield return new WaitForSeconds(0.1f);
        knife1.enabled = true;
        Instantiate(newBall, newBallPos.position, Quaternion.identity);
    }

    IEnumerator knifeR3()
    {
        yield return new WaitForSeconds(0.2f);
        Knifes.ElementAt(Knifes.Count - 1).DOMoveX(1, 0.08f, false).OnComplete(knifeRemove);
        playerImg.fillAmount -= 0.025f;
        KnifePlayer.transform.position -= new Vector3(0, 0.7f, 0);
        newBallPos.transform.position -= new Vector3(0, 0.7f, 0);
        yield return new WaitForSeconds(0.1f);
        Knifes.ElementAt(Knifes.Count - 1).DOMoveX(1, 0.08f, false).OnComplete(knifeRemove);
        playerImg.fillAmount -= 0.025f;
        KnifePlayer.transform.position -= new Vector3(0, 0.7f, 0);
        newBallPos.transform.position -= new Vector3(0, 0.7f, 0);
        yield return new WaitForSeconds(0.1f);
        Knifes.ElementAt(Knifes.Count - 1).DOMoveX(1, 0.08f, false).OnComplete(knifeRemove);
        playerImg.fillAmount -= 0.025f;
        KnifePlayer.transform.position -= new Vector3(0, 0.7f, 0);
        newBallPos.transform.position -= new Vector3(0, 0.7f, 0);
        yield return new WaitForSeconds(0.2f);
        knife1.enabled = true;
        Instantiate(newBall, newBallPos.position, Quaternion.identity);     
    }

    public void knifeRemove()
    {
        Knifes.ElementAt(Knifes.Count - 1).gameObject.SetActive(false);
        Knifes.RemoveAt(Knifes.Count - 1);
    }
}
