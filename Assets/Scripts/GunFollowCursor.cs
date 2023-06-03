using UnityEngine;

public class GunFollowCursor : MonoBehaviour
{
    public Transform centerPoint;  // Reference to the center point GameObject

    public float rotationSpeedMouse = 10f;  // Speed at which the object rotates around the center when using the mouse
    public float rotationSpeedController = 5f;  // Speed at which the object rotates around the center when using the controller

    private void Update()
    {
        // Check if mouse input is available
        bool isMouseAvailable = Input.mousePresent;

        // Get the position of the cursor or right stick input based on availability
        Vector3 inputPosition = isMouseAvailable ? Camera.main.ScreenToWorldPoint(Input.mousePosition) : GetControllerInput();

        inputPosition.z = 0f;  // Ensure the z-coordinate is zero (2D space)

        // Calculate the direction from the center point to the cursor or input
        Vector3 direction = inputPosition - centerPoint.position;

        // Calculate the desired rotation around the center point based on the input source
        float angle = isMouseAvailable ? Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg : GetControllerAngle(direction);
        Quaternion desiredRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Apply the rotation to the object
        transform.rotation = desiredRotation;

        // Calculate the position offset from the center point
        Vector3 offset = Quaternion.Euler(0f, 0f, angle) * Vector3.right;

        // Apply the rotation offset to the object's position
        transform.position = centerPoint.position + offset;
    }

    private Vector3 GetControllerInput()
    {
        // Get the input values from the right stick of the controller
        float inputX = Input.GetAxis("RightStickHorizontal");
        float inputY = Input.GetAxis("RightStickVertical");

        // Create a vector from the input values
        Vector3 inputVector = new Vector3(inputX, inputY, 0f);

        // Normalize the input vector and multiply it by the rotation speed
        inputVector = inputVector.normalized * rotationSpeedController;

        // Calculate the position based on the input vector and the center point
        Vector3 inputPosition = centerPoint.position + inputVector;

        return inputPosition;
    }

    private float GetControllerAngle(Vector3 direction)
    {
        // Calculate the angle using the direction vector
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Adjust the angle based on the rotation speed
        angle += Time.deltaTime * rotationSpeedController;

        return angle;
    }
}