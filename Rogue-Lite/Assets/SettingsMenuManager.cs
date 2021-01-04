using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Michsky.UI.ModernUIPack;

public class SettingsMenuManager : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] private GameObject[] _menus;

    [SerializeField] private HorizontalSelector _languageSelector;
    [SerializeField] private HorizontalSelector _showFPSSelector;


    [Header("Pages / Graphics Settings")]
    [SerializeField] private GameObject[] _pages;
    [SerializeField] private HorizontalSelector _graphicsSettingsPage;

    [Header("Page 1")]
    [SerializeField] private HorizontalSelector _graphicsSettings;
    [SerializeField] private HorizontalSelector _generalGraphicsSettings;
    [SerializeField] private HorizontalSelector _shadowQuality;
    [SerializeField] private HorizontalSelector _antialiasingSettings;

    [Header("Page 2")]
    [SerializeField] private HorizontalSelector _uiScale;
    [SerializeField] private HorizontalSelector _vSync;
    [SerializeField] private HorizontalSelector _fpsSync;
    [SerializeField] private HorizontalSelector _renderScale;
    [SerializeField] private HorizontalSelector _limitFPS;

    [SerializeField] private GameObject _customSettings;

    
    // Start is called before the first frame update
    void Start()
    {
        _graphicsSettings.defaultIndex = (int)Config.data.graphicsQuality;
        _graphicsSettings.index = (int)Config.data.graphicsQuality;
        _graphicsSettings.UpdateUI();

        _generalGraphicsSettings.defaultIndex = (int)Config.data.generalGraphics;
        _generalGraphicsSettings.index = (int)Config.data.generalGraphics;
        _generalGraphicsSettings.UpdateUI();

        switch (Config.data.shadowQuality)
        {
            case ShadowQualityState.NO_SHADOWS:
                _shadowQuality.defaultIndex = 0;
                _shadowQuality.index = 0;
                break;
            case ShadowQualityState.VERY_LOW:
                _shadowQuality.defaultIndex = 1;
                _shadowQuality.index = 1;
                break;
            case ShadowQualityState.LOW:
                _shadowQuality.defaultIndex = 2;
                _shadowQuality.index = 2;
                break;
            case ShadowQualityState.MEDIUM:
                _shadowQuality.defaultIndex = 3;
                _shadowQuality.index = 3;
                break;
            case ShadowQualityState.HIGH:
                _shadowQuality.defaultIndex = 4;
                _shadowQuality.index = 4;
                break;
            case ShadowQualityState.VERY_HIGH:
                _shadowQuality.defaultIndex = 5;
                _shadowQuality.index = 5;
                break;
            case ShadowQualityState.ULTRA:
                _shadowQuality.defaultIndex = 6;
                _shadowQuality.index = 6;
                break;
            default:
                break;
        }
        _shadowQuality.UpdateUI();

        _antialiasingSettings.defaultIndex = (int)Config.data.antialiasingQuality;
        _antialiasingSettings.index = (int)Config.data.antialiasingQuality;
        _antialiasingSettings.UpdateUI();


        switch (Config.data.canvasScale)
        {
            case 0.8f:
                SetSelector(_uiScale, 0);
                break;
            case 0.85f:
                SetSelector(_uiScale, 1);
                break;
            case 0.9f:
                SetSelector(_uiScale, 2);
                break;
            case 0.95f:
                SetSelector(_uiScale, 3);
                break;
            case 1.00f:
                SetSelector(_uiScale, 4);
                break;
            case 1.05f:
                SetSelector(_uiScale, 5);
                break;
            case 1.10f:
                SetSelector(_uiScale, 6);
                break;
            case 1.15f:
                SetSelector(_uiScale, 7);
                break;
            case 1.20f:
                SetSelector(_uiScale, 8);
                break;
            default:
                break;
        }
        // SetSelector(_uiScale, (int)Config.data.canvasScale);
        switch (Config.data.vSync)
        {
            case false:
                SetSelector(_vSync, 0);
                break;
            case true:
                SetSelector(_vSync, 1);
                break;
        }

        switch (Config.data.limitedFPS)
        {
            case FPSLimit.NONE:
                SetSelector(_limitFPS, 0);
                break;
            case FPSLimit.VERY_LOW:
                SetSelector(_limitFPS, 1);
                break;
            case FPSLimit.LOW:
                SetSelector(_limitFPS, 2);
                break;
            case FPSLimit.MEDIUM:
                SetSelector(_limitFPS, 3);
                break;
            case FPSLimit.HIGH:
                SetSelector(_limitFPS, 4);
                break;
            case FPSLimit.VERY_HIGH:
                SetSelector(_limitFPS, 5);
                break;
            case FPSLimit.ULTRA:
                SetSelector(_limitFPS, 6);
                break;
            default:
                break;
        }

        //switch (Config.data.isDebug)
        //{
        //    case false:
        //        SetSelector(_showFPS, 0);
        //        break;
        //    case true:
        //        SetSelector(_showFPS, 1);
        //        break;
        //}

        switch (Config.data.renderScale)
        {
            case 0.8f:
                SetSelector(_renderScale, 0);
                break;
            case 0.85f:
                SetSelector(_renderScale, 1);
                break;
            case 0.9f:
                SetSelector(_renderScale, 2);
                break;
            case 0.95f:
                SetSelector(_renderScale, 3);
                break;
            case 1.00f:
                SetSelector(_renderScale, 4);
                break;
            case 1.05f:
                SetSelector(_renderScale, 5);
                break;
            case 1.10f:
                SetSelector(_renderScale, 6);
                break;
            case 1.15f:
                SetSelector(_renderScale, 7);
                break;
            case 1.20f:
                SetSelector(_renderScale, 8);
                break;
            default:
                break;
        }

        SetSelector(_languageSelector, (int)Config.data.language);
        

        switch (Config.data.isDebug)
        {
            case true:
                SetSelector(_showFPSSelector, 1);
                break;
            case false:
                SetSelector(_showFPSSelector, 0);
                break;
            default:
                break;
        }

        OpenSettingsMenu(0);
    }

    void SetSelector(HorizontalSelector selector, int index)
    {
        selector.defaultIndex = index;
        selector.index = index;
        selector.UpdateUI();
    }

    public void UpdateUIScale(float scale)
    {
        print("Updated UI Scale");
        Config.data.canvasScale = scale;
    }

    public void UpdateVSync(bool vSync)
    {
        Config.data.vSync = vSync;
    }

    public void UpdateFPSLimit(int limit)
    {
        Config.data.limitedFPS = (FPSLimit)limit;
    }

    public void UpdateRenderScale(float scale)
    {
        Config.data.renderScale = scale;
    }

    public void SetLanguage(int languageIndex)
    {
        Config.data.language = (Language)languageIndex;
    }

    public void ShowFPS(bool show)
    {
        print(show);
        Config.data.isDebug = show;
        // print(Config.data.isDebug);
    }

    // Update is called once per frame
    void Update()
    {



        if (!_pages[0].activeSelf)
            return;


        #region Graphics Settings
        if (_menus[1].activeSelf)
        {
            if (_graphicsSettings.index == 3)
            {
                //Image[] childrenImage = _customSettings.GetComponentsInChildren<Image>();
                //foreach(Image image in childrenImage){
                //    // image.raycastTarget = true;
                //    image.color = new Color(1, 1, 1, 1f);
                //}

                TextMeshProUGUI[] childrenText = _customSettings.GetComponentsInChildren<TextMeshProUGUI>();
                foreach (TextMeshProUGUI text in childrenText)
                {
                    text.color = new Color(1, 1, 1, 1f);
                    text.faceColor = new Color(1, 1, 1, 1f);
                }

                HorizontalSelector[] childrenSelector = _customSettings.GetComponentsInChildren<HorizontalSelector>();
                foreach (HorizontalSelector children in childrenSelector)
                {
                    children.enabled = true;
                    //Image[] childrenImage = children.GetComponentsInChildren<Image>();
                    //foreach (Image image in childrenImage)
                    //{
                    //    image.color = new Color(1, 1, 1, 0.01f);
                    //}
                }
                Button[] childrenButtons = _customSettings.GetComponentsInChildren<Button>();
                foreach (Button button in childrenButtons)
                {
                    button.interactable = true;
                }
            }
            else
            {
                HorizontalSelector[] childrenSelector = _customSettings.GetComponentsInChildren<HorizontalSelector>();
                foreach (HorizontalSelector children in childrenSelector)
                {
                    children.enabled = false;
                    Image[] childrenImage = children.GetComponentsInChildren<Image>();
                    //foreach (Image image in childrenImage)
                    //{
                    //    image.color = new Color(1, 1, 1, 0.01f);
                    //}
                }

                TextMeshProUGUI[] childrenText = _customSettings.GetComponentsInChildren<TextMeshProUGUI>();
                foreach (TextMeshProUGUI text in childrenText)
                {
                    text.color = new Color(1, 1, 1, 0.25f);
                    text.faceColor = new Color(1, 1, 1, 0.25f);
                }

                Button[] childrenButtons = _customSettings.GetComponentsInChildren<Button>();
                foreach (Button button in childrenButtons)
                {
                    button.interactable = false;
                }
            }
        }
        #endregion
    }

    public void ShowGraphicsPage(int index)
    {
        for(int i = 0; i < _pages.Length; i++)
        {
            if(index == i)
            {
                _pages[i].SetActive(true);
            }
            else
            {
                _pages[i].SetActive(false);
            }
        }
    }

    public void UpdateGraphicsSettings(int index)
    {
        Config.data.graphicsQuality = (GraphicsQuality)index;
    }

    public void UpdateGeneralSettings(int index)
    {
        Config.data.generalGraphics = (GeneralGraphicsQuality) index;
    }

    public void UpdateShadowQuality(int index)
    {
        switch (index)
        {
            case 0:
                Config.data.shadowQuality = ShadowQualityState.NO_SHADOWS;
                break;
            case 1:
                Config.data.shadowQuality = ShadowQualityState.VERY_LOW;
                break;
            case 2:
                Config.data.shadowQuality = ShadowQualityState.LOW;
                break;
            case 3:
                Config.data.shadowQuality = ShadowQualityState.MEDIUM;
                break;
            case 4:
                Config.data.shadowQuality = ShadowQualityState.HIGH;
                break;
            case 5:
                Config.data.shadowQuality = ShadowQualityState.VERY_HIGH;
                break;
            case 6:
                Config.data.shadowQuality = ShadowQualityState.ULTRA;
                break;
            default:

                break;
        }
    }

    public void UpdateAntialiasing(int index)
    {
        switch (index)
        {
            case 0:
                Config.data.antialiasingQuality = AntialiasingState.NONE;
                break;
            case 1:
                Config.data.antialiasingQuality = AntialiasingState.FXAA;
                break;
            case 2:
                Config.data.antialiasingQuality = AntialiasingState.SMAA;
                break;
            case 3:
                Config.data.antialiasingQuality = AntialiasingState.MSAAx2;
                break;
            case 4:
                Config.data.antialiasingQuality = AntialiasingState.MSAAx4;
                break;
            case 5:
                Config.data.antialiasingQuality = AntialiasingState.MSAAx8;
                break;
            default:
                break;
        }
    }

    public void OpenSettingsMenu(int index)
    {
        for(int i = 0; i < _menus.Length; i++)
        {
            if(i == index)
            {
                _menus[i].SetActive(true);
            }
            else
            {
                _menus[i].SetActive(false);
            }
        }
    }

    public void ExitSettingsMenu()
    {
        Config.SaveData();
        GetComponentInParent<MenuManager>().ShowMenu(0);
    }
}
