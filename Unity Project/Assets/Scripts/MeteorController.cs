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
    public float MinHeight { get => 180; }
    public float MaxHeight { get => 200; }
    Transform body, visuals;
    Vector3 forwardVector, downwardVector;
    void Start()
    {
        body = transform.Find("Body");
        transform.Rotate(0, UnityEngine.Random.Range(0, 360), 0);
        forwardVector = new Vector3(1f, 0, 0);
        downwardVector = new Vector3(0, -1f);
    }
    void FixedUpdate()
    {
        body.Translate(downwardVector * MoveDownward);
        transform.Rotate(forwardVector * MoveForward);
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
