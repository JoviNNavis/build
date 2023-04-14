using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostScript : MonoBehaviour
{
    public GameObject tick, nxtButton;

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
        nxtButton.SetActive(true);
    }
}
