using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGunEffects : MonoBehaviour, IWeaponEffects
{
    Animator m_Anim;
    RangedWeaponSoundManager m_RangedWeaponSoundManager;
    ParticleSystem m_ParticleSystem;

    private void Awake() 
    {
        m_Anim = GetComponent<Animator>();
        m_RangedWeaponSoundManager = GetComponent<RangedWeaponSoundManager>();
        m_ParticleSystem = GetComponent<ParticleSystem>();
    }

    public void PlayShootEffect()
    {
        m_Anim.SetTrigger("hasShot");
        m_RangedWeaponSoundManager.PlayShotSound();
        StartCoroutine(DelayedParticlePlay(0.2f));
    }

    public void PlayReloadEffect()
    {
        m_RangedWeaponSoundManager.PlayReloadSound();
    }

    IEnumerator DelayedParticlePlay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        m_ParticleSystem.Play();
    }
}