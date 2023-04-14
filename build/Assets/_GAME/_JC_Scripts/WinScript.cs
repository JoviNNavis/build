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
    public GameObject text, target;
    public GameObject lostPanel;
    public bool islevelcompleted;
    public bool isLost = false;

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
    private void OnTriggerEnter(Collider other)
    {
        if(isLost == false && other.CompareTag("Knife"))
        {
            playerKnife.enabled = false;
            FindObjectOfType<KnifeScript>().counterText.SetActive(false);

            islevelcompleted = true;
              stopwatercolor();
            

           
            aiKnife.enabled = false;
            lvl.SetActive(false);
            blast.SetActive(true);
            winText.SetActive(true);
            StartCoroutine(winJump());
            Destroy(text);
            Destroy(target);
        }

        if (other.CompareTag("AiKnife"))
        {
            isLost = true;
            aiKnife.enabled = false;
        }

        if (isLost == true && other.CompareTag("Knife"))
        {
            playerKnife.enabled = false;
            aiKnife.enabled = false;
            lvl.SetActive(false);
            retry.SetActive(false);
            Destroy(text);
            Destroy(target);
            StartCoroutine(gameLost());        
        }
    }

    IEnumerator winJump()
    { 
        yield return new WaitForSeconds(3.5f);
        newBall.SetActive(true);
        newBall.transform.DOJump(jumpPos.position, 2, 1, 1, false);
        newBall.transform.SetParent(jumpPos.transform, true);
    }

    IEnumerator gameLost()
    {
        yield return new WaitForSeconds(1f);
        lostPanel.SetActive(true);
    }
}
