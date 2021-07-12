using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RbAddForce : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 3.0f;
    public bool onGround = true;
    public float gravity = 10.0f;

    private Rigidbody rb;
    private float movementX;
    private float movementZ;
    private float verticalVelocity;
    private Vector3 movementForce;
    private bool isJumping;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        ReadInput();

        if (Mathf.Approximately(rb.velocity.y, 0.1f))
        {
            onGround = true;
        }
        else if (verticalVelocity < -15) //Accounts for obstacle pushing player
        {
            onGround = true;
        }
    }

    private void ReadInput()
    {
        movementX = Input.GetAxis("Horizontal");
        movementZ = Input.GetAxis("Vertical");

        if (onGround)
        {
            verticalVelocity = -gravity * Time.deltaTime;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        movementForce = new Vector3(movementX, verticalVelocity, movementZ);

        if (Input.GetButtonDown("Jump") && onGround)
        {
            isJumping = true;
            onGround = false;
        }
    }

    private void FixedUpdate() 
    {
        Move();
        Jump();    
    }

    private void Move()
    {
        rb.AddForce(movementForce * speed);
    }

    private void Jump()
    {
        if (isJumping)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = false;
        } 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("collided with ground");
            onGround = true;
        }
        else
        {
            onGround = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Boundary"))
        {
            onGround = false;
        }
    }
}
