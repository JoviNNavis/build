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

    public GameObject aiUiPos;

    public AudioClip ballPuncture;

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
        yield return new WaitForSeconds(0.5f);
        Debug.Log("1");
        Knifes.ElementAt(Knifes.Count - 1).DOMoveX(1, 0.03f, false).OnComplete(knifeRemove);
        yield return new WaitForSeconds(0.4f);
        ai.enabled = true;
        Instantiate(newBall, newBallPos.position, Quaternion.identity);
    }

    IEnumerator knifeR2()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("1");
        Knifes.ElementAt(Knifes.Count - 1).DOMoveX(1, 0.03f, false).OnComplete(knifeRemove);
        aiUiPos.transform.position -= new Vector3(7.8f, 0, 0);
        Knifeai.transform.position -= new Vector3(0, 0.7f, 0);
        newBallPos.transform.position -= new Vector3(0, 0.7f, 0);
        yield return new WaitForSeconds(0.45f);
        Debug.Log("2");
        Knifes.ElementAt(Knifes.Count - 1).DOMoveX(1, 0.03f, false).OnComplete(knifeRemove);
        aiUiPos.transform.position -= new Vector3(7.8f, 0, 0);
        Knifeai.transform.position -= new Vector3(0, 0.7f, 0);
        newBallPos.transform.position -= new Vector3(0, 0.7f, 0);
        yield return new WaitForSeconds(0.4f);
        ai.enabled = true;
        Instantiate(newBall, newBallPos.position, Quaternion.identity);

    }

    IEnumerator knifeR3()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("1");
        Knifes.ElementAt(Knifes.Count - 1).DOMoveX(1, 0.03f, false).OnComplete(knifeRemove);
        aiUiPos.transform.position -= new Vector3(7.8f, 0, 0);
        Knifeai.transform.position -= new Vector3(0, 0.7f, 0);
        newBallPos.transform.position -= new Vector3(0, 0.7f, 0);
        yield return new WaitForSeconds(0.45f);
        Debug.Log("2");
        Knifes.ElementAt(Knifes.Count - 1).DOMoveX(1, 0.03f, false).OnComplete(knifeRemove);
        aiUiPos.transform.position -= new Vector3(7.8f, 0, 0);
        Knifeai.transform.position -= new Vector3(0, 0.7f, 0);
        newBallPos.transform.position -= new Vector3(0, 0.7f, 0);
        yield return new WaitForSeconds(0.45f);
        Knifes.ElementAt(Knifes.Count - 1).DOMoveX(1, 0.03f, false);
        Knifes.ElementAt(Knifes.Count - 1).gameObject.SetActive(false);
        Knifes.RemoveAt(Knifes.Count - 1);
        Debug.Log("3");
        aiUiPos.transform.position -= new Vector3(7.8f, 0, 0);
        Knifeai.transform.position -= new Vector3(0, 0.7f, 0);
        newBallPos.transform.position -= new Vector3(0, 0.7f, 0);
        yield return new WaitForSeconds(0.4f);
        ai.enabled = true;
        Instantiate(newBall, newBallPos.position, Quaternion.identity);
    }

    IEnumerator knifeR4()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("1");
        Knifes.ElementAt(Knifes.Count - 1).DOMoveX(1, 0.03f, false).OnComplete(knifeRemove);
        aiUiPos.transform.position -= new Vector3(7.8f, 0, 0);
        Knifeai.transform.position -= new Vector3(0, 0.7f, 0);
        yield return new WaitForSeconds(0.45f);
        Debug.Log("2");
        Knifes.ElementAt(Knifes.Count - 1).DOMoveX(1, 0.03f, false).OnComplete(knifeRemove);
        aiUiPos.transform.position -= new Vector3(7.8f, 0, 0);
        Knifeai.transform.position -= new Vector3(0, 0.7f, 0);
        yield return new WaitForSeconds(0.45f);
        Knifes.ElementAt(Knifes.Count - 1).DOMoveX(1, 0.03f, false);
        Knifes.ElementAt(Knifes.Count - 1).gameObject.SetActive(false);
        Knifes.RemoveAt(Knifes.Count - 1);
        Debug.Log("3");
        aiUiPos.transform.position -= new Vector3(7.8f, 0, 0);
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
