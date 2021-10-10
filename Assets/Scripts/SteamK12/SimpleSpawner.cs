using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteamK12.ExtremeBowling
{
    public class SimpleSpawner : MonoBehaviour
    {
        public GameObject enemy;
        public Transform[] spawnPoints;
        public bool isSpawning = true;

        public float timeBetweenSpawns = 2f;
        public float spawnTime = 1f;

        void Start()
        {
            if (spawnPoints.Length == 0)
            {
                Debug.Log("no spawn points referenced");
            }
            InvokeRepeating(nameof(SpawnEnemy), spawnTime, timeBetweenSpawns);
        }

        void SpawnEnemy()
        {
            if (!isSpawning) return;

            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
