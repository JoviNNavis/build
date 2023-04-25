using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CoinCollectScript : MonoBehaviour
{
    public Transform endPos;

    int delay = 0;

    public AudioClip collect;

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
            SoundManger.soundctrl.playClip(collect);
            if (!this.GetComponent<RectTransform>())
            {
                this.gameObject.AddComponent<RectTransform>();
                this.GetComponent<RectTransform>().anchorMin = Vector2.one;
                this.GetComponent<RectTransform>().anchorMax = Vector2.one;
            }

            transform.DOMove(endPos.position, 0.5f, false).SetEase(Ease.InOutSine);

            transform.DORotate(new Vector3(1, 1, 0.5f), 0.5f).SetDelay(delay + 0.5f).SetEase(Ease.Flash);

            cashScript.cashValue += 1;

            Destroy(this.gameObject, 0.5f);
        }

        if(other.CompareTag("Open"))
        {
            transform.DOMove(endPos.position, 1f, false).SetEase(Ease.InOutSine);

            transform.DORotate(new Vector3(1, 1, 0.5f), 0.5f).SetDelay(delay + 0.5f).SetEase(Ease.Flash);

            cashScript.cashValue += 5;

            Destroy(this.gameObject, 1f);
        }
    }
}
