using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoBar : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    public PlayerShoot playerAmmo;
    // Start is called before the first frame update
    void Start()
    {
        ammoText.GetComponent<TextMeshProUGUI>();
        playerAmmo = GameObject.FindGameObjectWithTag("Gun").GetComponent<PlayerShoot>();
    }

    // Update is called once per frame
    void Update()
    {
        ammoText.text = playerAmmo.magazineCurrentAmmo.ToString() + " / " + playerAmmo.currentAmmo.ToString() + " AMMO";
    }
}
