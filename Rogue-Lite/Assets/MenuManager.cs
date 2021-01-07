using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Photon.Pun;
using ShadowResolution = UnityEngine.Rendering.Universal.ShadowResolution;
using AwesomeToon;

public enum MainMenuType
{
    MAIN_MENU = 0,
    SETTINGS_MENU = 1,
    BRIGHTNESS_MENU = 2,
    LANGUAGE_MENU = 3,
    CREDITS_MENU = 4
}

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _brightnessMenu;
    [SerializeField] private GameObject _languageMenu;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _creditsMenu;

    [SerializeField] private GameObject _brightnessBackground;

    [SerializeField] private Animator _creditsAnimator;

    [SerializeField] private UniversalRenderPipelineAsset[] _renderers;

    private Dictionary<MainMenuType, GameObject> menus;

    // Start is called before the first frame update
    void Start()
    {
        // Debug
        Config.SaveData();

        menus = new Dictionary<MainMenuType, GameObject>();

        menus.Add(MainMenuType.MAIN_MENU, _mainMenu);
        menus.Add(MainMenuType.BRIGHTNESS_MENU, _brightnessMenu);
        menus.Add(MainMenuType.LANGUAGE_MENU, _languageMenu);
        menus.Add(MainMenuType.SETTINGS_MENU, _settingsMenu);
        menus.Add(MainMenuType.CREDITS_MENU, _creditsMenu);

        if (Config.data.firstTimeLoaded)
        {
            if (Application.isMobilePlatform)
            {
                #region UI
                Config.data.isTactile = true;
                Config.data.isDebug = false;
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
                Config.data.dynamicCelShadingEffect = false;
                #endregion

                Config.data.limitedFPS = FPSLimit.NONE;
                Config.data.vSync = false;
                #endregion
            }
            else
            {
                #region UI
                Config.data.isTactile = false;
                Config.data.isDebug = false;
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
                Config.data.dynamicCelShadingEffect = true;
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

        LoadGraphicsSettings(Camera.main);

        if (!Config.data.dynamicCelShadingEffect)
        {
            foreach (AwesomeToon.AwesomeToonHelper toonHelper in FindObjectsOfType<AwesomeToonHelper>())
            {
                toonHelper.update = false;
            }
        }
        
    }

    private System.Type universalRenderPipelineAssetType;
    private FieldInfo mainLightShadowmapResolutionFieldInfo;

    public void LoadGraphicsSettings(Camera cam)
    {
        print("Load Graphics");

        // General
        QualitySettings.SetQualityLevel((int)Config.data.graphicsQuality, true);

        // Graphics Settings
        GraphicsSettings.renderPipelineAsset = _renderers[(int)Config.data.graphicsQuality];
        UniversalRenderPipelineAsset asset = (UniversalRenderPipelineAsset)GraphicsSettings.currentRenderPipeline;

        // Automatic Antialiasing
        switch (Config.data.graphicsQuality)
        {
            case GraphicsQuality.LOW:
                asset.msaaSampleCount = 0;
                cam.GetComponent<UniversalAdditionalCameraData>().antialiasing = AntialiasingMode.None;
                break;
            case GraphicsQuality.MEDIUM:
                asset.msaaSampleCount = 0;
                cam.GetComponent<UniversalAdditionalCameraData>().antialiasing = AntialiasingMode.FastApproximateAntialiasing;
                break;
            case GraphicsQuality.HIGH:
                cam.GetComponent<UniversalAdditionalCameraData>().antialiasing = AntialiasingMode.None;
                asset.msaaSampleCount = 4;
                break;
            default:
                break;
        }

        // Custom Settings
        if (Config.data.graphicsQuality == GraphicsQuality.CUSTOM)
        {
            switch (Config.data.generalGraphics)
            {
                case GeneralGraphicsQuality.HIGH:
                    asset.supportsHDR = true;
                    break;
                case GeneralGraphicsQuality.MEDIUM:
                    asset.supportsHDR = false;
                    break;
                case GeneralGraphicsQuality.LOW:
                    asset.supportsHDR = false;
                    break;
                default:
                    break;
            }


            // Shadow Quality
            universalRenderPipelineAssetType = (GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset).GetType();
            mainLightShadowmapResolutionFieldInfo = universalRenderPipelineAssetType.GetField("m_MainLightShadowmapResolution", BindingFlags.Instance | BindingFlags.NonPublic);
            if (Config.data.shadowQuality != ShadowQualityState.NO_SHADOWS && Config.data.shadowQuality != ShadowQualityState.ULTRA)
                mainLightShadowmapResolutionFieldInfo.SetValue(GraphicsSettings.currentRenderPipeline, (int)Config.data.shadowQuality);
            else if (Config.data.shadowQuality == ShadowQualityState.ULTRA)
            {
                mainLightShadowmapResolutionFieldInfo.SetValue(GraphicsSettings.currentRenderPipeline, 4096);
            }
            else
                mainLightShadowmapResolutionFieldInfo.SetValue(GraphicsSettings.currentRenderPipeline, 256);


            // Shadow Cascades
            if (Config.data.shadowQuality == ShadowQualityState.ULTRA)
            {
                asset.shadowCascadeOption = ShadowCascadesOption.FourCascades;
            }
            else if (Config.data.shadowQuality == ShadowQualityState.VERY_HIGH || Config.data.shadowQuality == ShadowQualityState.HIGH || Config.data.shadowQuality == ShadowQualityState.MEDIUM)
            {
                asset.shadowCascadeOption = ShadowCascadesOption.TwoCascades;
            }
            else
            {
                asset.shadowCascadeOption = ShadowCascadesOption.NoCascades;
            }


            // Shadow Distance
            if (Config.data.shadowQuality != ShadowQualityState.NO_SHADOWS)
                asset.shadowDistance = (int)Config.data.shadowDistance;
            else
                asset.shadowDistance = 0;


            // Antialiasing
            if (Config.data.antialiasingQuality == AntialiasingState.NONE)
            {
                asset.msaaSampleCount = 0;
                cam.GetComponent<UniversalAdditionalCameraData>().antialiasing = AntialiasingMode.None;
            }
            else if (Config.data.antialiasingQuality == AntialiasingState.FXAA)
            {
                asset.msaaSampleCount = 0;
                cam.GetComponent<UniversalAdditionalCameraData>().antialiasing = AntialiasingMode.FastApproximateAntialiasing;
            }
            else if (Config.data.antialiasingQuality == AntialiasingState.SMAA)
            {
                asset.msaaSampleCount = 0;
                cam.GetComponent<UniversalAdditionalCameraData>().antialiasing = AntialiasingMode.SubpixelMorphologicalAntiAliasing;
                cam.GetComponent<UniversalAdditionalCameraData>().antialiasingQuality = AntialiasingQuality.High;
            }
            else if (Config.data.antialiasingQuality == AntialiasingState.MSAAx2 || Config.data.antialiasingQuality == AntialiasingState.MSAAx4 || Config.data.antialiasingQuality == AntialiasingState.MSAAx8)
            {
                cam.GetComponent<UniversalAdditionalCameraData>().antialiasing = AntialiasingMode.None;
                asset.msaaSampleCount = (int)Config.data.antialiasingQuality;
            }
        }

        // Render Scale
        asset.renderScale = Config.data.renderScale;

        // vSync
        if (Config.data.vSync)
            QualitySettings.vSyncCount = 1;
        else
            QualitySettings.vSyncCount = 0;

        // FPS Limit
        if (Config.data.limitedFPS != FPSLimit.NONE)
            Application.targetFrameRate = (int)Config.data.limitedFPS;
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
        Application.ExternalEval("window.open('" + url + "', '_blank')");
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

                    if (menu == MainMenuType.CREDITS_MENU)
                    {
                        print(menu);
                        _creditsAnimator.SetBool("IsPlaying", true);
                    }
                    else
                    {
                        print(menu);
                        _creditsAnimator.SetBool("IsPlaying", false);
                    }
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
