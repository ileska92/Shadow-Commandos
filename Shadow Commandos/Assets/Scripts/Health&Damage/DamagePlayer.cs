using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamagePlayer : MonoBehaviour
{
    public GameObject player;
    //public AudioSource myAudio; //Uncomment these if zombie attack sound is needed
    //public AudioClip[] zombieAttackSounds; //Uncomment these if zombie attack sound is needed
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //myAudio.clip = zombieAttackSounds[Random.Range(0, zombieAttackSounds.Length)]; //Uncomment these if zombie attack sound is needed
    }

    public void Damage()
    {
        player.GetComponent<PlayerHealth>().TakeDamage(20);
        //myAudio.Play(); //Uncomment these if zombie attack sound is needed
    }
}
