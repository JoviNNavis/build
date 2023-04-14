using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Dreamteck.Splines;
public class PlayerMove_Spline : MonoBehaviour
{
    public static PlayerMove_Spline instance;
    [Range(0, 10)]
    public float playerSpeed;
    public float swipespeed;
    public float rotationSpeed;
    public float maxDistance;
    public float lastTime;
    public SplineFollower splineFollower;
    public Animator animator;
    private bool isGameStart;
    public bool isEditor;
    private RaycastHit hit;
    public GameObject home;
    public Transform rayPositon;
    public List<GameObject> bodyParts = new List<GameObject>();
    //public CameraParent CP; //================ Camera movment along with player
    
    private void Awake()
    {
        if (!instance)
            instance = this;
    }
    void Start()
    {
        //CP = FindObjectOfType<CameraParent>();//=========== Camera Setup
        splineFollower.followSpeed = 0;
        isGameStart = true;
    }

    public void RunPlayer()
    {
        //============= Set Player Spilne Settings
        splineFollower.followSpeed = playerSpeed;
        splineFollower.follow = true;
        //============= Set Camera Settings
        //CP.splineFollower.follow = true;
        //============= Set Player Animations
        animator.SetBool("Run", true);
        animator.SetBool("Idle", false);
    }
    public void StopPlayer()
    {
        //============ Set Player Spline Settings
        splineFollower.followSpeed = 0;
        //============ Set Camera Settings
        //CP.splineFollower.follow = false;
        //============ Set Player Animation Settings
        animator.SetBool("Run", false);
        animator.SetBool("Idle", true);

    }
    #region Movement
    public void Movement()
    {
        if (isEditor)
        {
            //swipespeed = 500;
            float rotX = Input.GetAxis("Mouse X") * swipespeed * Mathf.Deg2Rad;
            Vector2 pos = new Vector3(rotX * Time.deltaTime, 0);
            splineFollower.motion.offset += pos;
            splineFollower.motion.offset = new Vector3(Mathf.Clamp(splineFollower.motion.offset.x, -1.25f, 1.25f), 0, 0);
            Debug.Log("CLICKING");
        }
        else
        {
            Debug.Log("NOT CLICKING");

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                float rotX = touchDeltaPosition.x * swipespeed * Mathf.Deg2Rad;
                Vector2 pos = new Vector3(rotX * Time.deltaTime, 0);
                splineFollower.motion.offset += pos;
                splineFollower.motion.offset = new Vector3(Mathf.Clamp(splineFollower.motion.offset.x, -1.25f, 1.25f), 0, 0);
                Debug.Log("CLICKING");
                home.SetActive(true);
            }
            else
            {
                Debug.Log("NOT CLICKING");
                home.SetActive(false);
            }
        }

        if(!isEditor)
        {
            Debug.Log("NOT CLICKING");
        }

    }
    float ClampAngle(float angle, float min, float max)
    {
        if (min < 0 && max > 0 && (angle > max || angle < min))
        {
            angle -= 360;
            if (angle > max || angle < min)
            {
                if (Mathf.Abs(Mathf.DeltaAngle(angle, min)) < Mathf.Abs(Mathf.DeltaAngle(angle, max))) return min;
                else return max;
            }
        }
        else if (min > 0 && (angle > max || angle < min))
        {
            angle += 360;
            if (angle > max || angle < min)
            {
                if (Mathf.Abs(Mathf.DeltaAngle(angle, min)) < Mathf.Abs(Mathf.DeltaAngle(angle, max))) return min;
                else return max;
            }
        }
        if (angle < min) return min;
        else if (angle > max) return max;
        else return angle;
    }
    #endregion
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Movement();
        }
    }
}
