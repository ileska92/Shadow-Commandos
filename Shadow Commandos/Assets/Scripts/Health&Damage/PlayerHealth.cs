using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;

    public HealthBar healthBar;
    public GameObject lowHpPostProcess;

    //Audio
    public AudioSource myAudioHeartBeat;
    public AudioClip hearthBeatSound;
    public AudioSource myAudioDamage;
    public AudioClip damageSound;

    // Start is called before the first frame update
    void Start()
    {
        lowHpPostProcess = GameObject.Find("LowHpPostProcess");
        healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        lowHpPostProcess.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        if (currentHealth > 30)
        {
            myAudioHeartBeat.Stop();
        }

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
        myAudioDamage.PlayOneShot(damageSound);

        if (currentHealth < 30)
        {
            lowHpPostProcess.SetActive(true);
            myAudioHeartBeat.Play();
        }


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
