using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Audio
{
    public static void PlaySFX(AudioClip clip)
    {
        AudioManager manager = GameObject.FindObjectOfType<AudioManager>();
        if(manager)
            manager.PlaySFX(clip);
    }

    public static void PlaySFX(AudioClip clip, float volume)
    {
        AudioManager manager = GameObject.FindObjectOfType<AudioManager>();
        if (manager)
            manager.PlaySFX(clip, volume);
    }

    public static void Play3DSFX(AudioClip clip, float volume, Transform soundOrigin)
    {
        AudioManager manager = GameObject.FindObjectOfType<AudioManager>();
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        if (!player)
            return;
        float distance = Mathf.Clamp((soundOrigin.position - player.position).magnitude/1.2f, 1, 100000);
        Debug.Log("Sound distance: <color=orange>" + distance + "</color>");
        float calculatedVolume = 1 / distance * volume;
        Debug.Log("CalculatedVolume: <color=orange>" + calculatedVolume + "</color>");

        if (manager)
            manager.PlaySFX(clip, calculatedVolume);
    }

    public static void Update3DSFX(float volume, Transform soundOrigin, AudioSource audioSource)
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        if (!player)
            return;
        float distance = Mathf.Clamp((soundOrigin.position - player.position).magnitude/1.2f, 1, 100000);
        Debug.Log("Sound distance: <color=orange>" + distance + "</color>");
        float calculatedVolume = 1 / distance * volume;
        Debug.Log("CalculatedVolume: <color=orange>" + calculatedVolume + "</color>");

        audioSource.volume = calculatedVolume;
    }

    public static void ActivateTrack(int trackIndex)
    {
        AudioManager manager = GameObject.FindObjectOfType<AudioManager>();
        if (manager)
            manager.ActivateDynamicTrack(trackIndex);
    }

    public static void DeactivateTrack(int trackIndex)
    {
        AudioManager manager = GameObject.FindObjectOfType<AudioManager>();
        if (manager)
            manager.DeactivateDynamicTrack(trackIndex);
    }
}
