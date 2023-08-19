using UnityEngine; // Import the Unity engine namespace

public class RatSpawner : MonoBehaviour // Declare the class named RatSpawner that inherits from MonoBehaviour
{
    public GameObject prefabToSpawn; // Public reference to the GameObject to be spawned
    public AudioClip spawnSound; // Public reference to an audio clip in the Inspector
    private AudioSource audioSource; // Private reference to an AudioSource component

    private float spawnInterval = 1.3f; // Time interval between spawns
    private float spawnForce = 50f; // Force applied to the spawned object
    private float maxDeviationAngle = 45f; // Maximum angle deviation for spawn direction
    private int maxPopulation = 500; // Maximum number of spawned objects allowed
    private float objectLifetime = 10f; // Time before spawned objects are destroyed

    private float timeSinceLastSpawn = 0f; // Time elapsed since the last spawn
    private int currentPopulation = 0; // Current number of spawned objects

    private void Start() // Start method, executed when the script initializes
    {
        // Get or add the AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update() // Update method, executed every frame
    {
        timeSinceLastSpawn += Time.deltaTime; // Increment time since last spawn

        // Check if it's time to spawn and the population limit isn't exceeded
        if (timeSinceLastSpawn >= spawnInterval && currentPopulation < maxPopulation)
        {
            timeSinceLastSpawn = 0f; // Reset time since last spawn

            // Play the spawn sound if assigned and AudioSource exists
            if (spawnSound != null && audioSource != null)
            {
                audioSource.clip = spawnSound;
                audioSource.Play();
            }

            // Instantiate the prefabToSpawn at the spawner's position with no rotation
            GameObject spawnedObject = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);

            Rigidbody spawnedRigidbody = spawnedObject.GetComponent<Rigidbody>(); // Get the spawned object's Rigidbody
            if (spawnedRigidbody != null)
            {
                float deviationAngle = Random.Range(-maxDeviationAngle, maxDeviationAngle); // Generate a random angle
                Quaternion deviationRotation = Quaternion.Euler(0f, deviationAngle, 0f); // Convert angle to rotation
                Vector3 spawnDirection = deviationRotation * transform.forward; // Calculate spawn direction

                spawnedRigidbody.velocity = spawnDirection.normalized * spawnForce; // Apply force to the spawned object

                currentPopulation++; // Increment the spawned object count

                // Destroy the spawned object after a set amount of time
                Destroy(spawnedObject, objectLifetime);
            }
        }
    }
}
