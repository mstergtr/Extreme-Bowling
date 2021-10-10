using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteamK12.ExtremeBowling
{
    public class GravityShift : MonoBehaviour
    {
        public float gravityY = -9.8f;

        public void ResetGravity()
        {
            Physics.gravity = new Vector3(0, -9.8f, 0);
        }
        public void LowerGravity()
        {
            Physics.gravity = new Vector3(0, gravityY, 0);
        }
        public void IncreaseGravity()
        {
            Physics.gravity = new Vector3(0, gravityY, 0);
        }
    }
}
