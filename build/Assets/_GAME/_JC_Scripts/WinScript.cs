using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WinScript : MonoBehaviour
{
    public GameObject blast, winText;
    public GameObject lvl, retry;

    
    public GameObject newBall;
    public Transform jumpPos;
    public bool spikelevel;
    public KnifeScript playerKnife;
    public AiScript aiKnife;
    private Color _blue;
    private MeshRenderer water;
    public GameObject text, target, aiTarget, comboText;
    public GameObject lostPanel;
    public bool islevelcompleted;
    public bool isLost = false;

    public GameObject emptyObj;

    public GameObject bonuschestBanner;

    public RayBall rayball;

    public GameObject playerNewObj, aiNewObj;

    public GameObject ball;

    public Transform ballPos;

    public bool BelowLevel5;

    public FailScript1 fail;

    public NewFailScript newFail;

    void Start()
    {
        water = FindObjectOfType<ButtonManager>().water;
        _blue = new Color32(0, 137, 255, 255);
       
    }

    
    void Update()
    {
        
    }
    public void stopwatercolor()
    {
        if (islevelcompleted)
        {
            water.material.SetColor("_BaseColor", _blue);
            

        }
    }

    IEnumerator winJump()
    {
        yield return new WaitForSeconds(3.5f);
        newBall.SetActive(true);
        newBall.transform.DOJump(jumpPos.position, 2, 1, 1, false);
        newBall.transform.SetParent(jumpPos.transform, true);
        bonuschestBanner.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        playerNewObj.SetActive(false);
        aiNewObj.SetActive(false);
        bonuschestBanner.SetActive(false);
    }

    IEnumerator gameLost()
    {
        yield return new WaitForSeconds(1f);
        playerNewObj.SetActive(false);
        lostPanel.SetActive(true);
        aiNewObj.SetActive(false);

    }

    IEnumerator ballOff()
    {
        yield return new WaitForSeconds(0.5f);
        aiNewObj.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isLost == false && other.CompareTag("Knife"))
        {
            
            playerKnife.enabled = false;
            //FindObjectOfType<KnifeScript>().counterText.SetActive(false);

            islevelcompleted = true;
              stopwatercolor();
            //FindObjectOfType<KnifeScript>().combo.SetActive(false);
            aiKnife.enabled = false;
            lvl.SetActive(false);
            blast.SetActive(true);
            winText.SetActive(true);
            StartCoroutine(winJump());
            Destroy(text);
            Destroy(comboText);
            Destroy(target);
            rayball.enabled = false;
        }


        if (isLost == true && other.CompareTag("Knife"))
        {
            
            playerKnife.enabled = false;
            aiKnife.enabled = false;
            lvl.SetActive(false);
            //FindObjectOfType<KnifeScript>().combo.SetActive(false);
            retry.SetActive(false);
            StartCoroutine(gameLost());
            rayball.enabled = false;
            Destroy(text);
            Destroy(target);
            
        }


        if (other.CompareTag("AiKnife"))
        {
            isLost = true;
            aiKnife.enabled = false;
            aiTarget.SetActive(false);
            StartCoroutine(ballOff());
            Destroy(aiNewObj, 0.5f);
            rayball.enabled = false;
        }

        if(BelowLevel5)
        {
            if (other.CompareTag("Ball") && fail.Knifes.Count > fail.winValue)
            {

                Destroy(other.gameObject, 0.1f);
                GameObject neBall = Instantiate(ball, ballPos.transform.position, Quaternion.identity);
                Destroy(neBall, 2f);
            }
        }

        if(!BelowLevel5 && !newFail.isSkinEnabled)
        {
            if (other.CompareTag("Ball") && fail.Knifes.Count > fail.winValue)
            {

                Destroy(other.gameObject, 0.1f);
                GameObject neBall = Instantiate(ball, ballPos.transform.position, Quaternion.identity);
                Destroy(neBall, 2f);
            }
        }
        

        if (newFail.isSkinEnabled)
        {
            if (other.CompareTag("Ball") && newFail.Knifes.Count > newFail.winValue)
            {

                Destroy(other.gameObject, 0.1f);
                GameObject neBall = Instantiate(ball, ballPos.transform.position, Quaternion.identity);
                Destroy(neBall, 2f);
            }
        }
    
              
        
    }

}
