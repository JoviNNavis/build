using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubearrangement : MonoBehaviour
{
    public List<GameObject> cubes;
   
    void Start()
    {
        for (int i = 0; i < cubes.Count; i++)
        {
            cubes[i].transform.position = new Vector3(0, cubes[i].transform.position.y + 0.7f, 0);
        }


    }


    void Update()
    {

    }
}
