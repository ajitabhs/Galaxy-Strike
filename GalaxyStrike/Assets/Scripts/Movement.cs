using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] float controlSpeed = 10f;
    [SerializeField] float xClampRange = 5f;
    [SerializeField] float yClampRange = 5f;

    [SerializeField] float pitchControl = 15f;
    [SerializeField] float rotationControl=20f;
    [SerializeField] float rotationSpeed = 5;

    Vector2 movement;

    void Update()
    {
        ProcessShipMotion();
        ProcessShipRotation();
    }

    void ProcessShipMotion()
    {
        /* The code snippet is calculating the movement offsets for the game object based on the input
        received from the player. */
        float xOffset = movement.x * controlSpeed * Time.deltaTime;
        float yOffset = movement.y * controlSpeed * Time.deltaTime;
        float rawX = transform.localPosition.x + xOffset;
        float rawY = transform.localPosition.y + yOffset;
        float clampedY = Mathf.Clamp(rawY, -yClampRange, yClampRange);
        float clampedX = Mathf.Clamp(rawX, -xClampRange, xClampRange);

        transform.localPosition = 
            new Vector3(clampedX, clampedY, 0);
    }

    void ProcessShipRotation()
    {
        //
        // We are rotating only on Z axis which is the axis of rotation
        // We need to be able to rotate to the same amount as we are moving left 
        // or right, that is why we are using movement.x
        // -1 : comes in because we have programmed the left arrow key as +ve and 
        // Right arrow key as -ve. So we have to change the direction of rotation.
        // 
        float roll = (-1) * rotationControl * movement.x;
        float pitch = (-1) * pitchControl * movement.y;

        Quaternion targetRotation = Quaternion.Euler(pitch, 0, roll);

        //Instead of assigning the rotation directly lets lerp it.
        // rotationSpeed is for controlling how fast the ship rotates

        transform.localRotation = Quaternion.Lerp(transform.localRotation,
            targetRotation,
            rotationSpeed * Time.deltaTime);
    }

    public void OnMove(InputValue Value)
    {
        movement.x = Value.Get<Vector2>().x;
        movement.y = Value.Get<Vector2>().y;
    }
}
