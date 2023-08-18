using TMPro;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public TextMeshProUGUI tutorialTrigger; // Assign the TextMeshPro GameObject in the Inspector
    public AudioClip enterSound; // Assign the audio clip in the Inspector

    private AudioSource audioSource;

    private void Start()
    {
        tutorialTrigger.enabled = false;

        // Get the AudioSource component from the GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // If AudioSource component doesn't exist, add it
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has entered the trigger zone.");

            // Play the enter sound
            if (enterSound != null && audioSource != null)
            {
                audioSource.clip = enterSound;
                audioSource.Play();
            }

            // Show the tutorial text
            tutorialTrigger.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has exited the trigger zone.");

            // Hide the tutorial text
            tutorialTrigger.enabled = false;
        }
    }
}