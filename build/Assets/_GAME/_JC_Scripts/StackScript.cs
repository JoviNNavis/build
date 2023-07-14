using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MoreMountains.NiceVibrations;

public class StackScript : MonoBehaviour
{
    public GameObject blast;
    public GameObject stacks;

    public AudioClip knifeHit;

    public StackCounterScript stackCount;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0.25f, 0);
      

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Knife")
        {
            for(int i = 0;  i < stackCount.stacks.Count; i++)
            {        
                stackCount.stacks.Remove(transform);
            }
            SoundManger.soundctrl.playClip(knifeHit);
            Instantiate(blast, transform.position, Quaternion.Euler(-90, 0, 0));
            //stacks.transform.localPosition -= new Vector3(0, 0.5f, 0);
            stacks.transform.DOLocalMoveY(stacks.transform.localPosition.y - 0.5f, 0f, false);
            if (FindObjectOfType<ButtonManager>().ishaptic)
            {
                MMVibrationManager.Haptic(HapticTypes.Success);
            }
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
