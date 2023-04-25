using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SelectionScript : MonoBehaviour
{
    public GameObject shootButton, cam1, cam2;
    public GameObject baner1, baner2;
    public GameObject player1, player2;

    public GameObject[] names;

    public AudioClip playerSelect;


    void Start()
    {
        baner1.transform.DOMoveX(Screen.width/2, 0.5f, false).SetEase(Ease.InOutSine);
        StartCoroutine(selection());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator selection()
    {
        
        yield return new WaitForSeconds(1f);
        baner2.transform.DOMoveX(Screen.width / 2, 0.5f, false).SetEase(Ease.InOutSine);
        yield return new WaitForSeconds(0.09f);
        SoundManger.soundctrl.playClip(playerSelect);
        names[0].SetActive(false);
        names[1].SetActive(true);
        yield return new WaitForSeconds(0.1f);
        names[1].SetActive(false);
        names[2].SetActive(true);
        yield return new WaitForSeconds(0.12f);
        names[2].SetActive(false);
        names[3].SetActive(true);
        yield return new WaitForSeconds(0.13f);
        names[3].SetActive(false);
        names[4].SetActive(true);
        yield return new WaitForSeconds(0.14f);
        names[4].SetActive(false);
        names[5].SetActive(true);
        yield return new WaitForSeconds(0.15f);
        names[5].SetActive(false);
        names[0].SetActive(true);
        yield return new WaitForSeconds(0.16f);
        names[0].SetActive(false);
        names[1].SetActive(true);
        yield return new WaitForSeconds(0.17f);
        names[1].SetActive(false);
        names[2].SetActive(true);
        yield return new WaitForSeconds(0.18f);
        names[2].SetActive(false);
        names[3].SetActive(true);
        yield return new WaitForSeconds(0.19f);
        names[3].SetActive(false);
        names[4].SetActive(true);
        yield return new WaitForSeconds(0.2f);
        names[4].SetActive(false);
        names[5].SetActive(true);
        yield return new WaitForSeconds(0.25f);
        player1.SetActive(true);
        player2.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        cam1.SetActive(false);
       // cam2.SetActive(true);
        shootButton.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
