using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private static GameObject dontDestroyInstance;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (dontDestroyInstance == null)
        {
            dontDestroyInstance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
