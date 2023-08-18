using UnityEngine;

public class RatSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public AudioClip spawnSound; // Assign the audio clip in the Inspector
    private AudioSource audioSource;

    private float spawnInterval = 2f;
    private float spawnForce = 10f;
    private float maxDeviationAngle = 45f;
    private int maxPopulation = 500;
    private float objectLifetime = 5f;

    private float timeSinceLastSpawn = 0f;
    private int currentPopulation = 0;

    private void Start()
    {
        // Get or add the AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval && currentPopulation < maxPopulation)
        {
            timeSinceLastSpawn = 0f;

            // Play the spawn sound
            if (spawnSound != null && audioSource != null)
            {
                audioSource.clip = spawnSound;
                audioSource.Play();
            }

            GameObject spawnedObject = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);

            Rigidbody spawnedRigidbody = spawnedObject.GetComponent<Rigidbody>();
            if (spawnedRigidbody != null)
            {
                float deviationAngle = Random.Range(-maxDeviationAngle, maxDeviationAngle);
                Quaternion deviationRotation = Quaternion.Euler(0f, deviationAngle, 0f);
                Vector3 spawnDirection = deviationRotation * transform.forward;

                spawnedRigidbody.velocity = spawnDirection.normalized * spawnForce;

                currentPopulation++;

                Destroy(spawnedObject, objectLifetime);
            }
        }
    }
}