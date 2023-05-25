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
            if (FindObjectOfType<ButtonManager>().ishaptic)
            {
                MMVibrationManager.Haptic(HapticTypes.Failure);
                Debug.LogError("vibe");
            }
            //MissedKnife.knifeValue += 1;
            ball.Balls.Remove(transform);
            Destroy(other.gameObject);
            ball.isOver = true;
        }
    }
}
