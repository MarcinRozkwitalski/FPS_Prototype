using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthObject : MonoBehaviour
{
    [SerializeField]
    public int m_HealthPoints;

    public void CheckHealthZero()
    {
        if(m_HealthPoints <= 0)
            gameObject.GetComponent<DamageableObject>().m_OnZeroHealth.Invoke();
    }
}