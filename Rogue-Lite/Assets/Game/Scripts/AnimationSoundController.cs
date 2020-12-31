using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AnimationSoundController : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] attacks;

    void PlayAttackSound(int id)
    {
        // audioSource.PlayOneShot(attacks[id]);
        if(GetComponentInParent<PlayerController>().isMe && Config.data.isOnline)
            Audio.PlaySFX(attacks[id]);
        else if(!Config.data.isOnline)
            Audio.PlaySFX(attacks[id]);
    }
}
