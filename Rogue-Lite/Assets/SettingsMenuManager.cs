using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Michsky.UI.ModernUIPack;

public class SettingsMenuManager : MonoBehaviour
{
    [SerializeField] private HorizontalSelector _graphicsSettings;
    [SerializeField] private HorizontalSelector _generalGraphicsSettings;
    [SerializeField] private HorizontalSelector _shadowQuality;
    [SerializeField] private HorizontalSelector _antialiasingSettings;

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
    }

    // Update is called once per frame
    void Update()
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

    public void ExitSettingsMenu()
    {
        Config.SaveData();
        GetComponentInParent<MenuManager>().ShowMenu(0);
    }
}
