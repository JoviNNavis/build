using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cubes : MonoBehaviour
{
    public int health;
    public MeshRenderer cube;
    public Material before, after;
    public enum placement
    {
        right ,
        left
    };
    public placement direction;
    public bool isright, isleft;
    float noofcubes;
    void Start()
    {
        noofcubes = FindObjectOfType<box_Sculpting>().cubeposes.Count;
    
        health = 100;
        cube.material = before;
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            Debug.LogError("Touched");
            health -= FindObjectOfType<clicks>().knifepower;
            Instantiate(FindObjectOfType<box_Sculpting>().falling, gameObject.transform.position + new Vector3(0, 0, 0), Quaternion.identity);
           // FindObjectOfType<box_Sculpting>().filbar.fillAmount +=  0.0625f;
        }
    }
        //public bool Rotate_knife()
        //   {
        //       switch (direction)
        //       {
        //           case placement.left:
        //               return isleft = true;

        //           case placement.right:
        //               return isright = true;
        //       }
        //       return isright = false;
        //   }



        //public Quater move_knife()
        //{
        //    switch (placement)
        //    {
        //        case placement.left:
        //            return FindObjectOfType<clicks>().knife.transform.DOLocalRotate(new Vector3(-0, 0, 18), 0.2f, RotateMode.Fast);
        //        case placement.right:
        //            return FindObjectOfType<clicks>().knife.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.2f, RotateMode.Fast);
        //    }
        //}

        void Update()
        {
    
        if (health <= 50) {
                cube.material = after;
            }
            if (health <= 0)
            {
                Destroy(this.gameObject,0.3f);
                FindObjectOfType<box_Sculpting>().cubeposes.Remove(this.gameObject.transform);
          //  FindObjectOfType<box_Sculpting>().filbar.fillAmount = 1 / noofcubes;

        }
        }
    }
