using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PistolEffects : MonoBehaviour, IWeaponEffects
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
        StartCoroutine(DelayedParticlePlay(0.05f));
    }

    public void PlayReloadEffect(TextMeshProUGUI textMeshProUGUI)
    {
        m_Anim.SetTrigger("hasReloaded");
        StartCoroutine(DelayedAdditional01SoundPlay((float)30/60, textMeshProUGUI));
        StartCoroutine(DelayedAdditional02SoundPlay((float)128/60));
        StartCoroutine(DelayedReloadSoundPlay((float)247/60));
    }

    IEnumerator DelayedParticlePlay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        m_ParticleSystem.Play();
    }

    IEnumerator DelayedAdditional01SoundPlay(float seconds, TextMeshProUGUI textMeshProUGUI)
    {
        yield return new WaitForSeconds(seconds);
        m_RangedWeaponSoundManager.PlayAdditional01Sound();
        ProjectileGun projectileGun = gameObject.GetComponent<ProjectileGun>();
        projectileGun.bulletsLeft = 0;
        textMeshProUGUI.SetText(projectileGun.bulletsLeft / projectileGun.bulletsPerTap + " / " + projectileGun.magazineSize / projectileGun.bulletsPerTap);
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