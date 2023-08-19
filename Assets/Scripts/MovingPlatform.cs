using UnityEngine; // Import the Unity engine namespace

public class MovingPlatform : MonoBehaviour // Declare the class named MovingPlatform
{
    public Transform leftWaypoint; // Reference to the leftmost waypoint
    public Transform rightWaypoint; // Reference to the rightmost waypoint
    public float moveSpeed = 3.0f; // Speed of the platform

    private bool movingRight = true; // Variable to track the movement direction

    private void Update() // Update method, executed every frame
    {
        // Determine the target position based on the movement direction
        Vector3 targetPosition = movingRight ? rightWaypoint.position : leftWaypoint.position;

        // Calculate the movement direction
        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        // Move the platform based on the calculated direction and speed
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Check if the platform has reached the target waypoint with a small tolerance
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Change movement direction by toggling the boolean value
            movingRight = !movingRight;
        }
    }
}
