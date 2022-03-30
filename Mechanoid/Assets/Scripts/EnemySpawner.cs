using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    private float resurrectAfter;
    private int singleton;

    void Start()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);

        enemy.transform.parent = transform;
        singleton = 0;
    }

    void Update()
    {
        if(transform.childCount == 0 && singleton == 0)
        {
            resurrectAfter = Time.time + 7f;
            singleton = 1;
        } 
        else if(Time.time > resurrectAfter)
        {
            if (transform.childCount == 0)
            {
                GameObject enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);

                enemy.transform.parent = transform;
                singleton = 0;
            }
        }
    }
}
