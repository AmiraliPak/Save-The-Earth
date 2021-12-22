using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float shootPower;
    [SerializeField] float shootRate;
    [SerializeField] float damage;
    static public float Damage { get; private set; }
    float MoveAmount { get => Time.deltaTime * moveSpeed; }
    float RotationAmount { get => Time.deltaTime * rotationSpeed; }
    Camera mainCam;
    Transform weapon, body;
    public GameObject bulletPrefab;
    IEnumerator shootCoroutine = null;

    void Awake()
    {
        Damage = damage;
    }
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
        var horizontalMovement = Input.GetAxis("Horizontal") * RotationAmount;
        // var rotationMovement = Input.GetAxis("Mouse X") * RotationAmount;
        transform.Rotate(new Vector3(-verticalMovement, horizontalMovement));
    }

    void HandleShoot()
    {
        if(Input.GetMouseButtonDown(0))
        {
            shootCoroutine = ShootCoroutine();
            StartCoroutine(shootCoroutine);
        }
        if(Input.GetMouseButtonUp(0))
        {
            StopCoroutine(shootCoroutine);
        }
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
        var bulletPos = weapon.position + 2*weapon.forward;
        var bullet = GameObject.Instantiate(bulletPrefab, bulletPos, Quaternion.identity);
        var bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(weapon.forward * shootPower, ForceMode.Impulse);
    }

    IEnumerator ShootCoroutine()
    {
        while(true)
        {
            ShootWeapon();
            yield return new WaitForSeconds(1f / shootRate);
        }
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
