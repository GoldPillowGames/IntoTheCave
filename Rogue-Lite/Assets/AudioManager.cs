using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Variables
    public static AudioManager Instance;
    [SerializeField] private AudioSource[] _musicSources = new AudioSource[4];
    [SerializeField] private AudioSource _sfxSource;

    private Dictionary<AudioSource, float> _tracks;
    private float _currentPitch = 1;
    private float _currentSFXVolume = 1;
    private float _currentSFXPitch = 1;
    private int _currentTrackIndex = 0;
    #endregion

    #region Methods
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        _tracks = new Dictionary<AudioSource, float>();
        _tracks.Add(_musicSources[0], 1);
        _musicSources[0].Play();
        for (int i = 1; i < _musicSources.Length; i++)
        {
            _musicSources[i].Play();
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
        _sfxSource.PlayOneShot(clip, volume);
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
                //if (!_musicSources[i].isPlaying)
                //{
                //    _musicSources[i].Play();
                //}
            }

            if (!_musicSources[i].isPlaying)
            {
                _musicSources[i].Play();
            }

            if (tryToChangeSong && _musicSources[0].volume < 0.05f)
            {
                tryToChangeSong = false;
                _musicSources[i].volume = _tracks[_musicSources[i]];
            }
        }

        _currentTrackIndex = musicIndex;
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

        _currentTrackIndex = musicIndex;

        
    }

    AudioClip[] clips;

    public void ChangeTracks(List<AudioClip> clips)
    {
        for(int i = 0; i < _musicSources.Length; i++)
        {
            _musicSources[i].clip = null;
        }

        for (int i = 0; i < clips.Count; i++)
        {
            _musicSources[i].clip = clips[i];
            
        }

        tryToChangeSong = true;
        // _musicSources[_currentTrackIndex].Play();
        // ActivateDynamicTrack(_currentTrackIndex);
    }

    private bool tryToChangeSong = false;

    public void ChangeTracks(AudioClip[] clips)
    {
        
        this.clips = clips;
        tryToChangeSong = true;
        // _musicSources[_currentTrackIndex].Play();
        // ActivateDynamicTrack(_currentTrackIndex);
    }

    public void Destroy()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!tryToChangeSong)
        {
            for (int i = 0; i < _musicSources.Length; i++)
            {
                _musicSources[i].volume = Mathf.Lerp(_musicSources[i].volume, _tracks[_musicSources[i]], 2f * Time.unscaledDeltaTime);
                _musicSources[i].pitch = Mathf.Lerp(_musicSources[i].pitch, _currentPitch, 1 * Time.unscaledDeltaTime);
            }
        }
        

        if (tryToChangeSong)
        {

            for (int i = 0; i < _musicSources.Length; i++)
            {
                _musicSources[i].volume = Mathf.Lerp(_musicSources[i].volume, 0, 3f * Time.unscaledDeltaTime);
            }

            if(_musicSources[0].volume < 0.01f)
            {
                
                for (int i = 0; i < _musicSources.Length; i++)
                {
                    _musicSources[i].clip = null;
                }

                for (int i = 0; i < clips.Length; i++)
                {
                    _musicSources[i].clip = clips[i];
                    
                }

                ActivateDynamicTrack(_currentTrackIndex);
            }
        }

        //_sfxSource.volume = Mathf.Lerp(_sfxSource.volume, _currentSFXVolume, 2 * Time.unscaledDeltaTime);
        _sfxSource.pitch = Mathf.Lerp(_sfxSource.pitch, _currentSFXPitch, 2 * Time.unscaledDeltaTime);
    }
    #endregion
}
