using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombo 
{
    void ActivateCombo();
}

public class EarthShieldCombo : ICombo
{
    public static GameObject EarthShield{ set; private get; }
    public void ActivateCombo()
    {
        EarthShield.SetActive(false);
        EarthShield.SetActive(true);
    }
}

public class HealthIncreaseCombo : ICombo
{
    static float healthIncreasePercent = 10f;
    public void ActivateCombo()
    {
        var pc = GameObject.Find("EarthMedium").GetComponent<PlanetController>();
        pc.AddLife(healthIncreasePercent * pc.MaxLife / 100f);
    }
}
