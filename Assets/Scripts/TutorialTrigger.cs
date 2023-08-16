using TMPro;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
   public TextMeshProUGUI tutorialTrigger; // Assign the TextMeshPro GameObject in the Inspector

    private void Start()
    {
        tutorialTrigger.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has entered the trigger zone.");

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
