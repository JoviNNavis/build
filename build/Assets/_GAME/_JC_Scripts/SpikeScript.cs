using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    public BonusKnifeScript knife;
    public GameObject afterspike;
    public MeshRenderer mesh;
    public Animator anim;

    void Start()
    {
        afterspike.SetActive(false);
        mesh = GetComponent<MeshRenderer>();
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
            StartCoroutine(knifeDisable());
            mesh.enabled = false;
        afterspike.SetActive(true);

           
        }
    }

    IEnumerator knifeDisable()
    {
        knife.enabled = false;
        anim.SetBool("Fill", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("Fill", false);
        knife.enabled = true;
    }
}
