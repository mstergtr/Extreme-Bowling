using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteamK12.ExtremeBowling
{
    public class Mover : MonoBehaviour
    {
        public float frequency = 1f;
        public float magnitude = 1f;
        public enum MoveDirection { UpDown, LeftRight };
        public MoveDirection selectedDirection;

        private Vector3 startPosition;
        private Vector3 direction;

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
            transform.position = startPosition + direction * Mathf.Sin(Time.time * frequency) * magnitude;
        }
    }
}
