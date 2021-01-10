using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    public AudioClip[] audios;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DeactivatePauseMenu();
        }
    }

    public void DeactivatePauseMenu()
    {
        FindObjectOfType<TimeManager>().timeScale = 1.0f;
        Time.timeScale = 1.0f;
        PlayerController[] players = FindObjectsOfType<PlayerController>();
        foreach (PlayerController player in players)
        {
            if (!Config.data.isOnline)
            {
                player.playerState = PlayerState.NEUTRAL;
            }
            else
            {
                if (player.isMe)
                {
                    player.playerState = PlayerState.NEUTRAL;
                }
            }
        }
        pauseMenu.SetActive(false);
    }

    public void ActivatePauseMenu()
    {
        PlayerController[] players = FindObjectsOfType<PlayerController>();
        foreach (PlayerController player in players)
        {
            if (!Config.data.isOnline)
            {
                player.playerState = PlayerState.PAUSE_MENU;
            }
            else
            {
                if (player.isMe)
                {
                    player.playerState = PlayerState.PAUSE_MENU;
                }
            }
        }
        pauseMenu.SetActive(true);
    }

    public void BackToMenu()
    {
        Audio.ChangeTracks(audios);
        Fade.SetTimeEffect(false);
        Fade.OnPlay = () =>
        {
            Time.timeScale = 1.0f;
            FindObjectOfType<GameManager>().DeleteAllDontDestroyOnLoad();
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            Fade.SetTimeEffect(true);
        };


        Fade.PlayFade();

    }
}
