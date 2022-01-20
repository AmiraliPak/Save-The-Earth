using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombo 
{
    void ActivateCombo();
    string Name { get; set; }
}

public class EarthShieldCombo : ICombo
{
    public static GameObject EarthShield{ set; private get; }
    public string Name { get => "EarthShield"; set => Name = value; }

   
       

    public void ActivateCombo()
    {
        EarthShield.SetActive(false);
        EarthShield.SetActive(true);
    }
}

public class HealthIncreaseCombo : ICombo
{
    static float healthIncreasePercent = 10f;

    public string Name { get => "HealthIncrease"; set => Name = value; }
       
    

    public void ActivateCombo()
    {
        var pc = GameObject.Find("EarthMedium").GetComponent<PlanetController>();
        pc.AddLife(healthIncreasePercent * pc.MaxLife / 100f);
    }
}

public class TurboRefillCombo : ICombo
{

    public string Name { get => "TurboRefill"; set => Name = value; }
    
    

    public void ActivateCombo()
    {
        var pc = GameObject.Find("Player").GetComponent<PlayerController>();
        pc.RefillTurbo();
    }
}
