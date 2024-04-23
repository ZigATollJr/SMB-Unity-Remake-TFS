using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D),typeof(SpriteRenderer))]
public class Player_Controller : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    [SerializeField] private float speed = 7.0f;
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private bool testMode = false;
    
    [SerializeField] private float jumpStartTime = 1f; 
    private float jumpTime = 0f;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask isGroundLayer;
    [SerializeField] private float groundCheckRadius = 0.02f;
    [SerializeField] private float jumpHeight = 4f;
    [SerializeField] private float timeToJumpApex = 0.55f;

    [SerializeField] private float tempFloat = 0f;

    [SerializeField] private bool isGrounded = false;
    [SerializeField] private bool isJumping = false;
    [SerializeField] private bool jump = false;
    [SerializeField] float xVel = 0f;
    [SerializeField] float xVelLimit = 5.72f;
    [SerializeField] float xVelInitialJump = 0f;
    [SerializeField] float yVel = 0f;
    [SerializeField] float yVelLimit = 0f;
    [SerializeField] sbyte xInput = 0;
    [SerializeField] sbyte xDir = 0;
    [SerializeField] sbyte flipDir = 0;
    [SerializeField] float jumpGrav = 0f;
    [SerializeField] float grav = 98.4375f;
    [SerializeField] bool isRunning;

    // Animator bools
         


    void Start()
    {
        if (groundCheckRadius > 0)
        {
            groundCheckRadius = 0.02f;
            if (testMode) Debug.Log("Ground check radius too low! Defaulted to 0.02f");
        }
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        if (speed <= 0)
        {

            speed = 7.0f;
            if (testMode) Debug.Log("Mario speed too low! Set to default of 7.0f");
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump")) jump = true;
        if (Input.GetButtonUp("Jump")) isJumping = false;
        if (Input.GetKey(KeyCode.LeftShift)) isRunning = true;
        else
        {
            xVelLimit = 5.72f;
            isRunning = false;
        }
        switch (Input.GetAxisRaw("Horizontal"))
        {
            case -1:
                xDir = -1;
                xInput = -1;
                break;
            case 1:
                xDir = 1;
                xInput = 1;
                break;
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, isGroundLayer);
        sbyte xVelDir = (sbyte)MathF.Sign(xVel);
        if (xVelDir == 0) xVelDir = xDir;
        Debug.Log(xVelDir + " guh " + MathF.Sign(xVel));
        if (isGrounded)
        {
            // MOVEMENT STUFF
            // Min Walk Vel
            yVel = 0;
            sr.flipX = (xDir == -1);
            flipDir = xDir;
            anim.SetBool("isSkidding", false);
            if (xInput != 0 && xInput == xVelDir)
            {
                
                
                
                if (xVel == 0f) {
                    Debug.Log("We started to walk");
                    xVel += 0.278f * xDir;
                }
                Debug.Log("And now we zooming");
                // Running Acceleration
                if (isRunning)
                {
                    xVel += (13.4f / 50f) * xVelDir;
                    xVelLimit = 9.61f;
                }
                    // Walking acceleration
                else
                {
                    Debug.Log("Should be walking");
                    xVel += (8.35f / 50f) * xVelDir;
                    xVelLimit = 5.72f;

                }
            }
            // Slowing down
            else
            {
                if (MathF.Abs(xVel) > 0f)
                {
                    Debug.Log("SKIDDING");
                    // Skidding / release decel
                    if (flipDir != MathF.Sign(xVel))
                    {
                        if (xDir != 0) xVel -= (22.85f / 50f) * xVelDir;
                        anim.SetBool("isSkidding", true);
                    }
                    // Release decel
                    else xVel -= (8.35f / 50f) * xVelDir;

                    // xVel -= (xVelDir * (flipDir != MathF.Sign(xVel) ? 22.85f : 8.35f) / 50f);

                    // Slowest skid (turnaround speed)
                    if (MathF.Abs(xVel) < 2.1f) xVel = 0f;
                }
                
            }
            anim.SetBool("isJumping", false);
            if (jump)
            {
                if (MathF.Abs(xVel) < 3.75f)
                {
                    yVel += 15f;
                    jumpGrav = 28.125f;
                    grav = 98.4375f;
                }
                else if (MathF.Abs(xVel) < 9.375f)
                {
                    yVel += 15f;
                    jumpGrav = 26.37f;
                    grav = 84.375f;
                }
                else
                {
                    yVel += 18f;
                    jumpGrav = 35.15625f;
                    grav = 126.54f;
                }
                xVelInitialJump = xVel;
                anim.SetBool("isSkidding", false);
                anim.SetBool("isJumping", true);
                jump = false;
                isJumping = true;
            }
            // ANIMATOR STUFF
            
            if (Mathf.Abs(xVel) > 6f) anim.speed = 2;
            else if (Mathf.Abs(xVel) > 4f) anim.speed = 1.4f;
            else anim.speed = 1;
        }
        // Else is NOT grounded (in the air)
        else
        {
            // MOVEMENT STUFF
            // Air acceleration (horizontal movement)
            if (xDir == flipDir)
            {
                if (MathF.Abs(xVel) < 5.86f) xVel += (8.35f / 50f) * xInput;
                else xVel += (12.52f / 50f) * xInput;
            }
            // Air Deceleration
            else
            {
                if (xVel >= 5.86f) xVel -= (13.4f / 50f) * xVelDir;
                else if (xVelInitialJump >= 6.8f) xVel += (12.52f / 50f) * xInput;
                else xVel += (8.349f/50f) * xInput;
            }
            Debug.Log("GRAVITY");
            // Gravity... is a harness.
            if (isJumping) {
                yVel -= jumpGrav / 50f;
                if (yVel < 0) isJumping = false;
            }
            else yVel -= grav / 50f;

            if (!isJumping) anim.speed = 0;
        }

        // Velocity caps, yknow how it is
        if (yVel < -16.875) yVel = -16;
        if (MathF.Abs(xVel) >= xVelLimit) xVel = xVelLimit * xVelDir;

        anim.SetFloat("xVel", Mathf.Abs(xVel));
        xInput = 0;
        rb.velocity = new Vector2 (xVel, yVel);
    }
}
