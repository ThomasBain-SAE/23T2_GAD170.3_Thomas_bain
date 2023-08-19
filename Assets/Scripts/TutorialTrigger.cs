using TMPro; // Import the TextMeshPro namespace
using UnityEngine; // Import the Unity engine namespace

public class TutorialTrigger : MonoBehaviour // Declare the class named TutorialTrigger that inherits from MonoBehaviour
{
    public TextMeshProUGUI tutorialTrigger; // Public reference to a TextMeshPro GameObject in the Inspector
    public AudioClip enterSound; // Public reference to an audio clip in the Inspector

    private AudioSource audioSource; // Private reference to an AudioSource component

    private void Start() // Start method, executed when the script initializes
    {
        tutorialTrigger.enabled = false; // Disable the visibility of the tutorial text

        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component from this GameObject
        if (audioSource == null) // Check if there's no AudioSource component
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Add an AudioSource component to this GameObject
        }
    }

    private void OnTriggerEnter(Collider other) // Executed when a collider enters this trigger collider
    {
        if (other.CompareTag("Player")) // Check if the entering collider has the "Player" tag
        {
            Debug.Log("Player has entered the trigger zone."); // Output a log message

            if (enterSound != null && audioSource != null) // Check if the enter sound and AudioSource are set
            {
                audioSource.clip = enterSound; // Set the audio clip for the AudioSource
                audioSource.Play(); // Play the audio clip
            }

            tutorialTrigger.enabled = true; // Enable the visibility of the tutorial text
        }
    }

    private void OnTriggerExit(Collider other) // Executed when a collider exits this trigger collider
    {
        if (other.CompareTag("Player")) // Check if the exiting collider has the "Player" tag
        {
            Debug.Log("Player has exited the trigger zone."); // Output a log message

            tutorialTrigger.enabled = false; // Disable the visibility of the tutorial text
        }
    }
}