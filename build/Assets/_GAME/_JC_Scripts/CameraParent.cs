using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class CameraParent : MonoBehaviour
{
    public static CameraParent instance;
    public SplineFollower splineFollower;
    public bool Follow;
    private void Awake()
    {
        if(!instance)
            instance = this;
    }
    void Start()
    {
        splineFollower = GetComponent<SplineFollower>();
    }

    public void StartFollow()
    {
         splineFollower.follow = true;
    }
    public void StopCamera()
    {
        splineFollower.follow = false;
    }
    private void Update()
    {
        splineFollower.followSpeed = PlayerMove_Spline.instance.playerSpeed;
    }
}
