using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animator myDoorLeft = null;
    public Animator myDoorRight = null;
    public bool openTrigger = false;
    public bool closeTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
           if(openTrigger)
            {
                myDoorLeft.Play("DoorLeftOpen", 0, 0.0f);
                myDoorRight.Play("DoorRightOpen", 0, 0.0f);
                gameObject.SetActive(false);
            }

           if (closeTrigger)
            {
                myDoorLeft.Play("DoorLeftClose", 0, 0.0f);
                myDoorRight.Play("DoorRightClose", 0, 0.0f);
                gameObject.SetActive(false);
            }
        }
    }
}
