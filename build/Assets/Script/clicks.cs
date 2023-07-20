using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class clicks : MonoBehaviour
{
    int power = 100;
    public int knifepower;
  [SerializeField] 
    private List<GameObject> Cubes;
    public GameObject[] _Cubes;

    public GameObject knife;
    public GameObject sword;
    public Animator bear;
    public Animator knife_anim;

    void Start()
    {

        bear.enabled = false;
        Cubes.Add(GameObject.Find("Top Right Blocks"));
        //Cubes.Add(GameObject.Find("Top left Blocks"));
        //Cubes.Add(GameObject.Find("Down rIGHT Blocks"));
        //Cubes.Add(GameObject.Find("Down left Blocks"));
        //Cubes.Add(GameObject.Find("Top Right Blocks"));
        _Cubes = gameObject.GetComponentsInChildren<GameObject>();     
    }
  

    void Update()
    {

        if (power == 0)
        {
            Destroy(this.gameObject, 0.5f);
        }
    }
 
}
