using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] float controlSpeed = 10f;
    Vector2 movement;

    void Update()
    {
        ProcessShipMotion();
    }

    void ProcessShipMotion()
    {
        float xOffset = movement.x * controlSpeed * Time.deltaTime;
        float yOffset = movement.y * controlSpeed * Time.deltaTime;

        transform.localPosition = 
            new Vector3(transform.localPosition.x + xOffset, transform.localPosition.y + yOffset, 0);

    }

    public void OnMove(InputValue Value)
    {
        movement.x = Value.Get<Vector2>().x;
        movement.y = Value.Get<Vector2>().y;
    }
}
