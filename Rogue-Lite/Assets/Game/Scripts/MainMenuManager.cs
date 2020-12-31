using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField] GameObject settingsMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel(int index)
    {
        Fade.OnPlay = () => { SceneManager.LoadScene(index); };
        Fade.PlayFade();
    }

    public void SetOnlineMode(bool online)
    {
        Config.data.isOnline = online;
        Config.SaveData();
    }

    public void ShowSettingsMenu()
    {
        settingsMenu.SetActive(true);
    }
}
