using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class StackScript : MonoBehaviour
{
    public GameObject blast;
    public GameObject stacks;

    public AudioClip knifeHit;
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
            SoundManger.soundctrl.playClip(knifeHit);
            Instantiate(blast, transform.position, Quaternion.Euler(-90, 0, 0));
            //stacks.transform.localPosition -= new Vector3(0, 0.5f, 0);
            stacks.transform.DOMoveY(stacks.transform.position.y - 0.15f, 0.1f, false);
    
            Destroy(other.gameObject);
            Destroy(this.gameObject, 0.1f);
        }
    }
}
