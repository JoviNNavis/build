using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AiScript : MonoBehaviour
{
    public float fireRate1;
    public GameObject[] knifemat;
    [SerializeField]private float fireTime1;
    [SerializeField]private float nextfireRate1;

    public Transform newBallPos;
    // public bool isaicolor;
    public Slider aiSlider;
   public int rndclr;
    public float rankValue;
   public bool aicolor;
    public GameObject Knife, newKnife;

    public AudioClip knifeThrow;

    void Start()
    {
        //fireRate1 = 3f;
    }

    private void FixedUpdate()
    {
       //StartCoroutine(shoot());
    }

    private void LateUpdate()
    {
        //StartCoroutine(shoot());
    }


    void Update()
    {
       
        transform.Rotate(0.5f, 0, 0);
        aicolor = FindObjectOfType<ButtonManager>().isaicolor;
        StartCoroutine(shoot());
    }

    IEnumerator shoot()
    {
        yield return new WaitForSeconds(1f);
        Shooting1();
    }

    void Shooting1()
    {
        fireTime1 += Time.deltaTime;
        nextfireRate1 = 2 / fireRate1;

        //nextfireRate1 = fireRate;

        if (fireTime1 >= nextfireRate1)
        {
            
            if (aicolor)
            {

                GameObject aiobject = Instantiate(knifemat[rndclr], transform.position, Quaternion.Euler(0, 180, 0));
                FindObjectOfType<AiFailScript>().Knifes.Add(aiobject.gameObject.transform);
                fireRate1 = 20f;
                rndclr++;
                fireTime1 = 0.05f;
                transform.position += new Vector3(0, 0.7f, 0);
                newBallPos.transform.position += new Vector3(0, 0.7f, 0);
                transform.rotation = Quaternion.Euler(90, 0, 0);
                aiSlider.value += rankValue;
                //fireTime1 = 0;
                if (rndclr >= knifemat.Length)
                {
                    rndclr = 0;
                }

            }
            else
            {
                SoundMangerAi.soundctrl.playClip(knifeThrow);
                GameObject aiobject = Instantiate(Knife, transform.position, Quaternion.Euler(0, 180, 0));
                FindObjectOfType<AiFailScript>().Knifes.Add(aiobject.gameObject.transform);
                fireRate1 = 18f;
                aiSlider.value += rankValue;
                transform.position += new Vector3(0, 0.7f, 0);
                newBallPos.transform.position += new Vector3(0, 0.7f, 0);
                transform.rotation = Quaternion.Euler(90, 0, 0);
                fireTime1 = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Finish"))
        {
            newKnife.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
