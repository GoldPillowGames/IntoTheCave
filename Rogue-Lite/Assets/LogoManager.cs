using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LogoManager : MonoBehaviour
{
    [SerializeField] private VideoPlayer _video;
    [SerializeField] private RawImage _rawImage;
    [SerializeField] private Texture _texture;
    [SerializeField] private GameObject _loadingIcon;
    [SerializeField] private GameObject _canvas;

    private bool _isActivated = false;
    private bool _logoPlaying = false;
    private bool _checkLogo = false;
    private bool _tryToPlayLogo = false;
    
    // Start is called before the first frame update
    void Start()
    {
        if (System.IO.File.Exists(Application.streamingAssetsPath + "/logo.mp4"))
            Debug.Log("Logo File Exists");
        _video.url = System.IO.Path.Combine(Application.streamingAssetsPath, "logo.mp4");
        _loadingIcon.SetActive(true);
        _video.Prepare();
        Invoke("PlayLogo", 3f);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            LoadMainMenu();
        }

        if (_checkLogo)
        {
            _checkLogo = false;
            LoadMainMenu();
        }

        if (_tryToPlayLogo)
        {
            _tryToPlayLogo = false;
            PlayLogo();
        }
    }

    void PlayLogo()
    {
        if (_video.isPrepared)
        {
            _tryToPlayLogo = false;
            _logoPlaying = true;
            _loadingIcon.SetActive(false);
            _video.Play();

            _rawImage.texture = _texture;
            _rawImage.color = Color.white;
            // Invoke("LoadMainMenu", (float)_video.length + 0.2f);
            Invoke("LoadMainMenu", (float)_video.length + 0.2f);
        }
        else
        {
            _tryToPlayLogo = true;
        }
        
    }

    void ShowLoadingIcon()
    {
        _loadingIcon.SetActive(true);
    }

    void LoadMainMenu()
    {
        if (!_isActivated && _logoPlaying && !_video.isPlaying)
        {
            _isActivated = true;
            ShowLoadingIcon();
            _video.Stop();
            SceneManager.LoadSceneAsync(1);
        }
        else if (_video.isPlaying)
        {
            _checkLogo = true;
        }
    }
}
