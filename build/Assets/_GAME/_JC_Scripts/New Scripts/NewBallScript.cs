using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Cinemachine;

public class NewBallScript : MonoBehaviour
{
    public float upForce;
    int level;
    public bool powerup_mode;
    private MeshRenderer water,water1;
    public ParticleSystem _fire;
    public Material skyboxbefore;
    public Material skybox2;
    public bool islevelcompleted;
    public bool changecolor;
    public TrailRenderer tail;
    private Rigidbody Rb;

    public Animator camAnim;

    public AudioClip ballBounce, fireBall; 

    //public KnifeScript knife;
  //  public KnifeScriptSword sword;

    private MeshRenderer _knife;
  public   Color _blue;
  public Color purple;
    [SerializeField] private bool abovelevel5;
    int num,max;
    void Start()
    {
      

        if (level <= 4)
        {
            abovelevel5 = false;
        }
        else
        {
            abovelevel5 = true;
        }
        Scene currentScene = SceneManager.GetActiveScene();
        level = currentScene.buildIndex;
        RenderSettings.fogColor = FindObjectOfType<ColorScript>().fog;

        Debug.Log(level);
    
        water = FindObjectOfType<ButtonManager>().water;
        _blue = new Color32(0, 137, 255,255);
        purple = new Color32(0, 1, 255,255);
        Rb = GetComponent<Rigidbody>();
        abovelevel5 = false;
        _fire.Pause();
        _fire.Clear();
        water.material.SetColor("_BaseColor", _blue);
        changecolor = false;
    }

    // Update is called once per frame
    void Update()
    {
        skyboxbefore = FindObjectOfType<ButtonManager>().BeforeSkybox;
        if (level <= 4)
        {
            abovelevel5 = false;
        }
        else
        {
            abovelevel5 = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            Rb.AddForce(transform.up * upForce);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
          
        }
        if (changecolor)
        {
           
        }
    }


    public void stopwatercolor()
    {
        if (islevelcompleted)
        {
            water.material.SetColor("_BaseColor", _blue);
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
    IEnumerator animations()
    {
      
        Rb.useGravity = false;
        yield return new WaitForSeconds(0.3f);

        Rb.useGravity = true;
    }

    IEnumerator poweranimations()
    {
     
        Rb.useGravity = false;
        yield return new WaitForSeconds(0.4f);

        Rb.useGravity = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Knife"))
        {
            if (FindObjectOfType<Ballpowerup>().time < 0.4f)
            {
                SoundManger.soundctrl.playClip(fireBall);
                camAnim.SetBool("Move", true);
                FindObjectOfType<ButtonManager>().changecolor = true;
              
                float _newUpforce = upForce + 200;
       
             
                StartCoroutine(poweranimations());

              Rb.AddForce(transform.up * _newUpforce, ForceMode.Force);
               
              if(FindObjectOfType<ColorScript>().spikelevel)
                {
                    FindObjectOfType<Starf1>().inpowermode = true;
                }
                powerup_mode = true;
                RenderSettings.skybox = skybox2;
                RenderSettings.fogColor = FindObjectOfType<ColorScript>().after_fog;
                FindObjectOfType<ColorScript>().spikemat.color = FindObjectOfType<ColorScript>().aftercolor;
                _fire.Play();
                if (!abovelevel5)
                {

                    water.material.SetColor("_BaseColor", purple);
                }
              


            }
            else
            {
                _fire.Pause();
                _fire.Clear();
                SoundManger.soundctrl.playClip(ballBounce);
                camAnim.SetBool("Move", false);
                powerup_mode = false;
                FindObjectOfType<ButtonManager>().changecolor = false;

                if (FindObjectOfType<ColorScript>().spikelevel)
                {
                    FindObjectOfType<Starf1>().inpowermode = false;
                }
                StartCoroutine(animations());
                //if (abovelevel5)
                //{
                //    RenderSettings.skybox = skyboxafter5;
                //}
                //else
                //{
                //    RenderSettings.skybox = skyboxbefore5;
                //}
                RenderSettings.skybox = skyboxbefore;
                Rb.mass = 1f;
                water.material.SetColor("_BaseColor", _blue);
                changecolor = false;
                RenderSettings.fogColor = FindObjectOfType<ColorScript>().fog;
                FindObjectOfType<ColorScript>().spikemat.color = FindObjectOfType<ColorScript>().beforecolor;

                Rb.AddForce(transform.up * upForce, ForceMode.Force);
                if (abovelevel5)
                {
                  
                }
            }
        }

        if (collision.gameObject.CompareTag("DKnife"))
        {
            _fire.Pause();
            _fire.Clear();
            SoundManger.soundctrl.playClip(ballBounce);
            camAnim.SetBool("Move", false);
            powerup_mode = false;
            FindObjectOfType<ButtonManager>().changecolor = false;

            if (FindObjectOfType<ColorScript>().spikelevel)
            {
                FindObjectOfType<Starf1>().inpowermode = false;
            }
            StartCoroutine(animations());
            //if (abovelevel5)
            //{
            //    RenderSettings.skybox = skyboxafter5;
            //}
            //else
            //{
            //    RenderSettings.skybox = skyboxbefore5;
            //}
            RenderSettings.skybox = skyboxbefore;
            Rb.mass = 1f;
            water.material.SetColor("_BaseColor", _blue);
            changecolor = false;
            RenderSettings.fogColor = FindObjectOfType<ColorScript>().fog;
            FindObjectOfType<ColorScript>().spikemat.color = FindObjectOfType<ColorScript>().beforecolor;

            Rb.AddForce(transform.up * upForce, ForceMode.Force);
        }

        if (collision.gameObject.CompareTag("End"))
        {
            Debug.Log("U R DEAD");
        }
    }
}
