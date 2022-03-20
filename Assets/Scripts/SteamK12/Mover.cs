using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteamK12.ExtremeBowling
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] float frequency = 1f;
        [SerializeField] float magnitude = 1f;
        public enum MoveDirection { UpDown, LeftRight };
        public MoveDirection selectedDirection;
        [SerializeField] float xAngle, yAngle, zAngle;

        private Vector3 startPosition, direction;
        
        void Start()
        {
            if (selectedDirection == MoveDirection.UpDown)
            {
                direction = transform.up;
            }
            else
            {
                direction = transform.right;
            }

            startPosition = transform.position;
        }

        void Update()
        {
            Vector3 rotationAngles = new Vector3(xAngle, yAngle, zAngle) * Time.deltaTime;
            transform.Rotate(rotationAngles, Space.World);
            transform.position = startPosition + direction * Mathf.Sin(Time.time * frequency) * magnitude;
        }
    }
}
