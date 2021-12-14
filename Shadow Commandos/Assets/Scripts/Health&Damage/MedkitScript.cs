using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MedkitScript : MonoBehaviour
{
    public GameObject player;
    public AudioSource myAudio;
    public AudioClip medKitSound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(player.GetComponent<PlayerHealth>().currentHealth < 100)
            {
                AudioSource.PlayClipAtPoint(medKitSound, this.gameObject.transform.position);
                player.GetComponent<PlayerHealth>().Healing(50);
                Destroy(gameObject);
            }
        }
    }
}
