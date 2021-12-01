using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _PlayerShoot : MonoBehaviour
{
    // https://www.youtube.com/watch?v=AGd16aspnPA&t

    public int gunDamage = 1;
    public float fireRate = .05f;
    public float weaponRange = 25f;
    public float weaponScreenRange = 5f;
    public float hitForce = 100f;
    public Transform gunEnd;

    ParticleSystem muzzleFlash01;
    ParticleSystem muzzleFlash02;

    [SerializeField]
    private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    private AudioSource gunAudio;
    private LineRenderer laserLine;
    private float nextFire;

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //muzzleFlash01 = GameObject.Find("MuzzleFlash01").GetComponent<ParticleSystem>();
        //muzzleFlash02 = GameObject.Find("MuzzleFlash02").GetComponent<ParticleSystem>();

        laserLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        fpsCam = GameObject.Find("RaycastCamera").GetComponent<Camera>();
    }

    void Update()
    {
        // https://www.youtube.com/watch?v=AGd16aspnPA
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            // print("FIRE");

            nextFire = Time.time + fireRate;

            StartCoroutine(ShotEffect());

            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            RaycastHit hit;

            laserLine.SetPosition(0, gunEnd.position);

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                laserLine.SetPosition(1, hit.point);

                //EnemyHealth health = hit.collider.GetComponent<EnemyHealth>();
                /*
                if (health != null)
                {
                    health.Damage(gunDamage);
                }
                */
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponScreenRange));
            }
        }
    }

    private IEnumerator ShotEffect()
    {
        //gunAudio.Play();
        //muzzleFlash01.Play();
        //muzzleFlash02.Play();
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }
}
