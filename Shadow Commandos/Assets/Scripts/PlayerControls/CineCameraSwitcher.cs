using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CineCameraSwitcher : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera playerVCam, blockedVCam;
    private float vPlayerCam;

    private void Start()
    {
        playerVCam = GameObject.Find("Cine Player Camera").GetComponent<CinemachineVirtualCamera>();
        blockedVCam = GameObject.Find("Cine Blocked Camera").GetComponent<CinemachineVirtualCamera>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerVCam.Priority = 0;
            blockedVCam.Priority = 1;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        playerVCam.Priority = 0;
        if (other.CompareTag("Player"))
        {
            blockedVCam.Priority = 0;
            playerVCam.Priority = 1;
        }
    }
}
