using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public AudioSource myAudio;
    public AudioClip zombieSound;

    private void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(zombieSound, this.gameObject.transform.position);
            gameObject.SetActive(false);
        }
    }
}
