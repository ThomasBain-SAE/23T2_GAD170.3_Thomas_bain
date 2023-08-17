using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damageAmount = 10; // Amount of damage to deal

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object can take damage
        CharacterDeath characterDeath = collision.gameObject.GetComponent<CharacterDeath>();
        if (characterDeath != null)
        {
            // Deal damage to the collided object
            characterDeath.TakeDamage(damageAmount);

            // Destroy the spawned object after dealing damage (optional)
           // Destroy(gameObject);
        }
    }
}
