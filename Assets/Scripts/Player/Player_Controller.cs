using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D),typeof(SpriteRenderer))]
public class Player_Controller : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    [SerializeField] private float speed = 7.0f;
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private bool testMode = false;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private bool isJumping = false;
    [SerializeField] private float jumpStartTime = 1f; 
    private float jumpTime = 0f;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask isGroundLayer;
    [SerializeField] private float groundCheckRadius = 0.02f;
    [SerializeField] private float jumpHeight = 4f;
    [SerializeField] private float timeToJumpApex = 0.55f;

    [SerializeField] private float tempFloat = 0f;
    float xVelocity = 0f;
    float yVelocity = 0f;
    // Start is called before the first frame update
    void Start()
    {
        if (groundCheckRadius > 0)
        {
            groundCheckRadius = 0.02f;
            if (testMode) Debug.Log("Ground check radius too low! Defaulted to 0.02f");
        }
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        if (speed <= 0)
        {

            speed = 7.0f;
            if (testMode) Debug.Log("Mario speed too low! Set to default of 7.0f");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        float xInput = Input.GetAxisRaw("Horizontal");
        xVelocity = (xInput * speed);


        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, isGroundLayer);


        if (xInput != 0) sr.flipX = (xInput < 0);

        float gravity = -(2f * jumpHeight) / (timeToJumpApex * timeToJumpApex);
        // JUMP STUFF
        if (Input.GetButton("Jump") && isJumping)
        {
            
            yVelocity -= 26.2f * Time.deltaTime;
            
            if (yVelocity < 0f)
            {
                isJumping = false;
            }
        }
        if (Input.GetButton("Jump") && isGrounded)
        {
            isJumping = true;
            isGrounded = false;
            yVelocity = 14.54f;
        }
        if (Input.GetButtonUp("Jump")) isJumping = false;

        // GRAVITY STUFF
        if (isJumping == false)
        {
            Debug.Log("gravity gaming");
            yVelocity -= 100f * Time.deltaTime;
            if (yVelocity < -15f)
            {
                yVelocity = -15f;
            }
        }
        if (isGrounded)
        {
            yVelocity = 0f;
        }
        Debug.Log(xVelocity + "guh" + yVelocity);
        rb.velocity = new Vector2(xVelocity, yVelocity);
    }
}
