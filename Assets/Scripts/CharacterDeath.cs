using UnityEngine;
using System.Collections;
public class CharacterDeath : MonoBehaviour
{
    public int maxHealth = 1;
    public GameObject deathEffect;
    [SerializeField] public Transform Player;
    [SerializeField] public Transform respawnPoint; // Assign the "RespawnPoint" GameObject here

    private int currentHealth;
    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (isDead && Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(RespawnWithDelay());
        }
    }

    private IEnumerator RespawnWithDelay()
    {
        Debug.Log("RespawnWithDelay coroutine started");

        yield return new WaitForSeconds(3f);

        Debug.Log("RespawnWithDelay coroutine resumed");


        // Reset health
        currentHealth = maxHealth;

        // Move the character to the respawn point's position
        Player.transform.position = respawnPoint.transform.position;

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

    public void Die()
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
    }
}