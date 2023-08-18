using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform leftWaypoint;   // Leftmost waypoint
    public Transform rightWaypoint;  // Rightmost waypoint
    public float moveSpeed = 3.0f;   // Speed of the platform

    private bool movingRight = true;

    private void Update()
    {
        // Determine the target position based on the movement direction
        Vector3 targetPosition = movingRight ? rightWaypoint.position : leftWaypoint.position;

        // Calculate the movement direction
        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        // Move the platform
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Check if the platform has reached the target waypoint
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Change movement direction
            movingRight = !movingRight;
        }
    }
}
