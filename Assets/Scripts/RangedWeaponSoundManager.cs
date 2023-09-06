using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class RangedWeaponSoundManager : MonoBehaviour
{
    public AudioSource m_AudioSrc;
    public AudioClip m_WeaponShotSrc;
    public AudioClip m_WeaponReloadSrc;

    [Range(0.05f, 0.20f)]
    public float pitchChangeMultiplier = 0.15f;

    [Range(0.01f, 1f)]
    public float volumeChangeMultiplier = 0.4f;

    private void Awake()
    {
        m_AudioSrc = gameObject.GetComponent<AudioSource>();
    }

    public void PlayShotSound()
    {
        m_AudioSrc.clip = m_WeaponShotSrc;
        m_AudioSrc.pitch = Random.Range(1 - pitchChangeMultiplier, 1 + pitchChangeMultiplier);
        m_AudioSrc.PlayOneShot(m_AudioSrc.clip, volumeChangeMultiplier);
    }

    public void PlayReloadSound()
    {
        m_AudioSrc.clip = m_WeaponReloadSrc;
        m_AudioSrc.pitch = Random.Range(1 - pitchChangeMultiplier, 1 + pitchChangeMultiplier);
        m_AudioSrc.PlayOneShot(m_AudioSrc.clip, volumeChangeMultiplier);
    }
}
