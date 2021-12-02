using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /****** https://www.youtube.com/watch?v=F5a4Xo6ijLE ******/

    Rigidbody rb;
    Animator anim;

    /*
     * Mouse clamping to circle (not working)
    [SerializeField]
    Transform myPlayer;
    [SerializeField]
    Transform crosshairObject;

    [SerializeField]
    float corsshairRadius = 5;
    */

    Transform cam;
    Vector3 lookPos;
    Vector3 camForward;
    Vector3 move;
    Vector3 moveInput;

    float forwardAmount;
    float turnAmount;

    /* Player speed */
    public float speed = 5;

    [Range(1f, 4f)]
    public float runSpeed = 2f;
    // public float punchCoolDown = 1f;

    public float mouseLerpingSpeed = 10f;

    public float yVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        cam = Camera.main.transform;
    }

    // Mouse look position
    // TODO: Mouse look smoothing (fixed)
    private void Update()
    {
        // Mouse clamping to circle (not working)
        /*
        // Get mouse position into world space:
        //Transform player;
        Vector3 wPos = Input.mousePosition;
        wPos.z = myPlayer.position.z - Camera.main.transform.position.z;
        wPos = Camera.main.ScreenToWorldPoint(wPos);

        // Get direction from player to worldMousePosition
        Vector3 direction = wPos - myPlayer.position;

        // Clamp magnitude to certain radius from player 
        direction = Vector3.ClampMagnitude(direction, corsshairRadius);

        // Set object position
        crosshairObject.position = myPlayer.position + direction;
        

        // #############################################
        // Mouse clamping to circle (not working) ver2

        Vector3 mousePos = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);

        mousePos.x = Mathf.Clamp(mousePos.x, 10, 10);
        mousePos.y = Mathf.Clamp(mousePos.y, 10, 10);

        Vector3 mousePosClamp = new Vector3(mousePos.x, mousePos.y, 0);

        // #############################################
        */

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.green); /* not necessary line */

        if (Physics.Raycast(ray, out hit, 1000))
        {
            lookPos = hit.point;
        }

        Vector3 lookDir = lookPos - transform.position;
        lookDir.y = 0;

        //transform.LookAt(transform.position + lookDir, Vector3.up);

        /* Smoother mouse feel */
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookDir), mouseLerpingSpeed * Time.deltaTime);

        yVelocity = rb.velocity.y; // for debugging falling errors
    }

    // Player Movement
    void FixedUpdate()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        /* ALWAYS FORWARD ANIM FIX */
        if (cam != null)
        {
            camForward = Vector3.Scale(cam.up, new Vector3(1, 0, 1)).normalized;
            move = ver * camForward + hor * cam.right;
        }
        else
        {
            move = ver * Vector3.forward + hor * Vector3.right;
        }

        if (move.magnitude > 1)
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
            if (rb.velocity.y > -4f)
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
        if (rb.velocity.y > -4f) /* falling is happening if vel y is more than -4f */
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
        if (move.magnitude > 1)
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