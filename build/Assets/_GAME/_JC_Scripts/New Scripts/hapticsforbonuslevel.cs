using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;
public class hapticsforbonuslevel : MonoBehaviour
{
  
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == "Knife")
        {
            Debug.LogError("bon sou");
            if (FindObjectOfType<ButtonManager>().ishaptic)
            {
                MMVibrationManager.Haptic(HapticTypes.SoftImpact);
            }
          
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
