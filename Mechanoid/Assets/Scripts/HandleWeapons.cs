using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleWeapons : MonoBehaviour
{
    public AudioSource gunSfx;
    public GameObject mainGuns;
    public GameObject bulletPrefab;
    public GameObject shellPrefab;
    public ParticleSystem sparks_1;
    public ParticleSystem sparks_2;
    public Transform bulletSpawnPoint1;
    public Transform bulletSpawnPoint2;
    public Transform shellSpawnPoint1;
    public Transform shellSpawnPoint2;

    private Animation animationController;
    private float timeToFire;
    private float fireRate;
    private float bulletSpeed;

    void Start()
    {
        animationController = mainGuns.GetComponent<Animation>();
        timeToFire = Time.time;
        fireRate = 0.04f;
        bulletSpeed = 50f;
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > timeToFire)
        {
            timeToFire = Time.time + fireRate;
            mainGuns.GetComponent<Animation>().Play("Fire_Cycle");

            if (!sparks_1.isPlaying)
            {
                sparks_1.Play();
            }

            if (!sparks_2.isPlaying)
            {
                sparks_2.Play();
            }

            fire();
        }
        else if(!Input.GetButton("Fire1"))
        {
            gunSfx.Stop();
        }
    }

    private void fire()
    {
        if(!gunSfx.isPlaying)
        {
            gunSfx.Play();
        }

        GameObject bullet1 = Instantiate(bulletPrefab, bulletSpawnPoint1.position, bulletPrefab.transform.rotation);
        GameObject bullet2 = Instantiate(bulletPrefab, bulletSpawnPoint2.position, bulletPrefab.transform.rotation);
        GameObject shell1 = Instantiate(shellPrefab, shellSpawnPoint1.position, shellPrefab.transform.rotation);
        GameObject shell2 = Instantiate(shellPrefab, shellSpawnPoint2.position, shellPrefab.transform.rotation);

        bullet1.GetComponent<Rigidbody>().AddForce(bulletSpawnPoint1.forward * bulletSpeed, ForceMode.Impulse);
        bullet2.GetComponent<Rigidbody>().AddForce(bulletSpawnPoint2.forward * bulletSpeed, ForceMode.Impulse);
    }

}
