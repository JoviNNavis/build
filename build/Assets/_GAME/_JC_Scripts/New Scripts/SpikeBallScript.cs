using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBallScript : MonoBehaviour
{
    public float upForce;
    public bool powerup_mode;
    public bool islevelcompleted;
    public ParticleSystem _fire;
    public Material skybox;
    public Material skybox2;
    public TrailRenderer tail;
    private Rigidbody Rb;
    public Collider _coll;
    public Animator camAnim;

    public AudioClip ballBounce, fireBallbounce;

    void Start()
    {
        RenderSettings.skybox = skybox;
        RenderSettings.fogColor = FindObjectOfType<ColorScript>().fog;
        RenderSettings.fogColor = new Color32(207,207,207,255);
        _coll = GetComponent<Collider>();


        Rb = GetComponent<Rigidbody>();
        _fire.Pause();
        _fire.Clear();
    }
    public void stopwatercolor()
    {
        if (islevelcompleted)
        {
          //  water.material.SetColor("_BaseColor", _blue);
            //if (abovelevel5)
            //{
            //    RenderSettings.skybox = skyboxafter5;
            //}
            //else
            //{
            //    RenderSettings.skybox = skyboxbefore5;
            //}

        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            Rb.AddForce(transform.up * upForce);
        }
    }
    IEnumerator colliders()
    {

        _coll.enabled = false;
        yield return new WaitForSeconds(0.025f);
        _coll.enabled = true;
        //  Rb.useGravity = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Knife"))
        {
            if (FindObjectOfType<Ballpowerup>().time < 0.6f)
            {
                SoundManger.soundctrl.playClip(fireBallbounce);
                camAnim.SetBool("Move", true);
                FindObjectOfType<ButtonManager>().changecolor = true;
                RenderSettings.fogColor = FindObjectOfType<ColorScript>().after_fog;
                FindObjectOfType<Starf1>().inpowermode = true;
                StartCoroutine(colliders());
                float _newUpforce = upForce + 150;
                Rb.AddForce(transform.up * _newUpforce, ForceMode.Force);
                powerup_mode = true;
                RenderSettings.skybox = skybox2;
                tail.enabled = false;
                _fire.Play();
              
             FindObjectOfType<ColorScript>().spikemat.color = FindObjectOfType<ColorScript>().aftercolor;

            }
            else
            {
                SoundManger.soundctrl.playClip(ballBounce);
                _fire.Pause();
                _fire.Clear();
                FindObjectOfType<Starf1>().inpowermode = false;
                StartCoroutine(colliders());
                camAnim.SetBool("Move", false);
                powerup_mode = false;
                FindObjectOfType<ButtonManager>().changecolor = false;
                RenderSettings.fogColor = FindObjectOfType<ColorScript>().fog;


                RenderSettings.skybox = skybox;
                Rb.AddForce(transform.up * upForce, ForceMode.Force);
               
                FindObjectOfType<ColorScript>().spikemat.color = FindObjectOfType<ColorScript>().beforecolor;

              

            }
        }

        if (collision.gameObject.CompareTag("DKnife"))
        {
            SoundManger.soundctrl.playClip(ballBounce);
            _fire.Pause();
            _fire.Clear();
            FindObjectOfType<Starf1>().inpowermode = false;

            camAnim.SetBool("Move", false);
            powerup_mode = false;
            FindObjectOfType<ButtonManager>().changecolor = false;
            RenderSettings.fogColor = FindObjectOfType<ColorScript>().fog;


            RenderSettings.skybox = skybox;
            Rb.AddForce(transform.up * upForce, ForceMode.Force);

            FindObjectOfType<ColorScript>().spikemat.color = FindObjectOfType<ColorScript>().beforecolor;
        }

        if (collision.gameObject.CompareTag("End"))
        {
            Debug.Log("U R DEAD");
        }
    }
}
