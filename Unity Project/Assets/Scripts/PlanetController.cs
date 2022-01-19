using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : Destructible
{
    public HealthBar healthBar;
    void Start()
    {
        healthBar.SetMaxValue(this.life);
    }
    public override void OnDestruction()
    {
        Debug.Log("Planet Destroyed! GAME OVER");
        // emit event ?
        EventSystemCustom.Instance.OnGameOver.Invoke();
        // UnityEditor.EditorApplication.isPlaying = false;
    }

    public override void TakeDamage()
    {
        healthBar.SetValue(this.life);
    }
}
