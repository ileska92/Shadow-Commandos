using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour
{
    public PlayerShoot playerAmmo;

    // Start is called before the first frame update
    void Start()
    {
        playerAmmo = GameObject.FindGameObjectWithTag("Gun").GetComponent<PlayerShoot>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(playerAmmo.currentAmmo < 150)
            {
                playerAmmo.currentAmmo += 30;
                playerAmmo.currentAmmo = Mathf.Clamp(playerAmmo.currentAmmo, 0, 150);
                Destroy(gameObject);
            }
        }
    }

}
