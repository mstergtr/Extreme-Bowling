using UnityEngine;
using UnityEngine.Events;

namespace SteamK12.ExtremeBowling
{
    public class SimpleTrigger : MonoBehaviour
    {
        [SerializeField] string triggerTag = "Player";
        [SerializeField] UnityEvent onTriggerEnter;
        private void OnTriggerEnter(Collider other) 
        {
            if (other.gameObject.CompareTag(triggerTag))
            {
                onTriggerEnter.Invoke();
            }
        }
    }
}
