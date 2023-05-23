using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Saveload : MonoBehaviour
{
    private int scenenumtocontinue;


    public void continuegame()
    {
        scenenumtocontinue = PlayerPrefs.GetInt("SavedScene");

        if (scenenumtocontinue != 0)
            SceneManager.LoadScene(scenenumtocontinue);
        else
            return;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
