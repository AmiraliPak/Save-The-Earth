using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectile
{
    [SerializeField] float moveSpeed;
    float MoveAmount { get => Time.fixedDeltaTime * moveSpeed; }
    Transform target;
    bool isLocked = false;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isLocked)
        {
            transform.LookAt(target);
            rb.MovePosition(rb.position + transform.forward * MoveAmount);
        }
    }

    public void LockOn(Transform target)
    {
        this.target = target;
        isLocked = true;
    }
}
