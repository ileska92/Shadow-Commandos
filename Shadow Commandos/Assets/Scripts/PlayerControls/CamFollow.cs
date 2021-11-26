using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform player;
    private float camOffset;

    void Start()
    {
        camOffset = gameObject.transform.position.z - player.position.z;
    }

    void Update()
    {
        Vector3 camPos = new Vector3(player.position.x, gameObject.transform.position.y, player.position.z + camOffset);
        gameObject.transform.position = camPos;
    }
}
