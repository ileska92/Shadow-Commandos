using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100;

    private void Start()
    {
        GameObject imageObject = GameObject.FindGameObjectWithTag("HealthImage");
        healthBar = imageObject.GetComponent<Image>();
    }

    public void Update()
    {
        if(healthAmount <= 0)
        {
            Destroy(gameObject);
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            TakeDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Healing(20);
        }
    }

    public void TakeDamage(float Damage)
    {
        healthAmount -= Damage;
        healthBar.fillAmount = healthAmount / 100;
    }

    public void Healing(float healPoints)
    {
        healthAmount += healPoints;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);
        healthBar.fillAmount = healthAmount / 100;
    }
}
