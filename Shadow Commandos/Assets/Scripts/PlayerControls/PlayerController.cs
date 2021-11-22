using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Input
    private InputActions playerActionAsset;
    private InputAction move;

    // Movement
    private Rigidbody rb;
    [SerializeField]
    private float movementForce = 1f;
    /*
    [SerializeField]
    private float jumpForce = 5f;
    */
    [SerializeField]
    private float maxSpeed = 5f;
    private Vector3 forceDirection = Vector3.zero;

    // Camera
    [SerializeField]
    private Camera playerCamera;

}
