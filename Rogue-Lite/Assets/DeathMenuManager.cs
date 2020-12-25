using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Febucci.UI;
using System;

public class DeathMenuManager : MonoBehaviour
{
    [SerializeField] private AudioClip _deathClip;
    [SerializeField] private TextAnimatorPlayer _textAnimatorPlayer;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayDeathMenu()
    {
        if (!animator)
            animator = GetComponent<Animator>();
        Audio.DeactivateTrack(0);

        animator.SetTrigger("DeathMenuIsActivated");
    }

    public void PlayRandomDeathSound()
    {
        _textAnimatorPlayer.ShowText("From my deepest dreams, a light reflects the dawn of a new day.");
        Audio.PlaySFX(_deathClip);
    }

    public void ReturnToLive()
    {
        PlayerController[] players = FindObjectsOfType<PlayerController>();
        foreach(PlayerController player in players)
        {
            player.health = player.maxHealth;
        }
        _textAnimatorPlayer.ShowText("");
        GetComponentInParent<UIController>()._playerIsDead = false;
        Invoke("StartMusic", 0.3f);
    }

    private void StartMusic()
    {
        Audio.ActivateTrack(0);
    }
}
