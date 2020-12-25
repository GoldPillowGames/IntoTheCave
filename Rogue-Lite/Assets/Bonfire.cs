using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    [SerializeField]
    [Range(0, 1)] private float volume = 0.3f; 
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _clip;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("playSound", 6);
    }

    void playSound()
    {
        //Audio.Play3DSFX(_clip, 1, transform);
        Invoke("playSound", 6);
    }

    // Update is called once per frame
    void Update()
    {
        Audio.Update3DSFX(volume, transform, _audioSource);
    }
}
