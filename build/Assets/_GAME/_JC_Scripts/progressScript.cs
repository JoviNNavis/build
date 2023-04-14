using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class progressScript : MonoBehaviour
{
    public GameObject tick, arrow, nxtButton;

    public float endValue;

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        StartCoroutine(over());
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator over()
    {
        tick.SetActive(true);
        yield return new WaitForSeconds(1f);
        arrow.transform.DOLocalMoveX(endValue, 1, false);
        yield return new WaitForSeconds(1.5f);
        nxtButton.SetActive(true);
    }
}
