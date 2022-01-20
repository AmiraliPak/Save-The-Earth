using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    List<(GameObject,GameObject)> targets = new List<(GameObject,GameObject)>(); // (real target, corresponding indicator target) 
    [SerializeField] GameObject indicatorTargetPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            var (rt, it) = targets[i];
            if(!rt.activeInHierarchy)
            {
                it.SetActive(false);
                targets.RemoveAt(i);
                i--;
                continue;
            }
            UpdateIndicator(rt, it);
        }
    }

    void UpdateIndicator(GameObject rt, GameObject it)
    {
        it.transform.LookAt(rt.transform);
        it.transform.rotation *= new Quaternion(1f, 0f, 0f, 0f);
    }

    bool TryAddTarget(GameObject target)
    {
        if(!target.CompareTag("SpaceShip") || !target.activeInHierarchy)
            return false;
        var it = Instantiate(indicatorTargetPrefab, transform);
        targets.Add((target, it));
        return true;
    }

    (float, float) GetObjectRotation(GameObject obj) => (obj.transform.rotation.x, obj.transform.rotation.z);
}
