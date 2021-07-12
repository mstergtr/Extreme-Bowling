using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DeathVolume : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LevelManager.instance.RestartScene();
        }

        if (other.gameObject.CompareTag("BouncyBall"))
        {
            Destroy(other.gameObject);
        }
    }
}
