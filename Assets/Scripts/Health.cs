using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    public int healthPoints;

    [SerializeField]
    UnityEvent onZeroHealth;

    public void ReceiveDamage(int damage)
    {
        healthPoints -= damage;
        if(healthPoints <= 0)
        {
            onZeroHealth.Invoke();
        }
    }

    public void SelfDestroy()
    {
        StartCoroutine(DelayedDestroy());
        
    }

    IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }
}