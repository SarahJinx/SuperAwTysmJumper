using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public enum PlayerState { IDLE, RUNNING, JUMPING }

public class PlayerController : MonoBehaviour
{
    public float playerHSpeed = 10, jumpForce = 10f, jumpTime = 0.35f;
    public float accelSpeed = 15, decelSpeed = 6, gravForce = 15f, dir = 0;
    public float currTime = 0, currHSpeed = 0, currVSpeed = 0, jumpVloume;
    [SerializeField]private bool isJumping = false;
    public float jumpTimeCounter, groundCheckRadius = 0.6f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    private Rigidbody2D rb;

    [SerializeField] private PlayerState currState;
    public AudioClip jumpingClip;

    [SerializeField] private float positionX;
    [SerializeField] private float positionY;
    [SerializeField] private float currPositionX;
    [SerializeField] private float currPositionY;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        positionX = transform.position.x;
        positionY = transform.position.y;
    }

    void Update()
    {
        HorizontalMovement();
        Jump();
        currPositionX = transform.position.x;
        currPositionY = transform.position.y;

        if (currHSpeed > 0) { dir = 1; }
        else if (currHSpeed < 0) { dir = -1; }
        else { dir = 0; }
    }

    private void FixedUpdate()
    {
        // Set the Rigidbody2D's velocity based on current speed and preserve vertical velocity
        rb.velocity = new Vector2(currHSpeed, rb.velocity.y);
    }

    void HorizontalMovement()
    {
        bool isHAccelerating = false;

        if (currHSpeed != 0)
        {
            isHAccelerating = true;
            currState = PlayerState.RUNNING;
        }
        else
        {
            isHAccelerating = false;
            currState = PlayerState.IDLE;
        }

        float x = Input.GetAxisRaw("Horizontal");

        if (x != 0) // If there is input
        {
            currHSpeed += x * accelSpeed * Time.deltaTime;   // Increase or decrease the current speed based on input 
            currHSpeed = Mathf.Clamp(currHSpeed, -playerHSpeed, playerHSpeed);  // Clamp the speed to ensure it doesn't exceed the maximum 

            if (!isHAccelerating)    // Set the direction of movement if the player wasn't accelerating before
            {
                dir = x;
            }
        }

        else // If there is no input
        {
            if (currHSpeed > 0) // Decelerate if moving right
            {
                currHSpeed -= decelSpeed * Time.deltaTime;
                currHSpeed = Mathf.Max(currHSpeed, 0);   // Ensure speed doesn't go negative
            }
            else if (currHSpeed < 0) // Decelerate if moving left
            {
                currHSpeed += decelSpeed * Time.deltaTime;
                currHSpeed = Mathf.Min(currHSpeed, 0); // Ensure speed doesn't go positive
            }
        }
    }

    bool IsGrounded()
    {
        // Check for ground collisions using a circle cast at the groundCheck position
        bool output = Physics2D.BoxCast(transform.position + 0.55f * Vector3.down, new Vector2(0.45f, 0.05f), 0, Vector2.up, 0, groundLayer);
        isJumping = false;
        return output;
    }

    public void Jump()
    {
        jumpTimeCounter -= Time.deltaTime; // Decrease jump time counter

        // Get input from the Vertical axis
        if (Input.GetButtonDown("Jump") && IsGrounded()) // Start jump if pressing up and grounded
        {
            isJumping = true;
            jumpTimeCounter = jumpTime; // Reset jump timer
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Apply initial jump force

            currState = PlayerState.JUMPING;
            AudioManager.instance.PlayAudio(jumpingClip, "jumpSound");
        }

        if (Input.GetButton("Jump") && isJumping) // Continue jump while holding up
        {
            if (jumpTimeCounter > 0) // Check remaining jump time
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Apply jump force              
            }
            else // Stop jump when timer runs out
            {
                isJumping = false;

                currState = PlayerState.JUMPING;
            }
        }

        if (Input.GetButtonUp("Jump")) // Stop jump when the axis is released early
        {
            isJumping = false;
        }

        if (jumpTimeCounter <= 0 && !IsGrounded()) // Apply gravity when not grounded
        {
            rb.velocity += Vector2.down * gravForce * Time.deltaTime; // Simulate gravity
        }
    }

    public Vector2 GetDirection(float dir)
    {
        if (dir > 0)
        {
            return Vector2.right;
        }
        if (dir < 0)
        {
            return Vector2.left;
        }
        else 
        { 
            return Vector2.zero; 
        }
    }

    public void RestartCharacter()
    {
        transform.position = new Vector2(positionX, positionY);
    }

    public PlayerState GetCurrentState()
    {
        return currState;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position + 0.55f * Vector3.down, new Vector2(0.45f, 0.05f));
    }
}