using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float shootPower;
    [SerializeField] float shootRate;
    float MoveAmount { get => Time.deltaTime * moveSpeed; }
    float RotationAmount { get => Time.deltaTime * rotationSpeed; }
    Camera mainCam;
    [SerializeField] Transform barrelTranform, spawnPoint;
    [SerializeField] float autoLockDuration;
    Weapon[] weapons;

    void Start()
    {
        mainCam = Camera.main;
        weapons = new Weapon[2];
        weapons[0] = new SimpleWeapon(shootPower, shootRate);
        weapons[1] = new MissileWeapon();
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
        var horizontalMovement = Input.GetAxis("Horizontal") * RotationAmount;
        // var rotationMovement = Input.GetAxis("Mouse X") * RotationAmount;
        transform.Rotate(new Vector3(-verticalMovement, horizontalMovement));
    }

    void HandleShoot()
    {
        bool shootAllowed = aim != Vector3.zero && target?.position != Vector3.zero;
        
        for(int i = 0; i < weapons.Length; i++)
        {
            if(Input.GetMouseButton(i) && shootAllowed)
                weapons[i].ActivateOnHold(aim, target, spawnPoint.position);
            else
                weapons[i].DeactivateOnHold();

            if(Input.GetMouseButtonDown(i) && shootAllowed)
                weapons[i].ActivateOnDown(aim, target, spawnPoint.position);
            if(Input.GetMouseButtonUp(i))
                weapons[i].DeactivateOnUp();
        }
    }

    float lockDuration = 0f;
    Vector3 aim;
    Transform target = null;
    void AimAtCursor()
    {
        Vector3 mousePos = Input.mousePosition;
        target = GetPointedObjectTransform(mousePos);
        if(target != null)
        {
            lockDuration = autoLockDuration;
            aim = target.position;
        }
        else
            lockDuration -= Time.deltaTime;

        if (lockDuration <= 0f) 
            aim = mainCam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0.5f*mainCam.farClipPlane));

        barrelTranform.LookAt(aim, transform.up);
    }


    Transform GetPointedObjectTransform(Vector3 mousePosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 500, 511))
            if (hit.collider != null)
                return hit.collider.transform;
        return null;
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("ComboBox"))
        {
            var comboBox = collision.gameObject.GetComponentInParent<ComboBoxController>();
            var combo = comboBox.Combo;
            Debug.Log("Player: get combo: " + combo.GetType().ToString());
            comboBox.Deactivate();
            combo.ActivateCombo();
        }
    }
}
