using UnityEngine;

public class RatSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public float spawnInterval = 2f;
    public float spawnForce = 10f; // Adjust the force as needed
    public float maxDeviationAngle = 45f; // Maximum angle deviation in degrees
    public int maxPopulation = 10; // Maximum number of spawned objects allowed
    public float objectLifetime = 5f; // Time after which spawned objects are destroyed

    private float timeSinceLastSpawn = 0f;
    private int currentPopulation = 0;

    private void Update()
    {
        // Count the time since the last spawn
        timeSinceLastSpawn += Time.deltaTime;

        // Check if it's time to spawn and if the population cap is not reached
        if (timeSinceLastSpawn >= spawnInterval && currentPopulation < maxPopulation)
        {
            // Reset the timer
            timeSinceLastSpawn = 0f;

            // Spawn the prefab
            GameObject spawnedObject = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);

            // Apply forward velocity with random angle deviation
            Rigidbody spawnedRigidbody = spawnedObject.GetComponent<Rigidbody>();
            if (spawnedRigidbody != null)
            {
                // Calculate a random deviation angle
                float deviationAngle = Random.Range(-maxDeviationAngle, maxDeviationAngle);

                // Calculate the direction rotated by the deviation angle
                Quaternion deviationRotation = Quaternion.Euler(0f, deviationAngle, 0f);
                Vector3 spawnDirection = deviationRotation * transform.forward;

                // Apply velocity
                spawnedRigidbody.velocity = spawnDirection.normalized * spawnForce;

                // Increase the current population count
                currentPopulation++;

                // Destroy the spawned object after the specified objectLifetime
                Destroy(spawnedObject, objectLifetime);
            }
        }
    }
}
