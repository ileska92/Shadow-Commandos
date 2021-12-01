using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Andtech.ProTracer;

public class PlayerShoot : MonoBehaviour
{
    public Transform gunTransform;
    public float maxRaycastDistance = 1000.0f;

    public Bullet bulletPrefab;
    public SmokeTrail smokeTrailPrefab;
    public GameObject impactPrefab;
    public ParticleSystem muzzleFlash;

    private void Start()
    {
        muzzleFlash = GameObject.Find("MuzzleFlash01 URP").GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }

    void Fire()
    {
        Ray ray = new Ray(gunTransform.position, gunTransform.forward);
        bool hasHit = Physics.Raycast(ray, out RaycastHit hitInfo, maxRaycastDistance);

        Vector3 start = gunTransform.position;
        Vector3 end;
        if (hasHit)
        {
            end = hitInfo.point;
        }
        else
        {
            end = ray.GetPoint(maxRaycastDistance);
        }
        DoTracerEffect(start, end, hitInfo.normal, doImpactEffect: hasHit);
    }

    void DoTracerEffect(Vector3 from, Vector3 to, Vector3 normal, bool doImpactEffect)
    {
        Bullet bullet = Instantiate(bulletPrefab);
        SmokeTrail smokeTrail = Instantiate(smokeTrailPrefab);
        muzzleFlash.Play();

        bullet.Completed += DestroyTracerObject;
        smokeTrail.Completed += DestroyTracerObject;
        //Destroy(muzzleFlash, 0.25f);

        if (doImpactEffect)
        {
            bullet.Arrived += DoImpactEffect;
        }

        bullet.DrawLine(from, to, speed: 100.0f);
        smokeTrail.DrawLine(from, to, speed: 100.0f);

        void DoImpactEffect(object sender, System.EventArgs e)
        {
            GameObject impact = Instantiate(impactPrefab);

            impact.transform.position = to;
            impact.transform.rotation = Quaternion.LookRotation(normal);

            Destroy(impact, 0.5f);
        }
    }

    private void DestroyTracerObject(object sender, System.EventArgs e)
    {
        Destroy((sender as TracerObject).gameObject);
    }
}
