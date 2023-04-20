using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class box_Sculpting : MonoBehaviour
{
    int clicks;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI powertext;
    // public ParticleSystem confitee;
    public int noofcubes;
    public GameObject fingeranim;
    public int Power_coincount;
    public int money_coincount;
    public int speed_coincount;
    public TextMeshProUGUI speedtext;
    public int randno;
    public bool _isleft, _isright;
    public float time;
    public Animator knife;
    public Animator sword;
    bool _isautomatic;
[SerializeField]   private float incval;
    public ParticleSystem falling;
    public GameObject[] positions;
    public MeshRenderer mesh;
    public List<Transform> cubeposes;
    public List<MeshCollider> colliders;

    public Image filbar;
    public Image image;

    public GameObject Panel;
    void Start()
    {
        knife.enabled = false;
        sword.enabled = false;
        _isautomatic = true;
       
        Debug.Log(incval);
        moneyText.text = money_coincount.ToString();
        speedtext.text = speed_coincount.ToString();
        powertext.text = Power_coincount.ToString();
        fingeranim.SetActive(true);
        filbar.fillAmount = 0;
    }



    public void power()
    {
        if(money_coincount >= Power_coincount)
        {
            FindObjectOfType<clicks>().knifepower = +10;
            Power_coincount += 20;
            powertext.text = Power_coincount.ToString();
            moneyText.text = ((int)money_coincount).ToString();
        }
     
    }
    IEnumerator knifeanim()
    {
        knife.enabled = true;
        sword.enabled = true;
        yield return new WaitForSeconds(0.80f);
        knife.enabled = false;
        sword.enabled = false;


    }

    IEnumerator automatic()
    {
        FindObjectOfType<clicks>().sword.transform.DOMove(cubeposes[randno].position, 0.2f, false);
        yield return new WaitForSeconds(1f);
        FindObjectOfType<clicks>().sword.transform.DOMove(cubeposes[randno].position - new Vector3(2.5f, 0, 0), 0.2f, false);
    }
    void auto()
    {

        if (_isautomatic)
        {
            
            StartCoroutine(automatic());
        }
    }
    IEnumerator animdelay()
    {
        for(int i = 0; i <= colliders.Count; i++)
        {
            colliders[i].enabled = false;
            yield return new WaitForSeconds(0.3f);
            colliders[i].enabled = true;
        }
     
    }
    void clicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

         if(FindObjectOfType<box_Sculpting>().cubeposes.Count == 0)
            {
                FindObjectOfType<clicks>().bear.enabled = true;
             
                StartCoroutine(win());
            }

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                  GameObject  target = hit.collider.gameObject;
                   // filbar.fillAmount += incval;
                    StartCoroutine(knifeanim());
                
                    if (target.tag == "TopRightBlocks")
                    {
              
                        FindObjectOfType<clicks>().knife.transform.DOJump(hit.transform.gameObject.transform.position + new Vector3(1.2f, 0.3f, 0.5f), 5, 1, 0.3f, false);
                        FindObjectOfType<clicks>().sword.transform.DOJump(hit.transform.gameObject.transform.position + new Vector3(1.2f, 0.3f, 0.5f), 5, 1, 0.3f, false);
                        FindObjectOfType<clicks>().knife.transform.DOLocalRotate(new Vector3(-0, 150, -18), 0.2f, RotateMode.Fast);
                        FindObjectOfType<clicks>().sword.transform.DOLocalRotate(new Vector3(-0, 150, -25), 0.2f, RotateMode.Fast);
                        
                        money_coincount += 20;
                        moneyText.text = money_coincount.ToString();
                    }

                       if (target.tag == "TopLeftBlocks")
                    {

                        FindObjectOfType<clicks>().knife.transform.DOJump(hit.transform.gameObject.transform.position+ new Vector3(-1.2f, 0.3f, -0.31f), 5, 1, 0.3f, false);
                        FindObjectOfType<clicks>().sword.transform.DOJump(hit.transform.gameObject.transform.position + new Vector3(-1.2f, 0.3f, -0.31f), 5, 1, 0.3f, false);

                        FindObjectOfType<clicks>().knife.transform.DOLocalRotate(new Vector3(-0, 0, -18), 0.2f, RotateMode.Fast);
                        FindObjectOfType<clicks>().sword.transform.DOLocalRotate(new Vector3(-0, 0, -25), 0.2f, RotateMode.Fast);

                        money_coincount += 20;
                        moneyText.text = money_coincount.ToString();
                    }

                       if (target.tag == "DownRightBlocks")
                    {
        
                        FindObjectOfType<clicks>().knife.transform.DOJump(hit.transform.gameObject.transform.position + new Vector3(1.2f, -0.6f, 0f), 5, 1, 0.3f, false);
                        FindObjectOfType<clicks>().sword.transform.DOJump(hit.transform.gameObject.transform.position + new Vector3(1.2f, -0.6f, 0f), 5, 1, 0.3f, false);

                        FindObjectOfType<clicks>().knife.transform.DOLocalRotate(new Vector3(-0, -150, 18), 0.2f, RotateMode.Fast);
                        FindObjectOfType<clicks>().sword.transform.DOLocalRotate(new Vector3(-0, -150, 25), 0.2f, RotateMode.Fast);

                        money_coincount += 20;
                        moneyText.text = money_coincount.ToString();
                    }
                       if (target.tag == "DownLeftBlocks")
                    {
             
                        FindObjectOfType<clicks>().knife.transform.DOJump(hit.transform.gameObject.transform.position+new Vector3(-1.2f, -0.6f, -0), 5, 1, 0.3f, false);
                        FindObjectOfType<clicks>().sword.transform.DOJump(hit.transform.gameObject.transform.position + new Vector3(-1.2f, -0.6f, -0), 5, 1, 0.3f, false);

                        FindObjectOfType<clicks>().knife.transform.DOLocalRotate(new Vector3(-0, -0, 18), 0.2f, RotateMode.Fast);
                        FindObjectOfType<clicks>().sword.transform.DOLocalRotate(new Vector3(-0, -0, 25), 0.2f, RotateMode.Fast);

                        money_coincount += 20;
                        moneyText.text = money_coincount.ToString();
                   
                    }
                    if (target.tag == "MiddleBlocks")
                    {
              
                        FindObjectOfType<clicks>().knife.transform.DOJump(hit.transform.gameObject.transform.position + new Vector3(-1.2f, -0.0f, 0f), 5, 1, 0.3f, false);
                        FindObjectOfType<clicks>().sword.transform.DOJump(hit.transform.gameObject.transform.position + new Vector3(-1.2f, -0.0f, 0f), 5, 1, 0.3f, false);


                    }


                }

            }
        }
       
    }

    IEnumerator win()
    {
        yield return new WaitForSeconds(3f);
        Panel.SetActive(true);
    }

    
    void Update()
    {
        if(cubeposes.Count == 0){
            Destroy(image);
        }
       // filbar.fillAmount = 1 / cubeposes.Count;
        if(cubeposes == null)
        {
            cubeposes.Clear();
        }
        time += Time.deltaTime;
   
        if ((time >= 4.9f))
        {
            randno += 1;
            time = 0;
            _isautomatic = true;
        }
        if(time > 0.7)
        {
            _isautomatic = false;
        }
        if (randno >= cubeposes.Count)
        {
            _isautomatic = false;
        }
      
        clicked();
        if (Input.GetMouseButtonDown(0))
        {
            fingeranim.SetActive(false);

            auto();
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                fingeranim.SetActive(false);

                auto();

            }
        }

        }
}
