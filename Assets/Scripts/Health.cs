using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    public int m_HealthPoints;

    [SerializeField]
    TMP_Text m_ObjectInfoText;

    [SerializeField]
    UnityEvent m_OnZeroHealth;

    private void Awake()
    {
        UpdateText();
    }

    public void ReceiveDamage(int damage)
    {
        m_HealthPoints -= damage;
        UpdateText();
        if(m_HealthPoints <= 0)
        {
            m_OnZeroHealth.Invoke();
        }
    }

    public void SelfDestroy()
    {
        Destroy(gameObject);
    }

    public void UpdateText()
    {
        MaterialType materialType = gameObject.GetComponent<MaterialType>(); 
        m_ObjectInfoText.text = materialType.t_Name + "\n" + m_HealthPoints;
    }
}