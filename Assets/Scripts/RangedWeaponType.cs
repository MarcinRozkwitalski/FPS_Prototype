using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponType : MonoBehaviour
{
    [SerializeField]
    public enum Name
    {
        NormalBullet,
        Laser
    }

    public Name t_Name;
}