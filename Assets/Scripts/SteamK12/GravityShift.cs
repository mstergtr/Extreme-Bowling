using UnityEngine;

namespace SteamK12.ExtremeBowling
{
    public class GravityShift : MonoBehaviour
    {
        [SerializeField] float gravityY = -9.8f;

        //Make sure to reset the gravity when the level restarts!
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
            Physics.gravity = new Vector3(0, -gravityY, 0);
        }
    }
}
