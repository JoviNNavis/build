using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Poles_Script : MonoBehaviour
{
    public List<GameObject> poles;
    void Start()
    {
        upgrade_poles();
    }
    IEnumerator openmoles()
    {

        for (int i = 0; i < poles.Count; i++)
        {
            poles[i].transform.DOMoveY(0.77f, 0.1f, false);
            yield return new WaitForSeconds(0.1f);
        }
    }
    void upgrade_poles()
    {
        StartCoroutine(openmoles());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
