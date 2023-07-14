using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class KnifePrefabScript : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    public Transform tower;
    public GameObject drops;
    public GameObject plane;
    //public Animator anim;
    public AudioClip hit, bonusBallHit;

    public bool isNew;
  //  public MeshRenderer knife;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
       // anim = GetComponent<Animator>();
        //plane.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        transform.position -= new Vector3(0.7f, 0, 0) * speed * Time.deltaTime;

    }
  

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Block") || other.gameObject.CompareTag("Finish"))
        {
            GameObject effects = Instantiate(drops, other.transform.position + new Vector3(1, 0, -0.4f), Quaternion.Euler(0, -270, 0));
            Destroy(effects, 0.15f);
            //Instantiate(plane, other.transform.position+new  Vector3(0.77f, -0.1f, -0.15f), Quaternion.Euler(0, -90, 0));
            plane.SetActive(true);
            transform.position = new Vector3(-0.581f, transform.localPosition.y, transform.localPosition.z);
            // StartCoroutine(disable());
            this.enabled = false;
        }

        if (other.gameObject.CompareTag("AfterBall"))
        {
            SoundManger.soundctrl.playClip(bonusBallHit);
            speed = 0;
            other.transform.SetParent(this.transform, true);
            transform.DOJump(new Vector3(10, 10, 10), 5, 1, 4);
            transform.position -= new Vector3(0f, 0, 0);
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }

   // IEnumerator disableanim()
   // {
   //anim.SetBool("static", true);
   //     yield return new WaitForSeconds(0.3f);
   // anim.SetBool("static", false);

   // }
    IEnumerator disable()
    {
        yield return new WaitForSeconds(1f);
        //jelly.enabled = false;
    }
}
