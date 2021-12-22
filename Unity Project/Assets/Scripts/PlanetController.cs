using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    [SerializeField] float life;
    float spaceshipDamage, playerDamage;
    void Start()
    {
        spaceshipDamage = PlayerController.Damage;
        playerDamage = PlayerController.Damage;
    }

    // Update is called once per frame
    void Update()
    {
        CheckLife();
    }

    private void CheckLife()
    {
        if (life <= 0f)
        {
            Debug.Log("Planet life zero: Game Over");
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Projectile_Spaceship"))
        {
            Debug.Log("Projectile_Spaceship hit");
            life -= spaceshipDamage;
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.CompareTag("Projectile_Player"))
        {
            Debug.Log("Projectile_Spaceship hit");
            life -= playerDamage;
            Destroy(collision.gameObject);
        }
    }
}
