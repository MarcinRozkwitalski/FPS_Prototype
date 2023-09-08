using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParticleManager : MonoBehaviour
{
    private ParticleSystem mParticleSystem;

    private void Start()
    {
        mParticleSystem = GetComponent<ParticleSystem>();
    }

    public void PlayParticle()
    {
        mParticleSystem.Play();
    }
}
