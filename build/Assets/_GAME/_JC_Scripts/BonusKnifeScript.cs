using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusKnifeScript : MonoBehaviour
{
    private bool isTouch;

    private bool isRelease = false;

    public float fireRate;

    [SerializeField] private float fireTime;
    [SerializeField] private float nextfireRate;

    public GameObject knife;

    public float posX;

    public Image playerImg;

    public float fillValue;

    public AudioClip knifeThrow;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(3f, 0, 0);
        playerImg.fillAmount += fillValue * Time.deltaTime;

        if (Input.GetMouseButton(1))
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
                //playerImg.fillAmount += fillValue;
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
            SoundManger.soundctrl.playClip(knifeThrow);
            Instantiate(knife, new Vector3(posX, transform.localPosition.y, transform.localPosition.z), Quaternion.identity);
            transform.rotation = Quaternion.Euler(90, -180, 0);
            fireTime = 0;
        }
    }
}
