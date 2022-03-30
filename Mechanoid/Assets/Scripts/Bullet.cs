using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bangParticleEffect;

    private Vector3 currentPos;
    private Vector3 prevPos;
    private float damage;

    void Start()
    {
        currentPos = transform.position;
        damage = 25f;
    }

    void Update()
    {
        prevPos = currentPos;
        currentPos = transform.position;

        RaycastHit hit;

        if (Physics.Linecast(prevPos, currentPos, out hit))
        {
            explode(hit.transform.gameObject);
        }
    }

    void OnCollisionEnter(Collision obj)
    {
        explode(obj.gameObject);
    }

    void explode(GameObject target)
    {
        if (target.tag != "Projectile" && target.tag != "Player" && target.tag != "Invisible Wall")
        {
            GameObject particleEffet = (GameObject) Instantiate(bangParticleEffect, prevPos, transform.rotation);

            if (target.tag == "Enemy")
            {
                target.GetComponent<Enemy>().takeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}