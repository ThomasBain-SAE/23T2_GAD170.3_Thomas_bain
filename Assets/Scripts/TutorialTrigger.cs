using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public GameObject tutorialText; // Assign the TextMeshPro GameObject in the Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has entered the trigger zone.");

            // Show the tutorial text
            tutorialText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has exited the trigger zone.");

            // Hide the tutorial text
            tutorialText.SetActive(false);
        }
    }
}
