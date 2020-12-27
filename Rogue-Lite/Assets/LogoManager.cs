using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LogoManager : MonoBehaviour
{
    [SerializeField] private VideoPlayer _video;

    private bool _isActivated = false;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadMainMenu", (float)_video.length);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            LoadMainMenu();
        }
    }

    void LoadMainMenu()
    {
        if (!_isActivated)
        {
            _isActivated = true;
            SceneManager.LoadSceneAsync(1);
        }
    }
}
