using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : MonoBehaviour
{
    [SerializeField] GameObject blocker;
    [SerializeField] private float movespeed = 10;
    private bool playerAtButton = false;
    private float deltaTime = 10f;
    private int buttonPressCount = 0; // Counter for button presses

    private void Update()
    {
        // Check if the player is at the button and if the "E" key is pressed
        if (playerAtButton && Input.GetKeyDown(KeyCode.E) && buttonPressCount < 7)
        {
            Debug.Log("E has been pressed");
            blocker.transform.position = blocker.transform.position + (Vector3.up * movespeed) * Time.deltaTime;
            if (blocker.transform.position.y < deltaTime)
            {
                movespeed = 1000;
            }

            buttonPressCount++; // Increment the button press counter
            if (buttonPressCount >= 7)
            {
                Debug.Log("Button has been pressed 7 times. Stopping functionality.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has entered the trigger zone.");
            playerAtButton = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has exited the trigger zone.");
            playerAtButton = false;
        }
    }
}
