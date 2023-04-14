using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RewardChestScript : MonoBehaviour
{
    public GameObject chest, glow, reward1, reward2, reward3, next;

    public RectTransform rwd1, rwd2, rwd3;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(rewardShow());
    }

    IEnumerator rewardShow()
    {
        chest.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        glow.SetActive(true);
        reward1.SetActive(true);
        rwd1.transform.DOLocalMove(new Vector3(0, 590, 0), 0.85f, false);
        yield return new WaitForSeconds(1f);
        reward2.SetActive(true);
        rwd2.transform.DOLocalMove(new Vector3(-590, 590, 0), 0.85f, false);
        yield return new WaitForSeconds(1f);
        reward3.SetActive(true);
        rwd3.transform.DOLocalMove(new Vector3(590, 590, 0), 0.85f, false);
        yield return new WaitForSeconds(1f);
        next.SetActive(true);
    }
}
