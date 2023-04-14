using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton1<T> : MonoBehaviour where T : MonoSingleton1<T>
{
    private static volatile T instance;

    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType(typeof(T)) as T;
            }
            return instance;
        }
    }


}
