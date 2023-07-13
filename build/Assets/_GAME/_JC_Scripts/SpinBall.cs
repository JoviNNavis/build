using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class SpinBall : MonoBehaviour
{
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
            ball.Balls.Remove(transform);
            if (FindObjectOfType<ButtonManager>().ishaptic)
            {
                MMVibrationManager.Haptic(HapticTypes.Failure);
                Debug.LogError("vibe");
            }
            //MissedKnife.knifeValue += 1;
            
            Destroy(other.gameObject);
            ball.isOver = true;
        }
    }
}
