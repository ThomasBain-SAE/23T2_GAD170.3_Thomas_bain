using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]


//My movement script has been moddified partially to allow for things I required
public class PlayerMovement : MonoBehaviour
{
    // INSTRUCTIONS
    // This script must be on an object that has a Character Controller component.
    // It will add this component if the object does not already have it.
    //    This is done by line 4: "[RequireComponent(typeof(CharacterController))]"
    //
    // Also, make a camera a child of this object and tilt it the way you want it to tilt.
    // The mouse will let you turn the object, and therefore, the camera.

    // These variables (visible in the inspector) are for you to set up to match the right feel
    [SerializeField] private float movementSpeed = 12f;
    private float yawSpeed = 2.0f;
    private float pitchSpeed = 2.0f;
    private float yawAngle = 0.0f;
    private float pitchAngle = 0.0f;

    Animator animator; // Reference to the Animator component
    int isRunningHash; // Hash code for the "isRunning" parameter in the Animator


    // This must be linked to the object that has the "Character Controller" in the inspector. You may need to add this component to the object
    private CharacterController controller;
    private Vector3 velocity;

    // Customisable gravity
    private float gravity = -20f;

    // Tells the script how far to keep the object off the ground
    private float groundDistance = 0.4f;

    // So the script knows if you can jump!
    private bool isGrounded;

    // How high the player can jump
    private float jumpHeight = 2f;

    private void Start()
    {
        // If the variable "controller" is empty...
        if (controller == null)
        {
            // ...then this searches the components on the gameobject and gets a reference to the CharacterController class
            controller = GetComponent<CharacterController>();
        }

        animator = GetComponent<Animator>(); // Get the Animator component from this GameObject
        isRunningHash = Animator.StringToHash("isRunning"); // Get the hash code for the "isRunning" parameter
    }
    public void MoveToPlace(bool status) // Custom method to enable/disable character movement
    {
        CharacterController controller = GetComponent<CharacterController>();
        controller.enabled = status;
    }

    private void Update()
    {
        // These lines let the script rotate the player based on the mouse moving
        yawAngle += yawSpeed * Input.GetAxis("Mouse X");
        pitchAngle -= pitchSpeed * Input.GetAxis("Mouse Y");

        // Get the Left/Right and Forward/Back values of the input being used (WASD, Joystick etc.)
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        {
            bool isRunning = animator.GetBool("isRunning");
            bool Forwardpressed = (Input.GetKey(KeyCode.W));

            //if player presses W key
            if (!isRunning && Forwardpressed)
            {
                //then set isRunning to true
                animator.SetBool(isRunningHash, true);
            }

            //if W key is not being pressed
            if (isRunning && !Forwardpressed)
            {
                //then set isRunning to false
                animator.SetBool(isRunningHash, false);  // Change this line
            }

        }
        // Let the player jump if they are on the ground and they press the jump button
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        // Rotate the player based off those mouse values we collected earlier
        transform.eulerAngles = new Vector3(0.0f, yawAngle, 0.0f);

        // This is stealing the data about the player being on the ground from the character controller
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // This fakes gravity!
        velocity.y += gravity * Time.deltaTime;

        // This takes the Left/Right and Forward/Back values to build a vector
        Vector3 move = transform.right * x + transform.forward * z;

        // Finally, it applies that vector it just made to the character
        controller.Move(move * movementSpeed * Time.deltaTime + velocity * Time.deltaTime);

    }
}