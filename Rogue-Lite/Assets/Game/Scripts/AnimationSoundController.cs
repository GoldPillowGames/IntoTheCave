using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSoundController : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] attacks;

    void PlayAttackSound(int id)
    {
        // audioSource.PlayOneShot(attacks[id]);
        Audio.PlaySFX(attacks[id]);
    }
}
