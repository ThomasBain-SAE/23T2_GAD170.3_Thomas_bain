using System.Collections; // Import the System.Collections namespace
using System.Collections.Generic; // Import the System.Collections.Generic namespace
using Unity.VisualScripting; // Import the Unity.VisualScripting namespace (note: this seems unnecessary)
using UnityEngine; // Import the Unity engine namespace

public class ImpactTester : MonoBehaviour // Declare the class named ImpactTester
{
    // We want this script to handle collision and trigger detection
    // When our object impacts another, we want to PRINT TEXT

    // First, collisions....
    private void OnCollisionEnter(Collision collision) // Executed when a collision occurs
    {
        Debug.Log("Ouch!"); // Output a log message

        // Destroy the object that was collided with
        Destroy(collision.gameObject); // Destroy the GameObject that collided with our object
    }

    // Next, triggers

    private void OnTriggerEnter(Collider other) // Executed when a trigger collider is entered
    {
        Debug.Log("Ouch, I have Triggered something."); // Output a log message
    }
}
