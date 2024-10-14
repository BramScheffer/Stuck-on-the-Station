using System.Collections;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    // Prefabs to spawn
    public GameObject objectToSpawn1;
    public GameObject objectToSpawn2;

    // Enum to define different object types
    public enum SpawnObjectType { Object1, Object2 };

    // Class to define spawn instruction with type and counts for both objects
    [System.Serializable]
    public class SpawnInstruction
    {
        public int object1Count; // Number of Object1 to spawn
        public int object2Count; // Number of Object2 to spawn
        public int countdown; // Countdown timer before the next round can start
        public Transform[] object1SpawnPoints; // Specific spawn points for Object1
        public Transform[] object2SpawnPoints; // Specific spawn points for Object2
    }

    // Array to hold the sequence of spawn instructions
    public SpawnInstruction[] spawnSequence;

    // Time interval between spawns
    public float spawnInterval = 2f;

    // Flag to control if spawning is enabled
    public bool spawnEnabled = true;

    // Current round index
    private int currentRoundIndex = 0;

    // Delay time after each round before starting the next
    public float roundDelay = 10f;

    // Random range for spawning around each spawn point
    public float spawnRange = 1.5f;

    void Start()
    {
        // Start the spawning coroutine
        StartCoroutine(SpawnRoutine());
    }

    // Coroutine for spawning objects at intervals
    IEnumerator SpawnRoutine()
    {
        while (spawnEnabled)
        {
            // Get the current spawn instruction
            SpawnInstruction currentInstruction = spawnSequence[currentRoundIndex];

            // Spawn Object1 specified times at its corresponding spawn points
            for (int i = 0; i < currentInstruction.object1Count; i++)
            {
                if (i < currentInstruction.object1SpawnPoints.Length)
                {
                    SpawnObject(SpawnObjectType.Object1, currentInstruction.object1SpawnPoints[i]);
                }
                yield return new WaitForSeconds(spawnInterval);
            }

            // Spawn Object2 specified times at its corresponding spawn points
            for (int i = 0; i < currentInstruction.object2Count; i++)
            {
                if (i < currentInstruction.object2SpawnPoints.Length)
                {
                    SpawnObject(SpawnObjectType.Object2, currentInstruction.object2SpawnPoints[i]);
                }
                yield return new WaitForSeconds(spawnInterval);
            }

            // Wait for the countdown to reach zero before moving to the next round
            while (currentInstruction.countdown > 0)
            {
                yield return null; // Wait until the countdown is decremented externally
            }

            // Wait for the round delay before moving to the next round
            yield return new WaitForSeconds(roundDelay);

            // Move to the next round
            currentRoundIndex++;
            if (currentRoundIndex >= spawnSequence.Length)
            {
                currentRoundIndex = 0; // Reset to the first round
            }
        }
    }

    // Function to spawn objects at specific spawn points with random range
    void SpawnObject(SpawnObjectType objectToSpawnType, Transform spawnPoint)
    {
        // Ensure that the spawn point is valid
        if (spawnPoint != null)
        {
            // Randomize the spawn position within a specified range around the spawn point
            Vector3 randomOffset = new Vector3(
                Random.Range(-spawnRange, spawnRange), // X-axis random range
                0, // Keep the Y-axis as is (assuming 2D spawning on the ground or flat surface)
                Random.Range(-spawnRange, spawnRange)  // Z-axis random range
            );

            // Final spawn position with offset
            Vector3 spawnPosition = spawnPoint.position + randomOffset;

            // Spawn the object based on the specified type
            switch (objectToSpawnType)
            {
                case SpawnObjectType.Object1:
                    Instantiate(objectToSpawn1, spawnPosition, Quaternion.identity);
                    break;
                case SpawnObjectType.Object2:
                    Instantiate(objectToSpawn2, spawnPosition, Quaternion.identity);
                    break;
            }
        }
        else
        {
            Debug.LogWarning("Spawn point is not assigned!");
        }
    }

    // Public method to manually decrement the countdown
    public void DecrementCountdown()
    {
        SpawnInstruction currentInstruction = spawnSequence[currentRoundIndex];

        // Check if the countdown can be decremented
        if (currentInstruction.countdown > 0)
        {
            currentInstruction.countdown--; // Decrease countdown
            Debug.Log($"Countdown decremented to: {currentInstruction.countdown}"); // Log current countdown
        }
        else
        {
            Debug.Log("Countdown has already reached zero.");
        }
    }

    // Optional: Method to get the current countdown value
    public int GetCurrentCountdown()
    {
        return spawnSequence[currentRoundIndex].countdown;
    }
}
    