using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float shootPower; // temp: should be in weapon properties;
    float MoveAmount { get => Time.deltaTime * moveSpeed; }
    float RotationAmount { get => Time.deltaTime * rotationSpeed; }
    Camera mainCam;
    Transform weapon, body;

    void Start()
    {
        mainCam = Camera.main;
        weapon = transform.Find("Body").Find("Weapon");
    }

    void Update()
    {
        AimAtCursor();
        HandleMovement();
        HandleShoot();
    }

    void HandleMovement()
    {
        var verticalMovement = Input.GetAxis("Vertical") * MoveAmount;
        var horizontalMovement = Input.GetAxis("Horizontal") * MoveAmount;
        var rotationMovement = Input.GetAxis("Mouse X") * RotationAmount;
        transform.Rotate(new Vector3(-verticalMovement, rotationMovement, horizontalMovement));
    }

    void HandleShoot()
    {
        if(Input.GetMouseButton(0))
            ShootWeapon();
    }

    void AimAtCursor()
    {
        Vector3 aim, mousePos = Input.mousePosition;
        var tr = GetPointedObjectTransform(mousePos);
        if(tr != null)
            aim = tr.position;
        else
            aim = mainCam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mainCam.farClipPlane));

        weapon.LookAt(aim);
    }

    void ShootWeapon()
    {
        var bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        bullet.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        bullet.transform.position = weapon.position + 2*weapon.forward;
        var rb = bullet.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.AddForce(weapon.forward * shootPower, ForceMode.Impulse);
    }

    Transform GetPointedObjectTransform(Vector3 mousePosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
            if (hit.collider != null)
                return hit.collider.transform;
        return null;
    }
}
