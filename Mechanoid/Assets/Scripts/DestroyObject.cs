using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public float expireAfter;

    private float lifetime;

    void Start()
    {
        lifetime = Time.time + expireAfter;
    }

    void Update()
    {
        if (Time.time > lifetime)
        {
            Destroy(gameObject);
        }
    }
}
