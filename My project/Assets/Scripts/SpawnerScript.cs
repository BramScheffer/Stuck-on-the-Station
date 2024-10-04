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
    }

    // Array to hold the sequence of spawn instructions
    public SpawnInstruction[] spawnSequence;

    // Time interval between spawns
    public float spawnInterval = 2f;

    // Reference to the plane where objects should spawn
    public Transform spawnPlane;

    // Range of random position within the XZ area of the plane
    public float spawnRangeX = 5f;
    public float spawnRangeZ = 5f;

    // Fixed Y position (the height of the plane)
    public float planeY = 0f;

    // Flag to control if spawning is enabled
    public bool spawnEnabled = true;

    // Current round index
    private int currentRoundIndex = 0;

    // Delay time after each round before starting the next
    public float roundDelay = 10f;

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

            // Spawn Object1 specified times
            for (int i = 0; i < currentInstruction.object1Count; i++)
            {
                SpawnObject(SpawnObjectType.Object1);
                yield return new WaitForSeconds(spawnInterval);
            }

            // Spawn Object2 specified times
            for (int i = 0; i < currentInstruction.object2Count; i++)
            {
                SpawnObject(SpawnObjectType.Object2);
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

    // Function to spawn objects on the plane
    void SpawnObject(SpawnObjectType objectToSpawnType)
    {
        // Randomize X and Z coordinates within the defined range
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        float randomZ = Random.Range(-spawnRangeZ, spawnRangeZ);

        // Set spawn position on the plane (keeping Y at the plane height)
        Vector3 spawnPosition = new Vector3(
            spawnPlane.position.x + randomX,
            planeY,  // This keeps the object at the plane's Y level
            spawnPlane.position.z + randomZ
        );

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