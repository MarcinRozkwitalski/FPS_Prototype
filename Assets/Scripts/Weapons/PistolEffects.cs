using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolEffects : MonoBehaviour, IWeaponEffects
{
    Animator m_Anim;
    RangedWeaponSoundManager m_RangedWeaponSoundManager;
    ParticleSystem m_ParticleSystem;

    [SerializeField]
    GameObject m_PistolSlide;

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
        StartCoroutine(DelayedParticlePlay(0.05f));
    }

    public void PlayReloadEffect()
    {
        m_Anim.SetTrigger("hasReloaded");
        StartCoroutine(DelayedAdditional01SoundPlay((float)30/60));
        StartCoroutine(DelayedAdditional02SoundPlay((float)128/60));
        StartCoroutine(DelayedReloadSoundPlay((float)247/60));
    }

    IEnumerator DelayedParticlePlay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        m_ParticleSystem.Play();
    }

    IEnumerator DelayedAdditional01SoundPlay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        m_RangedWeaponSoundManager.PlayAdditional01Sound();
        gameObject.GetComponent<ProjectileGun>().bulletsLeft = 0;
    }

    IEnumerator DelayedAdditional02SoundPlay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        m_RangedWeaponSoundManager.PlayAdditional02Sound();
    }

    IEnumerator DelayedReloadSoundPlay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        m_RangedWeaponSoundManager.PlayReloadSound();
    }
}