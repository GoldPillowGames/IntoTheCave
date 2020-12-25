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
}
