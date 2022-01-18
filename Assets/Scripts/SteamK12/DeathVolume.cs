using UnityEngine;

namespace SteamK12.ExtremeBowling
{
    public class DeathVolume : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                LevelManager.Instance.RestartScene();
            }

            if (other.gameObject.CompareTag("BouncyBall"))
            {
                Destroy(other.gameObject);
            }
        }
    }
}

