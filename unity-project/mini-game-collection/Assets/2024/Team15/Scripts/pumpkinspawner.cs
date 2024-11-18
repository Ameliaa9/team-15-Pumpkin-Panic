using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGameCollection.Games2024.Team15
{
    public class PumpkinSpawner : MonoBehaviour
    {
        public GameObject pumpkinPrefab;       // Pumpkin prefab to spawn
        public float spawnInterval = 1f;       // Minimum time interval between spawns
        private float spawnTimer = 0f;         // Timer to track spawn interval

        public int pumpkinsToSpawnAtOnce = 5;  // Number of pumpkins to spawn at once

        // Reference to the platform collider to get its bounds
        public Collider platformCollider;

        public float spawnHeightOffset = 20f;  // How high above the platform to spawn pumpkins

        private void Update()
        {
            spawnTimer += Time.deltaTime;

            // Only spawn pumpkins if the spawn timer reaches the minimum interval
            if (spawnTimer >= spawnInterval)
            {
                spawnTimer = 0f;
                SpawnMultiplePumpkins();  // Call method to spawn multiple pumpkins with random delays
            }
        }

        void SpawnMultiplePumpkins()
        {
            if (platformCollider != null)
            {
                // Get the platform's bounds
                Bounds platformBounds = platformCollider.bounds;

                for (int i = 0; i < pumpkinsToSpawnAtOnce; i++)  // Loop to spawn multiple pumpkins
                {
                    // Generate random X and Z positions within the platform's bounds
                    float randomX = Random.Range(platformBounds.min.x, platformBounds.max.x);
                    float randomZ = Random.Range(platformBounds.min.z, platformBounds.max.z);
                    float spawnY = platformBounds.max.y + spawnHeightOffset;  // Height above the platform with added offset

                    // Set the spawn position
                    Vector3 spawnPosition = new Vector3(randomX, spawnY, randomZ);

                    // Randomize the delay for each pumpkin to fall at different times
                    float randomDelay = Random.Range(0f, spawnInterval); // Random delay between 0 and spawnInterval

                    // Start a coroutine to instantiate the pumpkin after the random delay
                    StartCoroutine(SpawnPumpkinWithDelay(spawnPosition, randomDelay));
                }
            }
        }

        // Coroutine to handle delayed pumpkin spawn
        IEnumerator SpawnPumpkinWithDelay(Vector3 spawnPosition, float delay)
        {
            // Wait for the random delay before spawning the pumpkin
            yield return new WaitForSeconds(delay);

            // Instantiate the pumpkin at the spawn position after the delay
            Instantiate(pumpkinPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
