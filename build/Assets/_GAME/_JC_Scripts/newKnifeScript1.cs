using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class newKnifeScript1 : MonoBehaviour
{
    private bool isTouch;

    private bool isRelease = false;

    public float fireRate;
    [SerializeField] int level;
    [SerializeField] private float fireTime;
    [SerializeField] private float nextfireRate;
    public float xpos;
    public GameObject knife;
 
    public Text knifeCountText;
    public int knifeCount;

    public float spanwPosy;
   
    void Start()
    {
        xpos = gameObject.transform.position.x - 2f;
       
    }

    // Update is called once per frame
    void Update()
    {

        knifeCountText.text = "" + knifeCount;
        if(knifeCount == 0)
        {
            knifeCountText.text = "";
            this.enabled = false;

        }

        transform.Rotate(0.5f, 0, 0);
        if (Input.GetMouseButton(0))
        {
            Shooting();
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                isTouch = true;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                isTouch = false;
            }

            if (isTouch)
            {
                Shooting();
            }
        }
    }

    void Shooting()
    {
        fireTime += Time.deltaTime;
        nextfireRate = 1 / fireRate;

        if (fireTime >= nextfireRate)
        {

            Instantiate(knife, new Vector3(xpos, spanwPosy, transform.localPosition.z), Quaternion.Euler(-90, 0, 0));

            
            knifeCount -= 1;
            transform.rotation = Quaternion.Euler(90, -180, 0);
            fireTime = 0;
        }
    }
}
