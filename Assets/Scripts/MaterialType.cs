using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialType : MonoBehaviour
{
    [SerializeField]
    public enum Name
    {
        Metal,
        Wood,
        Glass
    }

    public Name t_Name;
}