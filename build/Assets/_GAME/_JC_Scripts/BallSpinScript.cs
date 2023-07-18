using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpinScript : MonoBehaviour
{
    
    public newKnifeScript knife;
    public newKnifeScript1 knife1;
    public bool isOver= false;
    public static bool istouch;
    bool islevelbelow5;
    public GameObject missKniePanel;
    public GameObject losePanel, newlosePanel;

    public List<Transform> Balls ;
    public List<Animator> ANIMBalls;

    float timer = 0;
    void Start()
    {
        for (int i = 0; i < ANIMBalls.Count; i++)
        {
            ANIMBalls[i].enabled = true;
        }
    }


    void Update()
    {
        islevelbelow5 = FindObjectOfType<ButtonManager>().isbelowlevel5;

        //istouch = isOver;

        if (!islevelbelow5)
        {


            if (isOver == true && knife.knifeCount == 0)
            {
                StartCoroutine(lose());
            }

            timer += 1 * Time.deltaTime;

            if (timer >= 0 && timer <= 3.8)
            {
                transform.Rotate(0, 1.5f, 0);

                for (int i = 0; i < Balls.Count; i++)
                {
                    //   Balls[i].transform.Rotate(0, 2f, 0);
                }
            }

            if (timer >= 4 && timer <= 8.8)
            {
                transform.Rotate(0, -1.5f, 0);

                for (int i = 0; i < Balls.Count; i++)
                {
                    //    Balls[i].transform.Rotate(0, -2f, 0);
                }
            }

            if (timer >= 9 && timer <= 14.8)
            {
                transform.Rotate(0, 1.5f, 0);

                for (int i = 0; i < Balls.Count; i++)
                {
                    //     Balls[i].transform.Rotate(0, 2f, 0);
                }
            }

            if (timer >= 15 && timer <= 20.8)
            {
                transform.Rotate(0, -1.5f, 0);

                for (int i = 0; i < Balls.Count; i++)
                {
                    //     Balls[i].transform.Rotate(0, 2f, 0);
                }
            }


            if (timer >= 21 && timer <= 23.8)
            {
                transform.Rotate(0, 1.5f, 0);

                for (int i = 0; i < Balls.Count; i++)
                {
                    //     Balls[i].transform.Rotate(0, 2f, 0);
                }
            }

            if (timer >= 24 && timer <= 29.8)
            {
                transform.Rotate(0, -1.5f, 0);

                for (int i = 0; i < Balls.Count; i++)
                {
                    //     Balls[i].transform.Rotate(0, 2f, 0);
                }
            }

            if (timer >= 30 && timer <= 33.8)
            {
                transform.Rotate(0, 1.5f, 0);

                for (int i = 0; i < Balls.Count; i++)
                {
                    //     Balls[i].transform.Rotate(0, 2f, 0);
                }
            }

            if (timer >= 34 && timer <= 37.8)
            {
                transform.Rotate(0, -1.5f, 0);

                for (int i = 0; i < Balls.Count; i++)
                {
                    //     Balls[i].transform.Rotate(0, 2f, 0);
                }
            }

            if (timer >= 38 && timer <= 43.8)
            {
                transform.Rotate(0, 1.5f, 0);

                for (int i = 0; i < Balls.Count; i++)
                {
                    //     Balls[i].transform.Rotate(0, 2f, 0);
                }
            }

            if (timer >= 44 && timer <= 49.8)
            {
                transform.Rotate(0, -1.5f, 0);

                for (int i = 0; i < Balls.Count; i++)
                {
                    //     Balls[i].transform.Rotate(0, 2f, 0);
                }
            }

            if (timer > 50)
            {
                transform.Rotate(0, 1.5f, 0);

                for (int i = 0; i < Balls.Count; i++)
                {
                    //     Balls[i].transform.Rotate(0, 2f, 0);
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

            if (timer >= 0 && timer <= 3.8)
            {
                transform.Rotate(0, 1.5f, 0);

                for (int i = 0; i < Balls.Count; i++)
                {
                    //   Balls[i].transform.Rotate(0, 2f, 0);
                }
            }

            if (timer >= 4 && timer <= 8.8)
            {
                transform.Rotate(0, -1.5f, 0);

                for (int i = 0; i < Balls.Count; i++)
                {
                    //    Balls[i].transform.Rotate(0, -2f, 0);
                }
            }

            if (timer >= 9 && timer <= 14.8)
            {
                transform.Rotate(0, 1.5f, 0);

                for (int i = 0; i < Balls.Count; i++)
                {
                    //     Balls[i].transform.Rotate(0, 2f, 0);
                }
            }

            if (timer >= 15 && timer <= 20.8)
            {
                transform.Rotate(0, -1.5f, 0);

                for (int i = 0; i < Balls.Count; i++)
                {
                    //     Balls[i].transform.Rotate(0, 2f, 0);
                }
            }


            if (timer >= 21 && timer <= 23.8)
            {
                transform.Rotate(0, 1.5f, 0);

                for (int i = 0; i < Balls.Count; i++)
                {
                    //     Balls[i].transform.Rotate(0, 2f, 0);
                }
            }

            if (timer >= 24 && timer <= 29.8)
            {
                transform.Rotate(0, -1.5f, 0);

                for (int i = 0; i < Balls.Count; i++)
                {
                    //     Balls[i].transform.Rotate(0, 2f, 0);
                }
            }

            if (timer >= 30 && timer <= 33.8)
            {
                transform.Rotate(0, 1.5f, 0);

                for (int i = 0; i < Balls.Count; i++)
                {
                    //     Balls[i].transform.Rotate(0, 2f, 0);
                }
            }

            if (timer >= 34 && timer <= 37.8)
            {
                transform.Rotate(0, -1.5f, 0);

                for (int i = 0; i < Balls.Count; i++)
                {
                    //     Balls[i].transform.Rotate(0, 2f, 0);
                }
            }

            if (timer >= 38 && timer <= 43.8)
            {
                transform.Rotate(0, 1.5f, 0);

                for (int i = 0; i < Balls.Count; i++)
                {
                    //     Balls[i].transform.Rotate(0, 2f, 0);
                }
            }

            if (timer >= 44 && timer <= 49.8)
            {
                transform.Rotate(0, -1.5f, 0);

                for (int i = 0; i < Balls.Count; i++)
                {
                    //     Balls[i].transform.Rotate(0, 2f, 0);
                }
            }

            if (timer > 50)
            {
                transform.Rotate(0, 1.5f, 0);

                for (int i = 0; i < Balls.Count; i++)
                {
                    //     Balls[i].transform.Rotate(0, 2f, 0);
                }
            }
        }

        //chestlost(isOver);
    }

    public void chestlost(bool isclicked)
    {
        isclicked = isOver;

        if(isclicked)
        {
            losePanel.SetActive(false);
            newlosePanel.SetActive(true);
        }
        else
        {
            newlosePanel.SetActive(false);
            losePanel.SetActive(true);
            
        }
    }

    IEnumerator lose()
    {
        yield return new WaitForSeconds(0.75f);
        missKniePanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        Destroy(missKniePanel);
        losePanel.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        //this.enabled = false;
    }
}
