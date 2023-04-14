using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusWin : MonoBehaviour
{
    public UpScript up;

    public BonusKnifeScript knife;

    public GameObject confetti, ui, rewardPnael, button, target;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Knife"))
        {
            Destroy(target);
            up.enabled = false;
            knife.enabled = false;
            ui.SetActive(false);
            confetti.SetActive(true);
            StartCoroutine(win());
        }
    }

    IEnumerator win()
    {
        yield return new WaitForSeconds(0.8f);
        rewardPnael.SetActive(true);
        yield return new WaitForSeconds(2f);
        button.SetActive(true);
    }
}
