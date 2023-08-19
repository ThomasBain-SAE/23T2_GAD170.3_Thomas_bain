using UnityEngine; // Import the Unity engine namespace

public class DamageDealer : MonoBehaviour // Declare the class named DamageDealer
{
    public int damageAmount = 10; // Amount of damage to deal

    private void OnCollisionEnter(Collision collision) // Executed when a collision occurs
    {
        // Check if the collided object can take damage
        CharacterDeath characterDeath = collision.gameObject.GetComponent<CharacterDeath>();
        if (characterDeath != null) // If the collided object has a CharacterDeath component
        {
            // Deal damage to the collided object by calling its TakeDamage method
            characterDeath.TakeDamage(damageAmount);

        }
    }
}


