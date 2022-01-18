using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteamK12.ExtremeBowling
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float speed = 5.0f;
        [SerializeField] float maxSpeed = 10.0f;
        [SerializeField] float gravityModifier = 10.0f;
        [SerializeField] float jumpForce = 10.0f;
        [SerializeField] LayerMask groundLayer;
        [SerializeField] bool isGrounded;
        private bool canCheckGround = true;
        private float verticalVelocity;
        private bool isJumping;
        private Rigidbody rb;
        private float movementX, movementZ;
        private Vector3 movementForce;

        void Awake() 
        {
            rb = GetComponent<Rigidbody>();
        }
        void Update()
        {
            ReadInput();

            if (isGrounded)
            {
                verticalVelocity = -gravityModifier * Time.deltaTime;
            }
            else
            {
                verticalVelocity -= gravityModifier * Time.deltaTime;
            }

            if (canCheckGround)
            {
                isGrounded = Physics.CheckSphere(transform.position, 0.65f, groundLayer);
            }
        }

        //used to allow player to be influenced by end game grav effect. Called from the GameManager WinEvent.
        public void GroundCheckEnable(bool checkGround) 
        {
            canCheckGround = checkGround;
            maxSpeed = 30f;
            isGrounded = true;
        }

        private void ReadInput()
        {
            movementX = Input.GetAxis("Horizontal");
            movementZ = Input.GetAxis("Vertical");

            movementForce = new Vector3(movementX, verticalVelocity, movementZ);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                isJumping = true;
            }
        }

        private void FixedUpdate()
        {
            Move();
            Jump();

            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
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
