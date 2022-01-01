using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TerrainNavigation : MonoBehaviour
{
    public Transform defaultZone;
    public Transform zoneA;
    public Transform zoneB;
    public Transform zoneC;
    public Transform zoneD;
    public Transform zoneE;
    public Transform zoneF;
    public GameObject mainMesh;

    private Transform target;
    private NavMeshAgent navMeshAgent;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        Cursor.lockState = CursorLockMode.Locked;
        target = defaultZone;
    }

    void Update()
    {
        navMeshAgent.destination = target.position;

        if (transform.position.x != target.position.x || transform.position.z != target.position.z)
        {
            mainMesh.GetComponent<Animation>().Play("Walk");
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            target = zoneA;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            target = zoneB;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            target = zoneC;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            target = zoneD;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            target = zoneF;
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            target = zoneE;
        }
    }
}
