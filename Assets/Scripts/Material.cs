using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Material : MonoBehaviour
{
    [SerializeField]
    public enum Type
    {
        Metal,
        Wood,
        Glass
    }

    public Type materialType;
}