using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq; 
using DG.Tweening;

public class AiFailScript : MonoBehaviour
{
    public List<Transform> Knifes = new List<Transform>();

    public AiScript ai;

    public GameObject Knifeai;

    public GameObject newBall;
    public Transform newBallPos;

    public Slider aiSlider;

    public float rankValue;

    public AudioClip ballPuncture;

    public Transform emptyObj;

    void Start()
    {
        //Knifes.Add(gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R) && Knifes.Count >= 3)
        {
            //knifeRemove();
            //StartCoroutine(knifeR());
        }

        //for(int i = 0; i < Knifes.Count; i++)
        //{
        //    var firstKnife = Knifes.
        //}
    }

    private void OnCollisionEnter(Collision collision)
    { 

        if (collision.gameObject.tag == "AiBall" && Knifes.Count == 1)
        {
            SoundMangerAi.soundctrl.playClip(ballPuncture);
            Destroy(collision.gameObject, 0.1f);
            ai.enabled = false;
            StartCoroutine(knifeR1());
        }

        if (collision.gameObject.tag == "AiBall" && Knifes.Count == 2)
        {
            SoundMangerAi.soundctrl.playClip(ballPuncture);
            Destroy(collision.gameObject, 0.1f);
            ai.enabled = false;
            StartCoroutine(knifeR2());
        }

        if (collision.gameObject.tag == "AiBall" && Knifes.Count >= 3)
        {
            SoundMangerAi.soundctrl.playClip(ballPuncture);
            Destroy(collision.gameObject, 0.1f);
            ai.enabled = false;
            StartCoroutine(knifeR3());
        }
    }

    IEnumerator knifeR1()
    {
        ai.enabled = false;
        yield return new WaitForSeconds(0.5f);
        Debug.Log("1");
        Knifes.ElementAt(Knifes.Count - 1).DOMoveX(1, 0.03f, false).OnComplete(knifeRemove);
        yield return new WaitForSeconds(0.4f);
        ai.enabled = true;
        GameObject oldBall = Instantiate(newBall, newBallPos.position, Quaternion.identity);
        oldBall.transform.SetParent(emptyObj);
    }

    IEnumerator knifeR2()
    {
        ai.enabled = false;
        yield return new WaitForSeconds(0.5f);
        Debug.Log("1");
        Knifes.ElementAt(Knifes.Count - 1).DOMoveX(1, 0.03f, false).OnComplete(knifeRemove);
        aiSlider.value -= rankValue;
        Knifeai.transform.position -= new Vector3(0, 0.7f, 0);
        newBallPos.transform.position -= new Vector3(0, 0.7f, 0);
        yield return new WaitForSeconds(0.45f);
        Debug.Log("2");
        Knifes.ElementAt(Knifes.Count - 1).DOMoveX(1, 0.03f, false).OnComplete(knifeRemove);
        aiSlider.value -= rankValue;
        Knifeai.transform.position -= new Vector3(0, 0.7f, 0);
        newBallPos.transform.position -= new Vector3(0, 0.7f, 0);
        yield return new WaitForSeconds(0.4f);     
        GameObject oldBall = Instantiate(newBall, newBallPos.position, Quaternion.identity);
        oldBall.transform.SetParent(emptyObj);
        ai.enabled = true;
    }

    IEnumerator knifeR3()
    {
        ai.enabled = false;
        yield return new WaitForSeconds(0.5f);
        Debug.Log("1");
        Knifes.ElementAt(Knifes.Count - 1).DOMoveX(1, 0.03f, false).OnComplete(knifeRemove);
        aiSlider.value -= rankValue;
        Knifeai.transform.position -= new Vector3(0, 0.7f, 0);
        newBallPos.transform.position -= new Vector3(0, 0.7f, 0);
        yield return new WaitForSeconds(0.45f);
        Debug.Log("2");
        Knifes.ElementAt(Knifes.Count - 1).DOMoveX(1, 0.03f, false).OnComplete(knifeRemove);
        aiSlider.value -= rankValue;
        Knifeai.transform.position -= new Vector3(0, 0.7f, 0);
        newBallPos.transform.position -= new Vector3(0, 0.7f, 0);
        yield return new WaitForSeconds(0.45f);
        Knifes.ElementAt(Knifes.Count - 1).DOMoveX(1, 0.03f, false);
        Knifes.ElementAt(Knifes.Count - 1).gameObject.SetActive(false);
        Knifes.RemoveAt(Knifes.Count - 1);
        Debug.Log("3");
        aiSlider.value -= rankValue;
        Knifeai.transform.position -= new Vector3(0, 0.7f, 0);
        newBallPos.transform.position -= new Vector3(0, 0.7f, 0);
        yield return new WaitForSeconds(0.4f);  
        GameObject oldBall = Instantiate(newBall, newBallPos.position, Quaternion.identity);
        oldBall.transform.SetParent(emptyObj);
        ai.enabled = true;
    }

    IEnumerator knifeR4()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("1");
        Knifes.ElementAt(Knifes.Count - 1).DOMoveX(1, 0.03f, false).OnComplete(knifeRemove);
        aiSlider.value -= rankValue;
        Knifeai.transform.position -= new Vector3(0, 0.7f, 0);
        yield return new WaitForSeconds(0.45f);
        Debug.Log("2");
        Knifes.ElementAt(Knifes.Count - 1).DOMoveX(1, 0.03f, false).OnComplete(knifeRemove);
        aiSlider.value -= rankValue;
        Knifeai.transform.position -= new Vector3(0, 0.7f, 0);
        yield return new WaitForSeconds(0.45f);
        Knifes.ElementAt(Knifes.Count - 1).DOMoveX(1, 0.03f, false);
        Knifes.ElementAt(Knifes.Count - 1).gameObject.SetActive(false);
        Knifes.RemoveAt(Knifes.Count - 1);
        Debug.Log("3");
        aiSlider.value -= rankValue;
        Knifeai.transform.position -= new Vector3(0, 0.7f, 0);
        yield return new WaitForSeconds(0.4f);
        ai.enabled = true;
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
