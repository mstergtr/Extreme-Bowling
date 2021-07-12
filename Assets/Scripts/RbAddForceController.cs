using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RbAddForceController : MonoBehaviour
{
    public float speed = 5.0f;
    public bool onGround = true;
    public float jumpForce = 3.0f;
    public float gravity = 10.0f;
    //public Transform camPivot;
    //public Transform cam;
    
    private Rigidbody rb;
    private float movementX;
    private float movementZ;
    private Vector3 movementForce;
    private float verticalVelocity;
    private bool isJumping;
    //private float heading = 0;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ReadInput();

        if (Mathf.Approximately(rb.velocity.y, 0.1f))
        {
            onGround = true;
        }
        else if (verticalVelocity < -10) //Accounts for obstacle pushing player
        {
            onGround = true;
        }
    }

    private void ReadInput()
    {
        //heading += Input.GetAxis("Mouse X") * Time.deltaTime * 180;
        //camPivot.rotation = Quaternion.Euler(0, heading, 0);

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

        if (Input.GetButton("Jump") && onGround)
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

    private void Jump()
    {
        if (isJumping)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = false;
        }
    }

    private void Move()
    {
        rb.AddForce(movementForce * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boundary"))
        {
            onGround = false;
        }
    }
}
