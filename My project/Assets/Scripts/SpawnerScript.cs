using System.Collections;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    // The prefab to spawn
    public GameObject objectToSpawn;

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
            SpawnObject();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Function to spawn objects on the plane
    void SpawnObject()
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

        // Instantiate the object at the calculated position with no rotation
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    }
}
