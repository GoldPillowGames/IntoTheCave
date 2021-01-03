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

    [SerializeField] private GameObject _brightnessBackground;

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
            if (Application.isMobilePlatform)
            {
                #region UI
                Config.data.isTactile = true;
                Config.data.isDebug = true;
                Config.data.canvasScale = 1.0f;
                #endregion

                #region Graphics
                Config.data.brightness = 0;
                Config.data.language = Language.EN;
                Config.data.graphicsQuality = GraphicsQuality.LOW;

                #region Custom Settings
                Config.data.generalGraphics = GeneralGraphicsQuality.LOW;
                Config.data.shadowQuality = ShadowQualityState.NO_SHADOWS;
                Config.data.shadowDistance = ShadowDistanceState.CLOSE;
                Config.data.antialiasingQuality = AntialiasingState.NONE;
                Config.data.renderScale = 1.0f;
                #endregion

                Config.data.limitedFPS = FPSLimit.NONE;
                Config.data.vSync = false;
                #endregion
            }
            else
            {
                #region UI
                Config.data.isTactile = false;
                Config.data.isDebug = true;
                Config.data.canvasScale = 1.0f;
                #endregion

                #region Graphics
                Config.data.brightness = 0;
                Config.data.language = Language.EN;
                Config.data.graphicsQuality = GraphicsQuality.HIGH;

                #region Custom Settings
                Config.data.generalGraphics = GeneralGraphicsQuality.HIGH;
                Config.data.shadowQuality = ShadowQualityState.ULTRA;
                Config.data.shadowDistance = ShadowDistanceState.FAR;
                Config.data.antialiasingQuality = AntialiasingState.MSAAx2;
                Config.data.renderScale = 1.0f;
                #endregion

                Config.data.limitedFPS = FPSLimit.NONE;
                Config.data.vSync = false;
                #endregion
            }
            ShowMenuInstantly(3);
        }
        else
        {
            ShowMenuInstantly(0);
            // ShowMenu(MainMenuType.MAIN_MENU);
        }

        if (Config.data.vSync)
            QualitySettings.vSyncCount = 1;
        else
            QualitySettings.vSyncCount = 0;


    }

    private void Update()
    {
        if (menus[MainMenuType.BRIGHTNESS_MENU].activeSelf && !_brightnessBackground.activeSelf)
        {
            _brightnessBackground.SetActive(true);
        }else if(!menus[MainMenuType.BRIGHTNESS_MENU].activeSelf && _brightnessBackground.activeSelf)
        {
            _brightnessBackground.SetActive(false);
        }
    }

    public void OpenAd(string url)
    {
        Application.OpenURL(url);
    }

    public void ShowMenu(MainMenuType menu)
    {
        Fade.OnPlay = () =>
        {
            for (int i = 0; i < menus.Count; i++)
            {
                if (menus[(MainMenuType)i] == menus[menu])
                {
                    menus[(MainMenuType)i].SetActive(true);
                }
                else
                {
                    menus[(MainMenuType)i].SetActive(false);
                }
            }
        };
        Fade.PlayFade();
    }

    public void ShowMenu(int index)
    {
        ShowMenu((MainMenuType)index);
    }

    public void ShowMenuInstantly(int index)
    {
        MainMenuType menu = (MainMenuType)index;
        for (int i = 0; i < menus.Count; i++)
        {
            if (menus[(MainMenuType)i] == menus[menu])
            {
                menus[(MainMenuType)i].SetActive(true);
            }
            else
            {
                menus[(MainMenuType)i].SetActive(false);
            }
        }
    }
}
