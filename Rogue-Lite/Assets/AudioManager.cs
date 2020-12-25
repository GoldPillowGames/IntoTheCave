using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private AudioSource[] _musicSources = new AudioSource[4];
    [SerializeField] private AudioSource _sfxSource;

    private Dictionary<AudioSource, float> _tracks;
    private float _currentPitch = 1;
    private float _currentSFXVolume = 1;
    private float _currentSFXPitch = 1;
    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        _tracks = new Dictionary<AudioSource, float>();
        _tracks.Add(_musicSources[0], 1);
        for (int i = 1; i < _musicSources.Length; i++)
        {
            _tracks.Add(_musicSources[i], 0);
        }
        _currentSFXVolume = _sfxSource.volume;
        _currentSFXPitch = _sfxSource.pitch;
    }

    public void PlaySFX(AudioClip clip)
    {
        _sfxSource.volume = 1;
        _sfxSource.PlayOneShot(clip);
    }

    public void PlaySFX(AudioClip clip, float volume)
    {
        _sfxSource.volume = volume;
        _sfxSource.PlayOneShot(clip);
    }

    public void SetPitch(float pitch)
    {
        _currentPitch = _currentPitch == 1 ? 0.9f : 1;
    }

    public void ActivateDynamicTrack(int musicIndex)
    {
        for(int i = 0; i < _musicSources.Length; i++)
        {
            if(i <= musicIndex)
            {
                _tracks[_musicSources[i]] = 1;
            }
        }
    }

    public void DeactivateDynamicTrack(int musicIndex)
    {
        for (int i = _musicSources.Length - 1; i >= 0; i--)
        {
            if (i >= musicIndex)
            {
                _tracks[_musicSources[i]] = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < _musicSources.Length; i++)
        {
            _musicSources[i].volume = Mathf.Lerp(_musicSources[i].volume, _tracks[_musicSources[i]], 2 * Time.unscaledDeltaTime);
            _musicSources[i].pitch = Mathf.Lerp(_musicSources[i].pitch, _currentPitch, 2 * Time.unscaledDeltaTime);
        }

        //_sfxSource.volume = Mathf.Lerp(_sfxSource.volume, _currentSFXVolume, 2 * Time.unscaledDeltaTime);
        _sfxSource.pitch = Mathf.Lerp(_sfxSource.pitch, _currentSFXPitch, 2 * Time.unscaledDeltaTime);
    }
    #endregion
}
