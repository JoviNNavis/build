using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMangerAi : MonoBehaviour
{
    public static SoundMangerAi soundctrl = null;
    public AudioSource audioSrcAi;
    private void Awake()
    {

        if (soundctrl == null)
        {
            soundctrl = this;
        }
        else if (soundctrl != this)
        {
            Destroy(gameObject);
        }
    }

    public void playClip(AudioClip clip)
    {
        audioSrcAi.PlayOneShot(clip);
    }
}
