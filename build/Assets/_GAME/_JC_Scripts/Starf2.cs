using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Starf2 : MonoBehaviour
{
    private MeshCollider meshCol;
    public AiFailScript aiFail;

    void Start()
    {
        StartCoroutine(starf());
        meshCol = GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AiKnife"))
        {
            aiFail.failed();
            Destroy(this.gameObject, 0.5f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("AiPref"))
        {
            this.enabled = false;
            meshCol.enabled = false;
            Destroy(this.gameObject, 0.25f);
        }
    }

    IEnumerator starf()
    {
        transform.DOLocalMoveZ(-1.53f, 1f, false).SetEase(Ease.InOutSine);
        yield return new WaitForSeconds(0);
        transform.DOLocalMoveZ(1.53f, 1f, false).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
}
