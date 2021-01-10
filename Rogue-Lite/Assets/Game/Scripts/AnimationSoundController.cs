using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AnimationSoundController : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] attacks;
    [SerializeField] AudioClip[] steps;

    void PlayAttackSound(int id)
    {
        // audioSource.PlayOneShot(attacks[id]);
        if(GetComponentInParent<PlayerController>().isMe && Config.data.isOnline)
            Audio.PlaySFX(attacks[id]);
        else if(!Config.data.isOnline)
            Audio.PlaySFX(attacks[id]);
    }

    void PlayStepSound()
    {
        if (GetComponentInParent<PlayerController>().isMe && Config.data.isOnline)
            Audio.PlaySFX(steps[Random.Range(0, steps.Length)], 1.4f);
        else if (!Config.data.isOnline)
            Audio.PlaySFX(steps[Random.Range(0, steps.Length)], 1.4f);
    }
}
