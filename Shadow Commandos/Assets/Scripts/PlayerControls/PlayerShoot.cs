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

    GameObject enemy;

    //AmmoSystem
    public int maxAmmo = 150;
    public int magazineCurrentAmmo;
    public int magazineMaxAmmo = 30;
    public int currentAmmo;
    public float reloadTime = 1;
    private bool isReloading = false;

    private void Start()
    {
        muzzleFlash = GameObject.Find("MuzzleFlash01 URP").GetComponent<ParticleSystem>();
        magazineCurrentAmmo = magazineMaxAmmo;
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if(isReloading)
        {
            return;
        }

        if(magazineCurrentAmmo <= 0 && currentAmmo > 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButtonDown("Fire1") && magazineCurrentAmmo > 0)
        {
            Fire();
        }
    }

    public void Fire()
    {
        Ray ray = new Ray(gunTransform.position, gunTransform.forward);
        bool hasHit = Physics.Raycast(ray, out RaycastHit hitInfo, maxRaycastDistance);
        magazineCurrentAmmo--;



        Vector3 start = gunTransform.position;
        Vector3 end;
        if (hasHit)
        {
            end = hitInfo.point;
            enemy = null;
            //print("hit"); //Debug
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                enemy = hitInfo.collider.gameObject;
               //print("collider"); //Debug
            }
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
            //print("impact"); //Debug
            bullet.Arrived += DoImpactEffect;
            if (enemy)
            {
                bullet.Arrived += EnemyDamage;
                //print("damage"); //Debug
            }
            
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

        void EnemyDamage(object sender, System.EventArgs e)
        {
            //hitInfo.collider.
            if(enemy != null)
            {
                enemy.gameObject.GetComponent<ZombieHealth>().TakeDamage(50);
                //print("zombiehealth"); //Debug
            }
        }
    }

    private void DestroyTracerObject(object sender, System.EventArgs e)
    {
        Destroy((sender as TracerObject).gameObject);
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("reloading..");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo -= 30;
        magazineCurrentAmmo = magazineMaxAmmo;
        isReloading = false;
    }
}
