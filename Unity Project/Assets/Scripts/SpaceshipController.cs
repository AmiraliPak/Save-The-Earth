using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : Destructible
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float shootPower;
    [SerializeField] float shootRate;
    float MoveAmount { get => Time.deltaTime * moveSpeed; }
    float RotationAmount { get => Time.deltaTime * rotationSpeed; }
    public GameObject bulletPrefab;
    Transform body;

    void Start()
    {
        body = transform.Find("Body");
        StartCoroutine(ShootCoroutine());
        StartCoroutine(RandGenCoroutine());
    }

    void Update()
    {
        RandomMove();
    }

    float rand1, rand2, rand3;
    private void RandomMove()
    {
        var verticalMovement = rand1 * MoveAmount;
        var horizontalMovement = rand2 * MoveAmount;
        var rotationMovement = rand3 * RotationAmount;
        transform.Rotate(new Vector3(-verticalMovement, rotationMovement, horizontalMovement));
    }

    void ShootWeapon()
    {
        var bulletPos = body.position - body.up;
        var bullet = GameObject.Instantiate(bulletPrefab, bulletPos, Quaternion.identity);
        var bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(-body.up * shootPower, ForceMode.Impulse);
    }

    IEnumerator ShootCoroutine()
    {
        while(true)
        {
            ShootWeapon();
            yield return new WaitForSeconds(1f / shootRate);
        }
    }
    
    IEnumerator RandGenCoroutine()
    {
        while(true)
        {
            rand1 = UnityEngine.Random.Range(-1f, 1f);
            rand2 = UnityEngine.Random.Range(-1f, 1f);
            rand3 = UnityEngine.Random.Range(-1f, 1f);
            yield return new WaitForSeconds(4f);
        }
    }

    public override void OnDestruction()
    {
        
    }
}
