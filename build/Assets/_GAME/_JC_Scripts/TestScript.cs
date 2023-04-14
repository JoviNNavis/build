using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(starf());
        }
    }

    IEnumerator starf()
    {
        //transform.DOMoveX(2, 2, false).SetEase(Ease.InOutSine);
        yield return new WaitForSeconds(0);
        transform.DOMoveX(-2, 2, false).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
}
