using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public GameObject bangParticleEffect;

    private float speed;
    private float rotationSpeed;
    private float startHoming;
    private float damage;

    private Vector3 direction;
    private Vector3 currentPos;
    private Vector3 prevPos;

    private GameObject target;

    void Start()
    {
        startHoming = Time.time + 1f;
        speed = 40f;
        rotationSpeed = 5f;
        damage = 100f;
        currentPos = transform.position;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        prevPos = currentPos;
        currentPos = transform.position;

        RaycastHit hit;

        if (Physics.Linecast(prevPos, currentPos, out hit))
        {
            if (hit.transform.gameObject.tag != "Projectile" && hit.transform.gameObject.tag != "Player" && hit.transform.gameObject.tag != "Invisible Wall")
            {
                explode(hit);
            }
        }

        if (Time.time > startHoming)
        {
            findTarget();
            homing(hit);
        }
    }

    private void findTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float minDist = Mathf.Infinity;

        if(enemies.Length == 0)
        {
            Destroy(gameObject);
        }

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(enemy.transform.position, transform.position);

            if (dist < minDist)
            {
                target = enemy;
                minDist = dist;
            }
        }
    }

    private void homing(RaycastHit hit)
    {
        direction = target.transform.position - transform.position;
        direction = direction.normalized;
        var rot = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotationSpeed * Time.deltaTime);
        float dist = Vector3.Distance(target.transform.position, transform.position);
    }

    private void explode(RaycastHit hit)
    {
        GameObject particleEffet = (GameObject) Instantiate(bangParticleEffect, transform.position, transform.rotation);

        if (hit.transform.gameObject.tag == "Enemy" && hit.transform.gameObject != null)
        {
            hit.transform.gameObject.GetComponent<Enemy>().takeDamage(damage);
            Destroy(gameObject);
        } 

        Destroy(gameObject);
    }
}