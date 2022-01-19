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
