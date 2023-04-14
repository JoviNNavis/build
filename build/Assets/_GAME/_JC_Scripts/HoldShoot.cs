using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldShoot : MonoBehaviour
{
    private bool isTouch;

    private bool isRelease = false;

    public float fireRate;

    [SerializeField] private float fireTime;
    [SerializeField] private float nextfireRate;

    public GameObject bullet;

    public Transform spawnPos;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                isTouch = true;
            }
            if(touch.phase == TouchPhase.Ended)
            {
                isTouch = false;
            }

            if(isTouch)
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
            Instantiate(bullet, spawnPos.position, Quaternion.Euler(0, 180, 0));
            transform.position += new Vector3(0, 0.8f, 0);
            transform.rotation = Quaternion.Euler(90, 0, 0);
            fireTime = 0;
        }
    }
}
