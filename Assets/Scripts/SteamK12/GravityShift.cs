using System.Collections;
using UnityEngine;

namespace SteamK12.ExtremeBowling
{
    public class GravityShift : MonoBehaviour
    {
        [SerializeField] float gravityY = -9.8f;
        [SerializeField] bool isGravityTimerOn;

        private void Start() 
        {
            if (!isGravityTimerOn) return;

            StartCoroutine(CoGravityTimer());
        }
        IEnumerator CoGravityTimer()
        {
            while (isGravityTimerOn)
            {
                LowerGravity();
                yield return new WaitForSeconds(3f);
                IncreaseGravity();
                yield return new WaitForSeconds(3f);
            }
        }

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
