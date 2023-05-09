using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combotext : MonoBehaviour
{
    public int combovalue;
    public bool islevel1;
    void Start()
    {
        combovalue = 1 ;
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Knife"))
        {
    
            if(islevel1)
            {
                if (FindObjectOfType<Ballpowerup>().time < 0.5f)
                {
                    FindObjectOfType<KnifeScript1>().combo.SetActive(true);
                    combovalue++;

                }
                else
                {
                    knifeCounter.knifeCountValue = knifeCounter.knifeCountValue * combovalue;

                    FindObjectOfType<KnifeScript1>().combo.SetActive(false);
                    combovalue = 1;

                }
            }
            else
            {
                    if (FindObjectOfType<Ballpowerup>().time < 0.5f)
                    {
                        FindObjectOfType<KnifeScript>().combo.SetActive(true);
                        combovalue++;

                    }
                    else
                    {
                        knifeCounter.knifeCountValue = knifeCounter.knifeCountValue * combovalue;

                        FindObjectOfType<KnifeScript>().combo.SetActive(false);
                        combovalue = 1;

                    }
                
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (islevel1)
        {
            FindObjectOfType<KnifeScript1>()._text.text = combovalue.ToString();
        }
        else
        {
            FindObjectOfType<KnifeScript>()._text.text = combovalue.ToString();
        }

    }
}
