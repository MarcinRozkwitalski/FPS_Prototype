using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAndSoundObjectManager : MonoBehaviour
{
    private AudioSource m_AudioSource;
    private ParticleSystem m_ParticleSystem;

    [Range(0.05f, 0.25f)]
    public float pitchChangeMultiplier;

    [SerializeField]
    private float m_TimeLeftUntilDestroying;

    private void Awake() 
    {
        if (gameObject.GetComponent<AudioSource>() != null)
            m_AudioSource = gameObject.GetComponent<AudioSource>();
        
        if (gameObject.GetComponent<ParticleSystem>() != null)
            m_ParticleSystem = gameObject.GetComponent<ParticleSystem>();
    }

    public void UnparentAndPlayEffects()
    {
        gameObject.transform.parent = null;
        
        if (m_AudioSource != null)
        {
            m_AudioSource.pitch = Random.Range(1 - pitchChangeMultiplier, 1 + pitchChangeMultiplier);
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
        }
            
        if(m_ParticleSystem != null)
            m_ParticleSystem.Play();

        if(m_ParticleSystem.gameObject.transform.childCount == 1)
            m_ParticleSystem.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>().Play();

        Destroy(gameObject, m_TimeLeftUntilDestroying);
    }
}