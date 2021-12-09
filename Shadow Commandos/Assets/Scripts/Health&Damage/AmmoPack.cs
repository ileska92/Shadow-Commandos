using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour
{
    public GameObject gun;
    public PlayerShoot playerAmmo;

    // Start is called before the first frame update
    void Start()
    {
        gun = GameObject.FindGameObjectWithTag("Gun");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(gun.GetComponent<PlayerShoot>().currentAmmo < 150)
            {
                playerAmmo.currentAmmo += 30;
                playerAmmo.currentAmmo = Mathf.Clamp(playerAmmo.currentAmmo, 0, 150);
            }
        }
    }

}
