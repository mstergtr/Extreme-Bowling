using UnityEngine;
using UnityEngine.Events;

namespace SteamK12.ExtremeBowling
{
    public class PinTrigger : MonoBehaviour
    {
        [SerializeField] UnityEvent pinFell;
        [SerializeField] UnityEvent pinTouched;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                pinFell.Invoke();
                /*
                Unity events are great for quickly prototyping events, 
                but when you have a common recurring event then there are better ways!
                Calling a public method on a static instance of a gamemanager is one possible way, as shown below.
                */
                GameManager.Instance.DecreasePinCount();
                gameObject.GetComponent<SphereCollider>().enabled = false;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                /* 
                In the scenes I am using Unity events to trigger the audio clips on the Audio gameobject. 
                Note that the prefab will lose its reference and will need to be reassigned manually when placing the pins in a new scene.
                */
                pinTouched.Invoke();
            }
        }
    }
}
