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
    public float enemyDamageAmmount = 34f;

    //Audio
    public AudioSource myAudioShoot;
    public AudioSource myAudioReload;
    public AudioClip shootSound;
    public AudioClip reloadSound;

    //AmmoSystem
    public int maxAmmo = 75;
    public int magazineCurrentAmmo;
    public int magazineMaxAmmo = 15;
    public int currentAmmo;
    public float reloadTime = 2;
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

        if(Input.GetKeyDown(KeyCode.R) && magazineCurrentAmmo < magazineMaxAmmo && currentAmmo > 0)
        {
            StartCoroutine(ManualReload());
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
        myAudioShoot.PlayOneShot(shootSound);


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
                enemy.gameObject.GetComponent<ZombieHealth>().TakeDamage(enemyDamageAmmount);
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
        myAudioReload.PlayOneShot(reloadSound);

        yield return new WaitForSeconds(reloadTime);

        if (currentAmmo < magazineMaxAmmo)
        {
            magazineCurrentAmmo = currentAmmo;
            currentAmmo -= currentAmmo;
        }

        else
        {
            currentAmmo -= magazineMaxAmmo;
            magazineCurrentAmmo = magazineMaxAmmo;
        }
        isReloading = false;
    }

    IEnumerator ManualReload()
    {
        isReloading = true;
        Debug.Log("Manually reloading..");
        myAudioReload.PlayOneShot(reloadSound);

        yield return new WaitForSeconds(reloadTime);

        if (currentAmmo < magazineMaxAmmo)
        {
            currentAmmo -= magazineMaxAmmo - magazineCurrentAmmo;
            magazineCurrentAmmo += magazineMaxAmmo - magazineCurrentAmmo;
            if(currentAmmo < (magazineMaxAmmo - magazineCurrentAmmo))
            {
                magazineCurrentAmmo += currentAmmo;
                currentAmmo -= currentAmmo;
            }
        }
        else
        {
            currentAmmo -= magazineMaxAmmo - magazineCurrentAmmo;
            magazineCurrentAmmo = magazineMaxAmmo;
        }
        isReloading = false;
    }
}
