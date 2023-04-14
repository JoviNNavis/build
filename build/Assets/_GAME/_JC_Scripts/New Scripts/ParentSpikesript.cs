using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ParentSpikesript : MonoBehaviour
{

 
    public bool spikehit;
    public Animator anim;
    public GameObject beforeonr, afterone;
    public MeshRenderer mesh;
    public Collider meshcol;
    public Collider childcol;
    // Start is called before the first frame update
    void Start()
    {

        spikehit = false;
        anim = GetComponentInChildren<Animator>();
        mesh = beforeonr.GetComponentInChildren<MeshRenderer>();
        meshcol = beforeonr.GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Knife"))
        {

            anim.enabled = false;
            mesh.material.DOColor(Color.gray, 0.3f);
            childcol.enabled = false;

        }
        }
    void Update()
    {
        childcol = FindObjectOfType<Starf1>().meshCol;
    }
}
