using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Variables
    public AudioSource[] musicSources = new AudioSource[4];

    private Dictionary<AudioSource, float> _tracks;
    private float _currentPitch = 1;
    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        _tracks = new Dictionary<AudioSource, float>();
        _tracks.Add(musicSources[0], 1);
        for (int i = 1; i < musicSources.Length; i++)
        {
            _tracks.Add(musicSources[i], 0);
        }
    }

    public void SetPitch(float pitch)
    {
        _currentPitch = _currentPitch == 1 ? 0.9f : 1;
    }

    public void ActivateDynamicTrack(int musicIndex)
    {
        for(int i = 0; i < musicSources.Length; i++)
        {
            if(i <= musicIndex)
            {
                _tracks[musicSources[i]] = 1;
            }
        }
    }

    public void DeactivateDynamicTrack(int musicIndex)
    {
        _tracks[musicSources[musicIndex]] = 0;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < musicSources.Length; i++)
        {
            musicSources[i].volume = Mathf.Lerp(musicSources[i].volume, _tracks[musicSources[i]], 2 * Time.unscaledDeltaTime);
            musicSources[i].pitch = Mathf.Lerp(musicSources[i].pitch, _currentPitch, 2 * Time.unscaledDeltaTime);
        }
    }
    #endregion
}
