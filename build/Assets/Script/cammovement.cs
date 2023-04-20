using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class cammovement : MonoBehaviour
{
    [SerializeField] private Camera cam;
    bool toucheddown, touchedup;
    [SerializeField] public Transform target;
    [SerializeField] public Transform targetpos;

    [SerializeField] private float distanceToTarget ;
    public float axis;
    private Vector3 previousPosition;
    [SerializeField] private float xaxis;

    private void Start()
    {
        toucheddown = false;
        touchedup = false;
      xaxis = cam.transform.localEulerAngles.x;

    }




    void rotate1()
    {
        axis = cam.transform.localRotation.x;



        if (Input.GetMouseButtonUp(0))
        {
             if ((cam.transform.localEulerAngles.x < xaxis-20))
            {
                cam.transform.eulerAngles = new Vector3(xaxis - 19.95f, cam.transform.localEulerAngles.y, 0);
     
            }

            
            
            else if ((cam.transform.localEulerAngles.x > xaxis + 7) )
            {
                cam.transform.eulerAngles = new Vector3(xaxis + 6.95f, cam.transform.localEulerAngles.y, 0);
             
            }
        }


        if (Input.GetMouseButtonDown(0))
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            Vector3 direction = previousPosition - newPosition;
                float rotationAroundYAxis = -direction.x * 180;
            float rotationAroundXAxispos = direction.y * 180;
          

            cam.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World); // <— This is what makes it work!

            if ((cam.transform.localEulerAngles.x < xaxis + 7) && (cam.transform.localEulerAngles.x > xaxis - 20))
            {


                cam.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxispos);

       

            }
            else 
            {
                cam.transform.Rotate(new Vector3(0, 0, 0), rotationAroundXAxispos);
             
            }

            cam.transform.position = target.localPosition+new Vector3(0.6f,-2.04f,0.1f) ;
            cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));

            previousPosition = newPosition;
        }

    }
    void Update()
    {

        rotate1();
    }

}
   