using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSoundManager : MonoBehaviour
{
    public AudioSource mAudioSrc;
    public AudioClip mWeaponShotSrc;
    public AudioClip mWeaponReloadSrc;

    [Range(0.05f, 0.20f)]
    public float pitchChangeMultiplier = 0.15f;

    void Start()
    {
        mAudioSrc = gameObject.AddComponent<AudioSource>();
    }

    // void Update()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         mAudioSrc.clip = mPistolShotSrc;
    //         mAudioSrc.pitch = Random.Range(1 - pitchChangeMultiplier, 1 + pitchChangeMultiplier);
    //         mAudioSrc.PlayOneShot(mAudioSrc.clip);
    //     }

    //     if (Input.GetKeyDown(KeyCode.R))
    //     {
    //         mAudioSrc.clip = mPistolReloadSrc;
    //         mAudioSrc.pitch = Random.Range(1 - pitchChangeMultiplier, 1 + pitchChangeMultiplier);
    //         mAudioSrc.PlayOneShot(mAudioSrc.clip);
    //     }
    // }

    public void PlayShotSound()
    {
        mAudioSrc.clip = mWeaponShotSrc;
        mAudioSrc.pitch = Random.Range(1 - pitchChangeMultiplier, 1 + pitchChangeMultiplier);
        mAudioSrc.PlayOneShot(mAudioSrc.clip);
    }

    public void PlayReloadSound()
    {
        mAudioSrc.clip = mWeaponReloadSrc;
        mAudioSrc.pitch = Random.Range(1 - pitchChangeMultiplier, 1 + pitchChangeMultiplier);
        mAudioSrc.PlayOneShot(mAudioSrc.clip);
    }
}
