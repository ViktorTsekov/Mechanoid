using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public GameObject bangParticleEffect;

    private float speed;
    private float rotationSpeed;
    private float startHoming;

    private Vector3 direction;
    private Vector3 currentPos;
    private Vector3 prevPos;

    private GameObject target;

    void Start()
    {
        startHoming = Time.time + 1f;
        speed = 40f;
        rotationSpeed = 5f;
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
            if (hit.transform.gameObject.tag != "Projectile" && hit.transform.gameObject.tag != "Player")
            {
                explode();
            }
        }

        if (Time.time > startHoming)
        {
            findTarget();
            homing();
        }
    }

    private void findTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float minDist = Mathf.Infinity;

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

    private void homing()
    {
        direction = target.transform.position - transform.position;
        direction = direction.normalized;
        var rot = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotationSpeed * Time.deltaTime);
        float dist = Vector3.Distance(target.transform.position, transform.position);

        if(dist < 6f)
        {
            explode();
        }
    }

    private void explode()
    {
        GameObject particleEffet = (GameObject)Instantiate(bangParticleEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}