using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class DamageableObject : MonoBehaviour, IDamageable
{
    [SerializeField]
    public UnityEvent m_OnZeroHealth;

    private MaterialType m_MaterialType;
    private HealthObject m_HealthObject;

    [SerializeField]
    private TMP_Text m_ObjectTextInfo;

    private void Awake()
    {
        m_MaterialType = gameObject.GetComponent<MaterialType>(); 
        m_HealthObject = gameObject.GetComponent<HealthObject>();

        if (m_ObjectTextInfo == null)
            m_ObjectTextInfo = gameObject.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        else
            Debug.LogError("Text for " + gameObject.name + " not found!");
    }

    private void Start()
    {
        UpdateText();        
    }

    public void TakeDamage(int damageAmount)
    {
        m_HealthObject.m_HealthPoints -= damageAmount;
        UpdateText();
        m_HealthObject.CheckHealthZero();
    }

    public void UpdateText()
    {
        m_ObjectTextInfo.text = m_MaterialType.t_Name + "\n" + m_HealthObject.m_HealthPoints;
    }

    public void SelfDestroy()
    {
        Destroy(gameObject);
    }    
}