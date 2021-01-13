using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField] GameObject settingsMenu;

    [SerializeField] private Animator _firstTimeAnim;

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
        Fade.OnPlay = () => { 
            if(!Config.data.firstSinglePlayer || index != 2)
            {
                SceneManager.LoadScene(index);
            }
            else if (Config.data.firstSinglePlayer)
            {
                _firstTimeAnim.gameObject.SetActive(true);
                _firstTimeAnim.SetBool("Play", true);
                MenuManager menu = GetComponentInParent<MenuManager>();
                for (int i = 0; i < menu.gameObject.transform.childCount; i++)
                {
                    if(menu.transform.GetChild(i) != this)
                    {
                        Destroy(menu.transform.GetChild(i).gameObject);
                        
                    }
                }
                menu.enabled = false;
            }
            else if(index == 2)
            {
                SceneManager.LoadScene(2);
            }
        };
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
