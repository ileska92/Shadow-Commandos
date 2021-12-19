using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEnemy : MonoBehaviour
{
    public GameObject enemy1, enemy2, enemy3, enemy4;
    public AudioClip zombieSound;
    public AudioSource myAudio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(zombieSound, this.gameObject.transform.position);
            enemy1.SetActive(true);
            enemy2.SetActive(true);
            enemy3.SetActive(true);
            enemy4.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
