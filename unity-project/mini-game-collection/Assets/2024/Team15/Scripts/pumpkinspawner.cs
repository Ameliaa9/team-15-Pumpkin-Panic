using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGameCollection.Games2024.Team15
{
    public class PumpkinSpawner : MonoBehaviour
    {
        // References to the ScoreTracker
        private ScoreTracker scoreTracker;

        // Pumpkin Prefab and spawn position
        [SerializeField] private GameObject pumpkinPrefab; // Drag the pumpkin prefab here
        [SerializeField] private Transform[] spawnPoints; // Array of spawn points for pumpkins (drag these in Unity)
        [SerializeField] private float spawnInterval = 2f; // Time interval between spawns

        // Timer for spawning pumpkins
        private float spawnTimer;

        private void Start()
        {
            // Get reference to ScoreTracker
            scoreTracker = FindObjectOfType<ScoreTracker>();

            // Set the initial spawn timer
            spawnTimer = spawnInterval;
        }

        private void Update()
        {
            // Decrease the spawn timer
            spawnTimer -= Time.deltaTime;

            // If the timer is up, spawn a pumpkin
            if (spawnTimer <= 0f)
            {
                SpawnPumpkin();
                spawnTimer = spawnInterval; // Reset the timer
            }
        }

        // Spawn a pumpkin at a random spawn point
        private void SpawnPumpkin()
        {
            // Choose a random spawn point
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomIndex];

            // Instantiate the pumpkin at the selected spawn point
            GameObject pumpkin = Instantiate(pumpkinPrefab, spawnPoint.position, Quaternion.identity);

            // Optionally, add a Rigidbody2D or Rigidbody for falling (if not already set in the prefab)
            Rigidbody rb = pumpkin.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = pumpkin.AddComponent<Rigidbody>();
            }
            rb.useGravity = true; // Make sure gravity affects the pumpkin

            // Pass the reference of the ScoreTracker to the Pumpkin object
            Pumpkin pumpkinScript = pumpkin.GetComponent<Pumpkin>();
            if (pumpkinScript != null)
            {
                pumpkinScript.Initialize(scoreTracker);
            }
        }

        // Handle pumpkin collisions with players
        public class Pumpkin : MonoBehaviour
        {
            private ScoreTracker scoreTracker; // Store the reference to ScoreTracker

            // Initialize the pumpkin with a reference to the ScoreTracker
            public void Initialize(ScoreTracker scoreTracker)
            {
                this.scoreTracker = scoreTracker;
            }

            private void OnTriggerEnter(Collider other)
            {
                // Check for collisions with Player 1 and Player 2
                if (other.CompareTag("Player1"))
                {
                    // Call the method to reduce Player 1's life
                    scoreTracker.Player1LoseLife();

                    // Destroy the pumpkin after collision
                    Destroy(gameObject);
                }
                else if (other.CompareTag("Player2"))
                {
                    // Call the method to reduce Player 2's life
                    scoreTracker.Player2LoseLife();

                    // Destroy the pumpkin after collision
                    Destroy(gameObject);
                }
            }
        }
    }
}