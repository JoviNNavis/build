using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WinScript1 : MonoBehaviour
{
    public GameObject blast, winText;
    public GameObject lvl, retry, target, text;
    
    public KnifeScript1 playerKnife;

    void Start()
    {
        
    }

 
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Knife"))
        {
            FindObjectOfType<KnifeScript1>().countText.SetActive(false);
            playerKnife.enabled = false;
            lvl.SetActive(false);
            FindObjectOfType<KnifeScript1>().combo.SetActive(false);

            retry.SetActive(false);
            blast.SetActive(true);
            winText.SetActive(true);
            text.SetActive(false);
            Destroy(target);
        }
    }
}
