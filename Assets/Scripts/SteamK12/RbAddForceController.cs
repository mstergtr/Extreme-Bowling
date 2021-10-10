using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteamK12.ExtremeBowling
{
    public class RbAddForceController : MonoBehaviour
    {
        public float speed = 5.0f;
        public float jumpForce = 3.0f;
        public float gravity = 10.0f;
        private Rigidbody rb;
        private float movementX;
        private float movementZ;
        private Vector3 movementForce;
        private float verticalVelocity;
        public bool isGrounded;
        private bool canCheckGround = true;
        public LayerMask groundLayer;
        private bool isJumping;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            ReadInput();
            if (canCheckGround)
            {
                isGrounded = Physics.CheckSphere(transform.position, 0.65f, groundLayer);
            }
        }
        //used to allow player to be influenced by end game grav effect
        public void GroundCheckEnable(bool checkGround) 
        {
            canCheckGround = checkGround;
            isGrounded = true;
        }

        private void ReadInput()
        {
            movementX = Input.GetAxis("Horizontal");
            movementZ = Input.GetAxis("Vertical");

            if (isGrounded)
            {
                verticalVelocity = -gravity * Time.deltaTime;
            }
            else
            {
                verticalVelocity -= gravity * Time.deltaTime;
            }
            
            movementForce = new Vector3(movementX, verticalVelocity, movementZ);

            if (Input.GetButton("Jump") && isGrounded)
            {
                isJumping = true;
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
    }
}
