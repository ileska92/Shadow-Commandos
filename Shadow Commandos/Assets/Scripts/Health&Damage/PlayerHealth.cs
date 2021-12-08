using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;

    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        /*  if(Input.GetKeyDown(KeyCode.F)) //For testing
       {
           TakeDamage(10);
       }
       if (Input.GetKeyDown(KeyCode.E))  //For testing
       {
           Healing(20);
       }*/
    }
    public void TakeDamage(float Damage)
    {
        currentHealth -= Damage;
        healthBar.SetHealth(currentHealth);
      
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Healing(float healPoints)
    {
        currentHealth += healPoints;
        currentHealth = Mathf.Clamp(currentHealth, 0, 100);
        healthBar.SetHealth(currentHealth);
    }
}
