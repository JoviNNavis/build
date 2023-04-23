using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class test_manager : MonoBehaviour
{


   public void with (GameObject SHAPE)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        FindObjectOfType<TEST1>().isskin = true;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
