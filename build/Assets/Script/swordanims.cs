using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class swordanims : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {

        GameObject target = other.gameObject;

        if (target.tag == "TopRightBlocks")
        {

            
            FindObjectOfType<clicks>().sword.transform.DOLocalRotate(new Vector3(19.7071342f, 219.14183f, 342.294434f), 0.2f, RotateMode.Fast);

          
        }
        if (target.tag == "TopFront")
        {
      
            FindObjectOfType<clicks>().sword.transform.DOLocalRotate(new Vector3(19.7071419f, 314.762787f, 342.294434f), 0.2f, RotateMode.Fast);
            

          
        }
        if (target.tag == "TopBack")
        {
        
            FindObjectOfType<clicks>().sword.transform.DOLocalRotate(new Vector3(14.7361193f, 129.330276f, 347.400208f), 0.2f, RotateMode.Fast);

        }
        if (target.tag == "DownFront")
        {
      FindObjectOfType<clicks>().sword.transform.DOLocalRotate(new Vector3(351.164124f, 317.431671f, 9.10415077f), 0.2f, RotateMode.Fast);




        }
        if (target.tag == "DownBack")
        {

            FindObjectOfType<clicks>().sword.transform.DOLocalRotate(new Vector3(351.164124f, 129.584412f, 9.10415268f), 0.2f, RotateMode.Fast);



           
        }

        if (target.tag == "TopLeftBlocks")
        {


       

      
            FindObjectOfType<clicks>().sword.transform.DOLocalRotate(new Vector3(19.7071419f, 12.5f, 342.294434f), 0.2f, RotateMode.Fast);

           
        }

        if (target.tag == "DownRightBlocks")
        {

       
            FindObjectOfType<clicks>().sword.transform.DOLocalRotate(new Vector3(14.7361107f, 236.080597f, 347.400208f), 0.2f, RotateMode.Fast);

     
          
        }
        if (other.gameObject.tag == "DownLeftBlocks")
        {

  
            FindObjectOfType<clicks>().sword.transform.DOLocalRotate(new Vector3(13.3320484f, 19.1664238f, 5.41082191f), 0.2f, RotateMode.Fast);
          
        }
        if (target.tag == "MiddleBlocks")
        {

        

        }
    }
 
    void Update()
    {
        
    }
}
