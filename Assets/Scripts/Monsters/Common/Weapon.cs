using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float Range;

    internal event Action WeaponHit;

    public void TriggerWeaponHit()
    {
        if (WeaponHit != null)
            WeaponHit();        
    }
}
