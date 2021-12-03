using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    public Image healthBar;
    public float maxHealth = 100;
    public float currentHealth;
    public TextMeshProUGUI healthText;

    public void Start()
    {
        GameObject imageObject = GameObject.FindGameObjectWithTag("HealthImage");
        healthBar = imageObject.GetComponent<Image>();
        currentHealth = maxHealth;
        healthText.GetComponent<TextMeshProUGUI>();
    }

    public void Update()
    {
       
        healthText.text = currentHealth.ToString();


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
        healthBar.fillAmount = currentHealth / 100;
        if(currentHealth <= 0)
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
        }
    }

    public void Healing(float healPoints)
    {
        currentHealth += healPoints;
        currentHealth = Mathf.Clamp(currentHealth, 0, 100);
        healthBar.fillAmount = currentHealth / 100;
    }
}
