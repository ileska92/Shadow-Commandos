using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /****** https://www.youtube.com/watch?v=F5a4Xo6ijLE ******/

    Rigidbody rb;
    Animator anim;

    public float speed = 10;

    Vector3 lookPos;

    Transform cam;
    Vector3 camForward;
    Vector3 move;
    Vector3 moveInput;

    float forwardAmount;
    float turnAmount;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        cam = Camera.main.transform;
    }

    // Mouse look position
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
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

        /* FIX FORWARD ANIM */
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

        /* FIRE WITH MOUSE */
        if(Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Shoot");
        }

        /*  MOVE PLAYER WITH RB */
        Vector3 playerMove = new Vector3(hor, 0, ver);
        //rb.AddForce(playerMove * speed / Time.deltaTime);
        //rb.AddForce(playerMove * speed / Time.deltaTime, ForceMode.Acceleration);
        rb.velocity = playerMove * speed;
    }

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

    private void UpdateAnimator()
    {
        anim.SetFloat("Forward", forwardAmount, 0.01f, Time.deltaTime);
        anim.SetFloat("Turn", turnAmount, 0.01f, Time.deltaTime);
    }

    private void ConvertMoveInput()
    {
        Vector3 localMove = transform.InverseTransformDirection(moveInput);
        turnAmount = localMove.x;

        forwardAmount = localMove.z;
    }
}
