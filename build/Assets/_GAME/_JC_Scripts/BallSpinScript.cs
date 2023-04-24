using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpinScript : MonoBehaviour
{
    
    public newKnifeScript knife;
    public newKnifeScript1 knife1;
    public bool isOver= false;
    bool islevelbelow5;
    public GameObject losePanel;

    public List<Transform> Balls = new List<Transform>();

    float timer = 0;
    void Start()
    {

    }


    void Update()
    {
        islevelbelow5 = FindObjectOfType<ButtonManager>().isbelowlevel5;

        if (!islevelbelow5)
        {


            if (isOver == true && knife.knifeCount == 0)
            {
                StartCoroutine(lose());
            }

            timer += 1 * Time.deltaTime;

            if (timer >= 0 && timer <= 12.8)
            {
                transform.Rotate(0, 1.5f, 0);

                for (int i = 0; i < Balls.Count; i++)
                {
                    Balls[i].transform.Rotate(0, 0.75f, 0);
                }
            }

            if (timer >= 13 && timer <= 27.8)
            {
                transform.Rotate(0, -1.5f, 0);

                for (int i = 0; i < Balls.Count; i++)
                {
                    Balls[i].transform.Rotate(0, -0.75f, 0);
                }
            }

            if (timer > 28)
            {
                transform.Rotate(0, 1.5f, 0);

                for (int i = 0; i < Balls.Count; i++)
                {
                    Balls[i].transform.Rotate(0, 0.75f, 0);
                }
            }
        }
        else
        {

            if (isOver == true && knife1.knifeCount == 0)
            {
                StartCoroutine(lose());
            }

            timer += 1 * Time.deltaTime;

            if (timer >= 0 && timer <= 12.8)
            {
                transform.Rotate(0, 1.5f, 0);

                for (int i = 0; i < Balls.Count; i++)
                {
                    Balls[i].transform.Rotate(0, 0.75f, 0);
                }
            }

            if (timer >= 13 && timer <= 27.8)
            {
                transform.Rotate(0, -1.5f, 0);

                for (int i = 0; i < Balls.Count; i++)
                {
                    Balls[i].transform.Rotate(0, -0.75f, 0);
                }
            }

            if (timer > 28)
            {
                transform.Rotate(0, 1.5f, 0);

                for (int i = 0; i < Balls.Count; i++)
                {
                    Balls[i].transform.Rotate(0, 0.75f, 0);
                }
            }
        
    }

    }

    IEnumerator lose()
    {
        yield return new WaitForSeconds(1f);
        losePanel.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        this.enabled = false;
    }
}
