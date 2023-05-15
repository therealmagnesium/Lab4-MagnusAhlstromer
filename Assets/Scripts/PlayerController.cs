using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool dead = false;

    private float speed = 50f;
    private float jumpForce = 12f;
    private float horizontalInput = 0f;
    private float verticalInput = 0f;
    private bool isGrounded = false;
    private bool shouldJump = false;
    private Rigidbody body;

    // Initialize components
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Do these all the time
    void Update()
    {
        if (!dead)
        {
            HandleInput();
            CheckForJump();
            ConstrainPlayer();
        }
    }

    // Do these all the time, but for physics
    void FixedUpdate()
    {
        if (!dead)
        {
            HandleMovement();
            
            if (shouldJump && isGrounded)
                Jump();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        // Do different things depending on what the player collides with
        switch (other.collider.tag)
        {
            case "ground":
                isGrounded = true;
                break;

            case "enemy":
                dead = true;
                Destroy(gameObject);
                break;

            default:
                break;
        }
    }

    void OnCollisionExit(Collision other)
    {
        // Do different things when the collison exits with the player and another object
        switch (other.collider.tag)
        {
            case "ground":
                isGrounded = false;
                shouldJump = false;
                break;

            default:
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // If we colide with the power up, double the player's size
        if (other.tag == "power_up")
        {
            transform.localScale = new Vector3(2f, 2f, 2f);
            Destroy(other.gameObject);
            Invoke("ResetScale", 4f);
        }
    }

    void HandleInput()
    {
        // These will return a value of either -1, 0, or 1
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    void HandleMovement()
    {
        // Add a force to the rigidbody based on the input
        Vector3 force = new Vector3(horizontalInput * speed * Time.fixedDeltaTime, 0f, verticalInput * speed * Time.fixedDeltaTime);
        body.AddForce(force, ForceMode.Impulse);
    }

    void CheckForJump()
    {
        // If the space bar is pressed, the player can jump
        if (Input.GetButtonDown("Jump") && isGrounded)
            shouldJump = true;
    }

    void Jump()
    {
        // Add a force to the y position of the player
        body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void ConstrainPlayer()
    {
        // Don't let the player fall off the stage
        if (transform.position.y < -5f)
        {
            transform.position = new Vector3(0f, 1f, 0f);
            body.velocity = Vector3.zero;
        }
    }

    void ResetScale()
    {
        // Resets the scale to <1, 1, 1>
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
