using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponSoundManager : MonoBehaviour
{
    public AudioSource audioSrc;

    [SerializeField]
    private AudioClip m_WeaponSwingSrc;

    [Range(0.05f, 0.20f)]
    public float pitchChangeMultiplier = 0.15f;

    [Range(0.01f, 1f)]
    public float volumeChangeMultiplier = 0.4f;

    private void Awake()
    {
        audioSrc = gameObject.GetComponent<AudioSource>();
    }

    public IEnumerator PlaySwingSound(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        audioSrc.clip = m_WeaponSwingSrc;
        audioSrc.pitch = Random.Range(1 - pitchChangeMultiplier, 1 + pitchChangeMultiplier);
        audioSrc.volume = volumeChangeMultiplier;
        audioSrc.Play();
    }

    public void StopSwingSound()
    {
        audioSrc.Stop();
    }
}