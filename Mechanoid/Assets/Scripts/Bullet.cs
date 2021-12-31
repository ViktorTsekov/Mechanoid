using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bangParticleEffect;

    private Vector3 currentPos;
    private Vector3 prevPos;

    void Start()
    {
        currentPos = transform.position;
    }

    void Update()
    {
        prevPos = currentPos;
        currentPos = transform.position;

        RaycastHit hit;

        if (Physics.Linecast(prevPos, currentPos, out hit))
        {
            if (hit.transform.gameObject.tag != "Projectile" && hit.transform.gameObject.tag != "Player")
            {
                GameObject particleEffet = (GameObject) Instantiate(bangParticleEffect, prevPos, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
