using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MainMenuType
{
    MAIN_MENU = 0,
    SETTINGS_MENU = 1,
    BRIGHTNESS_MENU = 2,
    LANGUAGE_MENU = 3
}

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _brightnessMenu;
    [SerializeField] private GameObject _languageMenu;
    [SerializeField] private GameObject _settingsMenu;

    private Dictionary<MainMenuType, GameObject> menus;

    // Start is called before the first frame update
    void Start()
    {
        // Debug
        Config.ResetData();

        menus = new Dictionary<MainMenuType, GameObject>();

        menus.Add(MainMenuType.MAIN_MENU, _mainMenu);
        menus.Add(MainMenuType.BRIGHTNESS_MENU, _brightnessMenu);
        menus.Add(MainMenuType.LANGUAGE_MENU, _languageMenu);
        menus.Add(MainMenuType.SETTINGS_MENU, _settingsMenu);

        if (Config.data.firstTimeLoaded)
        {
            ShowMenu(MainMenuType.LANGUAGE_MENU);
        }
        else
        {
            ShowMenu(MainMenuType.MAIN_MENU);
        }
    }

    public void ShowMenu(MainMenuType menu)
    {
        for(int i = 0; i < menus.Count; i++)
        {
            if(menus[(MainMenuType)i] == menus[menu])
            {
                menus[(MainMenuType)i].SetActive(true);
            }
            else
            {
                menus[(MainMenuType)i].SetActive(false);
            }
        }
    }
    public void ShowMenu(int index)
    {
        ShowMenu((MainMenuType) index);
    }
}
