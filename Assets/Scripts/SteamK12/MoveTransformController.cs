using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteamK12.ExtremeBowling
{
    public class MoveTransformController : MonoBehaviour
    {
        public float speed = 1.0f;
        public float rotateSpeed = 10.0f;

        void Update()
        {
            Move();
            Jump();
            Rotate();
        }
        private void Rotate()
        {
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(Vector3.down * rotateSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
            }
        }
        private void Jump()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                transform.position += Vector3.up * speed * Time.deltaTime;
            }
        }
        private void Move()
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += Vector3.forward * speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position += Vector3.back * speed * Time.deltaTime;
            }
        }
    }
}
