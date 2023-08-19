using UnityEngine; // Import the Unity engine namespace
using TMPro; // Import the TextMeshPro namespace
using System.Collections; // Import the System.Collections namespace

public class CharacterDeath : MonoBehaviour // Declare the class named CharacterDeath
{
    public int maxHealth = 1; // Maximum health of the character
    public GameObject deathEffect; // Effect to instantiate on death
    [SerializeField] public Transform Player; // Reference to the player's transform
    [SerializeField] private string respawnTag = "Respawn"; // Tag for respawn points
    public TextMeshProUGUI respawnPopup; // Reference to the TextMeshPro text element for respawn UI

    private int currentHealth; // Current health of the character
    private bool isDead = false; // Flag to indicate if the character is dead

    private int deathCount = 0; // Counter for the number of times the character has died

    public void Start() // Start method, executed when the script initializes
    {
        currentHealth = maxHealth; // Set current health to maximum health
        respawnPopup.gameObject.SetActive(false); // Hide the respawn popup initially
    }

    private void Update() // Update method, executed every frame
    {
        if (isDead) // If the character is dead
        {
            if (Input.GetKeyDown(KeyCode.R)) // Check if the R key is pressed
            {
                StartCoroutine(RespawnWithDelay()); // Start the respawn coroutine
            }
        }
    }

    private IEnumerator RespawnWithDelay() // Coroutine for respawning with a delay
    {
        respawnPopup.gameObject.SetActive(false); // Hide the respawn popup

        Debug.Log("RespawnWithDelay coroutine started");

        yield return new WaitForSeconds(3f); // Wait for 3 seconds

        Debug.Log("RespawnWithDelay coroutine resumed");

        currentHealth = maxHealth; // Reset the health to maximum

        GameObject[] respawnPoints = GameObject.FindGameObjectsWithTag(respawnTag); // Find respawn points with the specified tag
        if (respawnPoints.Length > 0) // Check if respawn points were found
        {
            int randomIndex = Random.Range(0, respawnPoints.Length); // Get a random index
            Player.gameObject.GetComponent<PlayerMovement>().MoveToPlace(false); // Disable player movement temporarily
            Player.transform.position = respawnPoints[randomIndex].transform.position; // Move player to the selected respawn point
            Player.gameObject.GetComponent<PlayerMovement>().MoveToPlace(true); // Enable player movement
        }
        else
        {
            Debug.LogWarning("No respawn points found with tag: " + respawnTag); // Log a warning if no respawn points were found
        }

        GetComponent<PlayerMovement>().enabled = true; // Enable player movement

        isDead = false; // Reset the death flag

        deathCount++; // Increment death count
    }

    public void TakeDamage(int damageAmount) // Method to take damage
    {
        if (isDead) return; // If the character is already dead, return

        currentHealth -= damageAmount; // Reduce health by damage amount

        if (currentHealth <= 0) // Check if health is zero or negative
        {
            Die(); // Call the Die method
        }
    }

    private void Die() // Method executed when the character dies
    {
        if (isDead) return; // If the character is already dead, return

        isDead = true; // Set the death flag

        if (deathEffect != null) // Check if a death effect prefab is assigned
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity); // Instantiate the death effect
        }

        GetComponent<PlayerMovement>().enabled = false; // Disable movement and other relevant components

        respawnPopup.gameObject.SetActive(true); // Show the respawn popup

        deathCount++; // Increment death count
    }

    public bool IsDead() // Method to check if the character is dead
    {
        return isDead; // Return the value of the death flag
    }

    public int GetDeathCount() // Method to get the death count
    {
        return deathCount; // Return the death count
    }
}