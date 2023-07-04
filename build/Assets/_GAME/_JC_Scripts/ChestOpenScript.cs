using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChestOpenScript : MonoBehaviour
{
    public Animator anim;
    public CoinScript coin;

    public GameObject glow;
    public GameObject confetti;

    public GameObject rewardPanel, noThanksButton;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {



        //if (mid.isskin)
        //{
        //    transform.localPosition = new Vector3(0, 26f, 0);

        //}
        //else
        //{
        //    transform.localPosition = new Vector3(0, 26f, 0);

        //}
        glow.transform.Rotate(0, 0, 1);
        if(Input.GetKey(KeyCode.E))
        {
            glow.transform.Rotate(0, 0, 1);
            //Top.transform.DORotate(new Vector3(-90, 0, 0), 0.5f, RotateMode.Fast);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Open"))
        {
            StartCoroutine(open());
        }

        
    }

    IEnumerator open()
    {
        yield return new WaitForSeconds(0.4f);
        anim.enabled = true;
        confetti.SetActive(true);
        glow.SetActive(true);
        yield return new WaitForSeconds(1f);
        coin.Reward(10);
        yield return new WaitForSeconds(4f);
        rewardPanel.SetActive(true);
        yield return new WaitForSeconds(3);
        noThanksButton.SetActive(true);
    }

    IEnumerator failOpen()
    {
        yield return new WaitForSeconds(0.5f);
        rewardPanel.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        noThanksButton.SetActive(true);
    }

}
