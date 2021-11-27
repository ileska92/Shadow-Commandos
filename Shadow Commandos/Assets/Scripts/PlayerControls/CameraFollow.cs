using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    [Range(0.01f, 1f)]
    public float smoothSpeed = 0.125f;
    private Vector3 offset;
    private Vector3 velocity = Vector3.zero;


    private void Awake()
    {
       offset = Camera.main.transform.position - player.position;
    }

    void FixedUpdate()
    {
        Vector3 desiredPos = player.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPos, ref velocity, smoothSpeed);
    }
}
