using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Poles_Script : MonoBehaviour
{
    public List<GameObject> poles;
    public List<GameObject> ROPES;
    public List<GameObject> _coins;
    void Start()
    {
        for (int i = 0; i < ROPES.Count; i++)
        {
            ROPES[i].SetActive(false);
        }
        for (int i = 0; i < _coins.Count; i++)
        {
            _coins[i].SetActive(false);
        }

        upgrade_poles();
      StartCoroutine(coinsupgrade());
    }
    IEnumerator openmoles()
    {

        for (int i = 0; i < poles.Count; i++)
        {
            poles[i].transform.DOMoveY(0.77f, 0.1f, false);
            yield return new WaitForSeconds(0.001f);
            
            ROPES[i].SetActive(true);
            yield return new WaitForSeconds(0.0005f);
            ROPES[i].transform.DOLocalMoveY(0.5f, 0.1F, false);

        }
    }
    IEnumerator coinsupgrade()
    {

        for (int i = 0; i < _coins.Count; i++)
        {
            
            yield return new WaitForSeconds(18);
            _coins[i].SetActive(true);

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
