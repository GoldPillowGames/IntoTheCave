using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using ShadowResolution = UnityEngine.Rendering.Universal.ShadowResolution;


public class GameManager : MonoBehaviour
{
    [SerializeField] private UniversalRenderPipelineAsset[] _renderers;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        for(int i = 0; i < transform.childCount; i++)
        {
            DontDestroyOnLoad(transform.GetChild(i));
        }

        
    }

    private System.Type universalRenderPipelineAssetType;
    private FieldInfo mainLightShadowmapResolutionFieldInfo;

    private void Start()
    {
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
                Camera.main.GetComponent<UniversalAdditionalCameraData>().antialiasing = AntialiasingMode.None;
                break;
            case GraphicsQuality.MEDIUM:
                asset.msaaSampleCount = 0;
                Camera.main.GetComponent<UniversalAdditionalCameraData>().antialiasing = AntialiasingMode.FastApproximateAntialiasing;
                break;
            case GraphicsQuality.HIGH:
                Camera.main.GetComponent<UniversalAdditionalCameraData>().antialiasing = AntialiasingMode.None;
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
            if(Config.data.shadowQuality != ShadowQualityState.NO_SHADOWS && Config.data.shadowQuality != ShadowQualityState.ULTRA)
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
            else if(Config.data.shadowQuality == ShadowQualityState.VERY_HIGH || Config.data.shadowQuality == ShadowQualityState.HIGH || Config.data.shadowQuality == ShadowQualityState.MEDIUM)
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
                Camera.main.GetComponent<UniversalAdditionalCameraData>().antialiasing = AntialiasingMode.None;
            }
            else if (Config.data.antialiasingQuality == AntialiasingState.FXAA)
            {
                asset.msaaSampleCount = 0;
                Camera.main.GetComponent<UniversalAdditionalCameraData>().antialiasing = AntialiasingMode.FastApproximateAntialiasing;
            }
            else if(Config.data.antialiasingQuality == AntialiasingState.SMAA)
            {
                asset.msaaSampleCount = 0;
                Camera.main.GetComponent<UniversalAdditionalCameraData>().antialiasing = AntialiasingMode.SubpixelMorphologicalAntiAliasing;
                Camera.main.GetComponent<UniversalAdditionalCameraData>().antialiasingQuality = AntialiasingQuality.High;
            }
            else if (Config.data.antialiasingQuality == AntialiasingState.MSAAx2 || Config.data.antialiasingQuality == AntialiasingState.MSAAx4 || Config.data.antialiasingQuality == AntialiasingState.MSAAx8)
            {
                Camera.main.GetComponent<UniversalAdditionalCameraData>().antialiasing = AntialiasingMode.None;
                asset.msaaSampleCount = (int)Config.data.antialiasingQuality;
            }


            // Render Scale
            asset.renderScale = Config.data.renderScale;
        }

        // vSync
        if (Config.data.vSync)
            QualitySettings.vSyncCount = 1;
        else
            QualitySettings.vSyncCount = 0;

        // FPS Limit
        if (Config.data.limitedFPS != FPSLimit.NONE)
            Application.targetFrameRate = (int)Config.data.limitedFPS;

        
    }

}
