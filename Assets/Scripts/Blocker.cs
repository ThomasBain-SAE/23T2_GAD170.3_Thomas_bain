using System.Collections; // Import the System.Collections namespace
using System.Collections.Generic; // Import the System.Collections.Generic namespace
using UnityEngine; // Import the Unity engine namespace

public class Blocker : MonoBehaviour // Declare the class named Blocker
{
    [SerializeField] GameObject blocker; // Reference to the blocker GameObject
    [SerializeField] private float movespeed = 10; // Speed at which the blocker moves
    [SerializeField] ParticleSystem particleEffect; // Reference to the Particle System
    [SerializeField] AudioClip audioClip; // Reference to the Audio Clip
    private bool playerAtButton = false; // Flag to indicate if the player is at the button
    private float deltaTime = 10f; // A threshold value for changes in position

    private int buttonPressCount = 0; // Counter for button presses

    private bool particleEffectActivated = false; // Track if the particle effect has been activated
    private bool audioEffectPlayed = false; // Track if the audio effect has been played

    private AudioSource audioSource; // Reference to the AudioSource component

    private void Start() // Start method, executed when the script initializes
    {
        if (particleEffect != null)
        {
            particleEffect.Stop(); // Stop the Particle System on start
        }
        else
        {
            Debug.LogWarning("Particle effect reference is null. Make sure to assign it in the Inspector.");
        }

        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
    }

    private void Update() // Update method, executed every frame
    {
        // Check if the player is at the button and if the "E" key is pressed
        if (playerAtButton && Input.GetKeyDown(KeyCode.E) && buttonPressCount < 7)
        {
            Debug.Log("E has been pressed");
            // Move the blocker upward based on movespeed and time
            blocker.transform.position = blocker.transform.position + (Vector3.up * movespeed) * Time.deltaTime;

            if (blocker.transform.position.y < deltaTime) // Check if the blocker has moved above a certain threshold
            {
                movespeed = 1050; // Increase the movespeed significantly
            }

            buttonPressCount++; // Increment the button press counter

            if (buttonPressCount >= 7 && !particleEffectActivated) // Check if button pressed 7 times and particle effect not activated
            {
                Debug.Log("Button has been pressed 7 times. Activating particle effect.");
                if (particleEffect != null)
                {
                    particleEffect.Play(); // Play the Particle System
                    particleEffectActivated = true; // Mark the particle effect as activated
                }
            }
        }

        if (buttonPressCount >= 7 && !audioEffectPlayed) // Check if button pressed 7 times and audio effect not played
        {
            Debug.Log("Button has been pressed 7 times. Playing audio effect.");
            if (audioClip != null && audioSource != null)
            {
                audioSource.PlayOneShot(audioClip); // Play the Audio Clip
                audioEffectPlayed = true; // Mark the audio effect as played
            }
        }
    }

    private void OnTriggerEnter(Collider other) // Executed when a collider enters this trigger collider
    {
        if (other.CompareTag("Player")) // Check if the entering collider has the "Player" tag
        {
            Debug.Log("Player has entered the trigger zone.");
            playerAtButton = true; // Set the playerAtButton flag to true
        }
    }

    private void OnTriggerExit(Collider other) // Executed when a collider exits this trigger collider
    {
        if (other.CompareTag("Player")) // Check if the exiting collider has the "Player" tag
        {
            Debug.Log("Player has exited the trigger zone.");
            playerAtButton = false; // Set the playerAtButton flag to false
        }
    }
}