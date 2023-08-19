using UnityEngine;
using TMPro; // You'll need to add this for TextMeshPro components
using System.Collections;

public class CharacterDeath : MonoBehaviour
{
    public int maxHealth = 1;
    public GameObject deathEffect;
    [SerializeField] public Transform Player;
    [SerializeField] private string respawnTag = "Respawn"; // Tag for respawn points
    public TextMeshProUGUI respawnPopup; // Reference to the TextMeshPro text element

    private int currentHealth;
    private bool isDead = false;
   
    public void Start()
    {
        currentHealth = maxHealth;
        respawnPopup.gameObject.SetActive(false); // Hide the respawn popup initially
    }

    private void Update()
    {
        if (isDead)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(RespawnWithDelay());

                Player.MovetoPlace(false);
                Player.MovetoPlace(true);
            }
        }
    }

    private IEnumerator RespawnWithDelay()
    {
        respawnPopup.gameObject.SetActive(false); // Hide the respawn popup

        Debug.Log("RespawnWithDelay coroutine started");

        yield return new WaitForSeconds(3f);

        Debug.Log("RespawnWithDelay coroutine resumed");

        // Reset health
        currentHealth = maxHealth;

        // Find a respawn point with the specified tag
        GameObject[] respawnPoints = GameObject.FindGameObjectsWithTag(respawnTag);
        if (respawnPoints.Length > 0)
        {
            // Move the character to a random respawn point's position
            int randomIndex = Random.Range(0, respawnPoints.Length);
            Player.transform.position = respawnPoints[randomIndex].transform.position;
        }
        else
        {
            Debug.LogWarning("No respawn points found with tag: " + respawnTag);
        }

        // Re-enable movement and other components
        GetComponent<PlayerMovement>().enabled = true;

        isDead = false;
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDead) return;

        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;

        // Play death animation or other effects here

        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        // Disable movement and any other relevant components
        GetComponent<PlayerMovement>().enabled = false;

        respawnPopup.gameObject.SetActive(true); // Show the respawn popup
    }

    public bool IsDead()
    {
        return isDead;
    }
}
