using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAndSoundObjectManager : MonoBehaviour
{
    private AudioSource m_AudioSource;
    private ParticleSystem m_ParticleSystem;

    private void Awake() 
    {
        m_AudioSource = gameObject.GetComponent<AudioSource>();
        m_ParticleSystem = gameObject.GetComponent<ParticleSystem>();
    }

    public void UnparentAndPlayEffects()
    {
        gameObject.transform.parent = null;
        m_AudioSource.PlayOneShot(m_AudioSource.clip);
        m_ParticleSystem.Play();
        StartCoroutine(DisappearAfterTime(m_ParticleSystem.main.duration));
    }

    IEnumerator DisappearAfterTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        Destroy(gameObject);
    }
}