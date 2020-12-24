using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource musicTrack1;
    public AudioSource musicTrack2;

    private float _currentVolume = 0;
    private float _currentPitch = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            _currentVolume = _currentVolume == 0 ? 1 : 0;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            _currentPitch = _currentPitch == 1 ? 0.9f : 1;
        }

        Time.timeScale = Mathf.Lerp(Time.timeScale, _currentPitch, 2 * Time.unscaledDeltaTime);

        musicTrack1.pitch = Time.timeScale;
        musicTrack2.pitch = Time.timeScale;
        musicTrack2.volume = Mathf.Lerp(musicTrack2.volume, _currentVolume, 2 * Time.unscaledDeltaTime);
    }
}
