using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class SpinBall : MonoBehaviour
{
    public GameObject punctureBall;
    public BallSpinScript ball;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Knife"))
        {
            Destroy(other.gameObject);
            ball.Balls.Remove(transform);
            if (FindObjectOfType<ButtonManager>().ishaptic)
            {
                MMVibrationManager.Haptic(HapticTypes.Failure);
                Debug.LogError("vibe");
            }
            //MissedKnife.knifeValue += 1;
            Instantiate(punctureBall, this.transform.position, Quaternion.Euler(0, 70, 0));
            
            ball.isOver = true;
        }
    }
}
