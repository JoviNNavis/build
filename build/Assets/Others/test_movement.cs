using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class test_movement : MonoBehaviour
{
    public List<Transform> Positions;
  public  float time;
   public int randno;
   public bool isenable;
    void Start()
    {
        isenable = true;   
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        movement();
        if ((time >= 4.9f))
        {
            randno += 1;
            time = 0;
        }
        if (randno >= Positions.Count)
        {
            randno = 0;
        }

    }
    IEnumerator delaymoving()
    {
        yield return new WaitForSeconds(0.5f);
    }
    void movement()

    {
        if (isenable)
        {
            gameObject.transform.DOMove(Positions[randno].transform.position, 0.5f, false);
        }
      }
}
