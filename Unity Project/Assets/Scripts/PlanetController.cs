using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : Destructible
{
    public override void OnDestruction()
    {
        Debug.Log("Planet Destroyed! GAME OVER");
        // emit event ?
        EventSystemCustom.Instance.OnGameOver.Invoke();
        // UnityEditor.EditorApplication.isPlaying = false;
    }
}
