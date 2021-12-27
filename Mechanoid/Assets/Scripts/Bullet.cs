using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bangParticleEffect;

    private float lifetime;

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag != "Player")
        {
            GameObject particleEffet = (GameObject) Instantiate(bangParticleEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
