using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    [SerializeField] AudioSource laserSource;
    [SerializeField] AudioSource meteorSource;
    [SerializeField] AudioClip fallClip;
    [SerializeField] AudioClip contactClip;
    [SerializeField] AudioSource explosionSource;
    [SerializeField] AudioSource teleportSource;
    [SerializeField] AudioSource countdownSource;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioClip musicLoop;

    private void Update()
    {
        if(!musicSource.isPlaying)
        {
            musicSource.clip = musicLoop;
            musicSource.Play();
        }
    }

    //Specific
    public void PlayLaserSoundEffect()
    {
        PlayIfNotPlaying(laserSource);
    }

    public void PlayCountdownEffect()
    {
        countdownSource.Play();
    }

    public void StopLaserSoundEffect()
    {
        StopIfPlaying(laserSource);
    }

    public void PlayMeteorFallEffect()
    {
        meteorSource.clip = fallClip;
        meteorSource.Play();
    }

    public void PlayMeteorContactEffect()
    {
        meteorSource.clip = contactClip;
        meteorSource.Play();
    }

    public void PlayExplosionEffect()
    {
        explosionSource.Play();
    }

    public void PlayTeleportEffect()
    {
        PlayIfNotPlaying(teleportSource);
    }

    //General Purpose Functions
    private void PlayIfNotPlaying(AudioSource source)
    {
        if(!source.isPlaying)
        {
            source.Play();
        }
    }

    private void StopIfPlaying(AudioSource source)
    {
        if(source.isPlaying)
        {
            source.Stop();
        }
    }
}
