using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : Destructible, IProjectile, ISpawnable
{
    [SerializeField] float forwardSpeed, fallSpeed;
    float MoveForward { get => Time.fixedDeltaTime * forwardSpeed; }
    float MoveDownward { get => Time.fixedDeltaTime * fallSpeed; }
    [SerializeField] float _damage;
    public float Damage { get => _damage; }
    public float MinHeight { get => 70; }
    public float MaxHeight { get => 80; }
    Transform body;
    void Start()
    {
        body = transform.Find("Body");
    }
    void Update()
    {
        // change forward and fall speed for non-linear movement
    }
    void FixedUpdate()
    {
        body.Translate(new Vector3(0, -MoveDownward));
        transform.Rotate(new Vector3(MoveForward, 0, 0));
    }

    public override void OnDestruction()
    {
        Debug.Log("Meteor destroyed by shooting");
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
