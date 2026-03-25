using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] GameObject [] weapons;
    [SerializeField] RectTransform crossHair;

    [SerializeField] Transform targetObject;
    [SerializeField] float targetDistance=50f;
    private void Start()
    {
        Cursor.visible = false;    
    }

    private void Update()
    {
        MoveCrosshair();
        MoveTarget();
        AimLasers();
    }
    public void OnFire(InputValue value)
    {
        foreach (GameObject weapon in weapons)
        {
            var emissionModule = weapon.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = value.isPressed;
        }
    }

    private void MoveCrosshair()
    {
        crossHair.position = Input.mousePosition;
    }

    private void MoveTarget()
    {
        Vector3 targetPos =  new Vector3(Input.mousePosition.x, Input.mousePosition.y, targetDistance);
        targetObject.transform.position = Camera.main.ScreenToWorldPoint(targetPos);
    }

    private void AimLasers()
    {
        foreach (GameObject weapon in weapons)
        {
            // Should we aim using the weapon or using the ship??
            // lets try both..

            // 
            // This one is for calculating the direction using the vector of the weapon. 
            // it resulted in lasers crossing each other. This may be good for some
            //
            // Vector3 fireDirection = targetObject.transform.position - weapon.transform.position;

            // This is where we user the position vector of the ship to calculate the fire direction.
            // this will result in laser being parallel to each other no matter how far the distance is. 
            Vector3 fireDirection = targetObject.transform.position - this.transform.position;

            Quaternion rotationToTarget = Quaternion.LookRotation(fireDirection);
            weapon.transform.rotation = rotationToTarget;

        }
    }
}
