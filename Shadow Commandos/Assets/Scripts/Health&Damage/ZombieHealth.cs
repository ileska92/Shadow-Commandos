using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float Damage)
    {
        currentHealth -= Damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject, 0.5f);
        }
    }
}
