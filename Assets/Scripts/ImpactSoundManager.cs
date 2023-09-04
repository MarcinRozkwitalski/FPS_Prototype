using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactSoundManager : MonoBehaviour
{
    [SerializeField]
    public AudioSource m_AudioSrc;
    
    [SerializeField]
    public AudioClip m_BulletImpactSoundSrc;

    [Range(0.05f, 0.25f)]
    public float pitchChangeMultiplier = 0.25f;

    [Range(0.01f, 1f)]
    public float volumeChangeMultiplier = 0.08f;

    private void Awake()
    {
        PlayImpactSound();
    }

    public void PlayImpactSound()
    {
        m_AudioSrc.clip = m_BulletImpactSoundSrc;
        m_AudioSrc.pitch = Random.Range(1 - pitchChangeMultiplier, 1 + pitchChangeMultiplier);
        m_AudioSrc.PlayOneShot(m_AudioSrc.clip, volumeChangeMultiplier);
    }
}