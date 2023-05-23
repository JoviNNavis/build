using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private int currSceneIndex;

    private int loadSceneIndex;

    void Start()
    {
        //if (loadSceneIndex != 0)
        //    SceneManager.LoadScene(loadSceneIndex);
    }

    void Update()
    {

        Debug.LogWarning(loadSceneIndex);
        currSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SavedScene", currSceneIndex);

        loadSceneIndex = PlayerPrefs.GetInt("SavedScene");

        if (loadSceneIndex != 0)
            SceneManager.LoadScene(loadSceneIndex);
        else
            return;
    }
}
