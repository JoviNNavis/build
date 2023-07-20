using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class SpinBall : MonoBehaviour
{
    public GameObject punctureBall;
    public BallSpinScript ball;

    public bool islvl3;
    public bool islvl4;
    public bool islvl5;

    public bool islvl8;
    public bool islvl9;
    public bool islvl10;
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
            if(islvl3)
            {
                mid.lvl3Fail = true;

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

            if (islvl4)
            {
                lvl4Fail.lvl4fail = true;
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

            if (islvl5)
            {
                mid.lvl5fail = true;
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

            if (islvl8)
            {
                mid.lvl8fail = true;

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

            if (islvl9)
            {
                mid.lvl9fail = true;

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

            if (islvl10)
            {
                mid.lvl10fail = true;

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

            else
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
}
