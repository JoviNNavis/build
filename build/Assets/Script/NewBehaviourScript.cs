using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject StartObject;
    public GameObject Endobject;
    private Vector3 InitialScale;

    private void Start()
    {
        InitialScale = transform.localScale;
        UpdateTransformForScale();
    }

    void Update()
    {
        if (StartObject.transform.hasChanged || Endobject.transform.hasChanged)
        {
            UpdateTransformForScale();
        }
    }

    public void UpdateTransformForScale()
    {
        float distance =  Vector3.Distance( StartObject.transform.position, Endobject.transform.position); //Change Scale
        transform.localScale = new Vector3(InitialScale.x, distance, InitialScale.z); // /2 for cylinder

        Vector3 middlePoint = (StartObject.transform.position + Endobject.transform.position) /2; //Change Position
        transform.position = middlePoint;
        
        Vector3 rotation_direction = (Endobject.transform.position - StartObject.transform.position); //Change Rotation
        transform.up = rotation_direction;

    }
}