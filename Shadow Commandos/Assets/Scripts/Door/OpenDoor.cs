using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SickscoreGames.HUDNavigationSystem;

public class OpenDoor : MonoBehaviour
{
    public Animator myDoorLeft = null;
    public Animator myDoorRight = null;
    public bool openTrigger = false;
    public bool closeTrigger = false;
    public GameObject doorText;
    public AudioSource myAudioOpen;
    public AudioSource myAudioClose;
    public AudioClip doorOpenSound;
    public AudioClip doorCloseSound;
    public GameObject door, door2;

    private void Start()
    {
        doorText = GameObject.Find("DoorOpenText");
        door = GameObject.Find("Door_L");
        door2 = GameObject.Find("Door_L1");
    }

    private void Update()
    {
        if(doorText.GetComponent<TextMeshProUGUI>().enabled)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                door.GetComponent<HUDNavigationElement>().enabled = false;
                door2.GetComponent<HUDNavigationElement>().enabled = true;
                AudioSource.PlayClipAtPoint(doorOpenSound, this.gameObject.transform.position);
                myDoorLeft.Play("DoorLeftOpen", 0, 0.0f);
                myDoorRight.Play("DoorRightOpen", 0, 0.0f);
                gameObject.SetActive(false);
                doorText.GetComponent<TextMeshProUGUI>().enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
           if(openTrigger)
            {
                doorText.GetComponent<TextMeshProUGUI>().enabled = true;
            }

           if (closeTrigger)
            {
                AudioSource.PlayClipAtPoint(doorCloseSound, this.gameObject.transform.position);
                myDoorLeft.Play("DoorLeftClose", 0, 0.0f);
                myDoorRight.Play("DoorRightClose", 0, 0.0f);
                gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorText.GetComponent<TextMeshProUGUI>().enabled = false;
        }
    }
}
