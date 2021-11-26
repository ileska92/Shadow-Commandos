using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /****** https://www.youtube.com/watch?v=F5a4Xo6ijLE ******/

    Rigidbody rb;
    Animator anim;

    Transform cam;
    Vector3 lookPos;
    Vector3 camForward;
    Vector3 move;
    Vector3 moveInput;

    float forwardAmount;
    float turnAmount;

    /* Player speed */
    public float speed = 10;

    public float force;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        cam = Camera.main.transform;
    }

    // Mouse look position
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition - new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);

        if (Physics.Raycast(ray, out hit, 1000))
        {
            lookPos = hit.point;
        }

        Vector3 lookDir = lookPos - transform.position;
        lookDir.y = 0;

        transform.LookAt(transform.position + lookDir, Vector3.up);
    }

    // Player Movement
    void FixedUpdate()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        /* ALWAYS FORWARD ANIM FIX */
        if(cam != null)
        {
            camForward = Vector3.Scale(cam.up, new Vector3(1, 0, 1)).normalized;
            move = ver * camForward + hor * cam.right;
        }
        else
        {
            move = ver * Vector3.forward + hor * Vector3.right;
        }

        if(move.magnitude > 1)
        {
            move.Normalize();
        }

        Move(move);

        /*  MOVE PLAYER WITH RB */
        Vector3 playerMove = new Vector3(hor, 0, ver);
        rb.velocity = playerMove * speed;

        /* FIRE WITH MOUSE */
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Shoot");
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = playerMove * speed * 2f;
        }
    }
    
    /* ALWAYS FORWARD ANIM FIX */
    private void Move(Vector3 move)
    {
        if(move.magnitude > 1)
        {
            move.Normalize();
        }

        this.moveInput = move;

        ConvertMoveInput();
        UpdateAnimator();
    }

    private void ConvertMoveInput()
    {
        Vector3 localMove = transform.InverseTransformDirection(moveInput);
        turnAmount = localMove.x;
        forwardAmount = localMove.z;
    }

    private void UpdateAnimator()
    {
        anim.SetFloat("Forward", forwardAmount, 0.1f, Time.deltaTime);
        anim.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);
    }
}