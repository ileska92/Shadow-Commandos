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
    float fallAmount;

    /* Player speed */
    public float speed = 10;

    [Range(1f,4f)]
    public float runSpeed = 1.5f;
    // public float punchCoolDown = 1f;

    // bool isGrounded = true;

    public float lerpingSpeed = 1f;

    public float yVelocity;

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

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.green); /* not necessary line */

        if (Physics.Raycast(ray, out hit, 1000))
        {
            lookPos = hit.point;
        }

        Vector3 lookDir = lookPos - transform.position;
        lookDir.y = 0;

        transform.LookAt(transform.position + lookDir, Vector3.up);
        //transform.LookAt(Vector3.Lerp(transform.position, lookDir, lerpingSpeed * Time.deltaTime), Vector3.up);

        yVelocity = rb.velocity.y;
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

        /*  MOVE PLAYER WITH RIGIDBODY */
        Vector3 gravity = new Vector3(0.0f, rb.velocity.y, 0.0f);
        Vector3 playerMove = new Vector3(hor, 0, ver); // y = 0 means no gravity
        rb.velocity = playerMove * speed + gravity; // new vector 3 for re-assigning gravity

        /* FIRE WITH MOUSE */
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Shoot");
        }

        /* RUN FASTER */
        /* TODO: runCoolDown timer */
        if (Input.GetKey(KeyCode.LeftShift))
        {
            /* Can't run faster if falling */
            if (rb.velocity.y > -2.5f) /* falling is happening if vel y is more than -2.5f */
            {
                rb.velocity = playerMove * speed * runSpeed + gravity;
            }
            else
            {
                rb.velocity = playerMove * speed + gravity;
            }
        }

        /*
        float coolDownTimer = Time.time + punchCoolDown;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if(coolDownTimer <= Time.time)
            {
                rb.velocity = playerMove * speed * 2f;
                anim.SetTrigger("Punch");
            }
        }
        */

        /* GROUND CHECK AND FALLING */
        if (rb.velocity.y > -2.5f) /* falling is happening if vel y is more than -2.5f */
        {
            Debug.Log("Ground!");
            anim.SetBool("Fall", false);
        }
        else
        {
            Debug.Log("Not Ground!");
            anim.SetBool("Fall", true);
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