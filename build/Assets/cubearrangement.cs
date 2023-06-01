using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubearrangement : MonoBehaviour
{
    public List<GameObject> cubes;
   
    void Start()
    {


    }


    void Update()
    {
        for (int i = 1; i < cubes.Count; i++)
        {
            cubes[i].transform.position += new Vector3(0, 0.7f, 0);
        }
    }
}
