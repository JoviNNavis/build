using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class CoinScript : MonoBehaviour
{
    [SerializeField] private GameObject coinGrp;
    [SerializeField] private Text Counter;
    [SerializeField] private Vector3[] InitialPos;
    [SerializeField] private Quaternion[] InitialRot;
    [SerializeField] private int CoinNo;

    private int CoinValue = 5;

    public AudioClip coinCollect;
    void Start()
    {
        InitialPos = new Vector3[CoinNo];
        InitialRot = new Quaternion[CoinNo];

        for(int i = 0; i < coinGrp.transform.childCount; i++)
        {
            InitialPos[i] = coinGrp.transform.GetChild(i).position;
            InitialRot[i] = coinGrp.transform.GetChild(i).rotation;
        }
    }

    private void Reset()
    {
        for (int i = 0; i < coinGrp.transform.childCount; i++)
        {
            coinGrp.transform.GetChild(i).position = InitialPos[i];
            coinGrp.transform.GetChild(i).rotation = InitialRot[i];
        }
    }

    public void Reward(int noCoin)
    {
        Reset();

        var delay = 0f;

        coinGrp.SetActive(true);

        for (int i = 0; i < coinGrp.transform.childCount; i++)
        {
            coinGrp.transform.GetChild(i).DOScale(0.6f, 0.3f).SetDelay(delay).SetEase(Ease.OutBack);

            coinGrp.transform.GetChild(i).GetComponent<RectTransform>().DOAnchorPos(new Vector2(840, 1454), 1).SetDelay(delay + 1f).SetEase(Ease.InBack);
            //coinGrp.transform.GetChild(i).GetComponent<RectTransform>().DOAnchorPos(new Vector2(970, 1760), 1).SetDelay(delay + 1f).SetEase(Ease.InBack);

            coinGrp.transform.GetChild(i).DORotate(Vector3.zero, 0.5f).SetDelay(delay + 0.5f).SetEase(Ease.Flash);

            coinGrp.transform.GetChild(i).DOScale(0, 0.3f).SetDelay(delay + 1.9f).SetEase(Ease.OutBack).OnComplete(coinCountByComplete);

            delay += 0.2f;
        }

        //StartCoroutine(coinCount(10));
    }

    void coinCountByComplete()
    {
        //PlayerPrefs.SetInt("Countcoin", PlayerPrefs.GetInt("Countcoin") + 50);

        //Counter.text = PlayerPrefs.GetInt("Countcoin").ToString();
        SoundManger.soundctrl.playClip(coinCollect);

        cashScript.cashValue += CoinValue;

        CoinValue += 10;
    }

    IEnumerator coinCount(int coinNo)
    {
        yield return new WaitForSecondsRealtime(0.8f);

        var timer = 0f;

        for (int i = 0; i < coinNo; i++)
        {
            //PlayerPrefs.SetInt("Countcoin", PlayerPrefs.GetInt("Countcoin") + 1);

            //Counter.text = PlayerPrefs.GetInt("Countcoin").ToString();

            timer += 0.05f;

            yield return new WaitForSecondsRealtime(timer);
        }
    }
}
