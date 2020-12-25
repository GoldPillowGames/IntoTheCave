using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenuManager : MonoBehaviour
{
    [SerializeField] private AudioClip _deathClip;

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
        Audio.PlaySFX(_deathClip);
    }
}
