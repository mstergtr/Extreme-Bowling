using UnityEngine;

namespace SteamK12.ExtremeBowling
{
    public class Explosion : MonoBehaviour
    {
        public float blastRadius = 10f;
        public float expForce = 10f;

        public void Explode()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);

            foreach (Collider nearbyObject in colliders)
            {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(expForce, transform.position, blastRadius);
                }
            }

            Destroy(gameObject);
        }
    }
}
