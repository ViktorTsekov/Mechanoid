using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TerrainNavigation : MonoBehaviour
{
    public Transform allZones;
    public Transform zoneA;
    public Transform zoneB;
    public Transform zoneC;
    public Transform zoneD;
    public Transform zoneE;
    public Transform zoneF;
    public GameObject defaultZone;
    public GameObject mainMesh;

    private Transform target;
    private NavMeshAgent navMeshAgent;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        Cursor.lockState = CursorLockMode.Locked;
        initializeZones();
        setActiveZone(defaultZone);
    }

    void Update()
    {
        navMeshAgent.destination = target.position;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(wait(0.2f));
        }

        if (transform.position.x != target.position.x || transform.position.z != target.position.z)
        {
            mainMesh.GetComponent<Animation>().Play("Walk");
        }
    }

    private void initializeZones()
    {
        Color red = Color.red;

        red.a = 0.4f;

        foreach (Transform child in allZones)
        {
            child.gameObject.GetComponent<Image>().color = red;
        }
    }

    private IEnumerator wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void setActiveZone(GameObject zone)
    {
        Color green = Color.green;
        Color red = Color.red;

        green.a = 0.4f;
        red.a = 0.4f;

        foreach (Transform child in allZones)
        {
            child.gameObject.GetComponent<Image>().color = red;
        }

        zone.GetComponent<Image>().color = green;

        switch (zone.name)
        {
            case "A": target = zoneA; break;
            case "B": target = zoneB; break;
            case "C": target = zoneC; break;
            case "D": target = zoneD; break;
            case "E": target = zoneE; break;
            case "F": target = zoneF; break;
        }
    }
}
