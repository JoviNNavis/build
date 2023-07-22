using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MoreMountains.NiceVibrations;
public class Cubes : MonoBehaviour
{
    public int health;
    public MeshRenderer cube;
    public float inte;
    public Material before, after;
    public enum placement
    {
        right ,
        left
    };
    public placement direction;
    public bool isright, isleft;
    public  int noofcubes;
    void Start()
    {
   
        inte = 0.125f;
        health = 100;
        cube.material = before;
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {

            if (FindObjectOfType<ButtonManager>().ishaptic)
            {
                MMVibrationManager.Haptic(HapticTypes.SoftImpact);
                Debug.LogWarning("haptics");
            }
            Debug.LogError("Touched");
            health -= FindObjectOfType<clicks>().knifepower;
           FindObjectOfType<box_Sculpting>().filbar.fillAmount +=  0.0007f;
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
        //            return FindObjectOfType<clicks>().knife.transform.DOLocalRotate(new Vector3(-0, 0, 18), rotate_time, RotateMode.Fast);
        //        case placement.right:
        //            return FindObjectOfType<clicks>().knife.transform.DOLocalRotate(new Vector3(0, 0, 0), rotate_time, RotateMode.Fast);
        //    }
        //}

        void Update()
        {
        noofcubes = FindObjectOfType<box_Sculpting>().cubeposes.Count;
        if (health <= 50) {
                cube.material = after;
            }
            if (health <= 0)
            {
                Destroy(this.gameObject);
                FindObjectOfType<box_Sculpting>().cubeposes.Remove(this.gameObject.transform);
          FindObjectOfType<box_Sculpting>().filbar.fillAmount += inte;
            Instantiate(FindObjectOfType<box_Sculpting>().falling, gameObject.transform.position + new Vector3(0, 0, 0), Quaternion.identity);
            Instantiate(FindObjectOfType<box_Sculpting>().blast, gameObject.transform.position + new Vector3(0, 0, 0), Quaternion.identity);

        }
    }
    }
