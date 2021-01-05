using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Photon.Pun;
using ShadowResolution = UnityEngine.Rendering.Universal.ShadowResolution;
using AwesomeToon;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private UniversalRenderPipelineAsset[] _renderers;

    private void Awake()
    {
        if (Instance)
            Destroy(this);

        Instance = this;
        DontDestroyOnLoad(this);

        for(int i = 0; i < transform.childCount; i++)
        {
            DontDestroyOnLoad(transform.GetChild(i));
        }
    }

    public void EndRun()
    {
        Config.SaveData();
        DeleteAllDontDestroyOnLoad();
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    private System.Type universalRenderPipelineAssetType;
    private FieldInfo mainLightShadowmapResolutionFieldInfo;

    private void Start()
    {
        if (!Config.data.dynamicCelShadingEffect)
        {
            foreach (AwesomeToon.AwesomeToonHelper toonHelper in FindObjectsOfType<AwesomeToonHelper>())
            {
                toonHelper.update = false;
            }
        }
        
    }

    [PunRPC]
    public void DisconnectPlayer()
    {
        // PhotonNetwork.LeaveRoom();
        // DeleteAllDontDestroyOnLoad();
        FindObjectOfType<RunManager>().ResetOnSceneLoaded();
        PhotonNetwork.LeaveRoom();
        
        // StartCoroutine(DisconnectAndLoad());
    }

    IEnumerator DisconnectAndLoad()
    {
        
        while (PhotonNetwork.InRoom)
        {
            yield return null;
        }
        
        
    }

    public void DeleteAllDontDestroyOnLoad()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<RunManager>())
                transform.GetChild(i).GetComponent<RunManager>().ResetOnSceneLoaded();

            if (transform.GetChild(i).GetComponent<SaveManager>())
                transform.GetChild(i).transform.parent = null;
            else
                Destroy(transform.GetChild(i).gameObject);
        }
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject player in players)
        {
            Destroy(player);
        }

        foreach (EventTrigger trigger in FindObjectsOfType<EventTrigger>())
        {
            Destroy(trigger.gameObject);
        }

        Destroy(GameObject.FindGameObjectWithTag("Light"));

        //if (Config.data.isOnline)
        //{
        //    PhotonNetwork.Destroy(FindObjectOfType<OnlineRoomManager>().gameObject);
        //}

        Destroy(this.gameObject);
    }

    public void DeleteAllDontDestroyOnLoadAndLoadScene(int index)
    {
        foreach (AwesomeToonHelper a in FindObjectsOfType<AwesomeToonHelper>())
        {
            a.enabled = false;
        }

        foreach (ItemSpawner item in FindObjectsOfType<ItemSpawner>())
        {
            item.enabled = false;
            Destroy(item.gameObject);
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<RunManager>())
                transform.GetChild(i).GetComponent<RunManager>().ResetOnSceneLoaded();

            if (transform.GetChild(i).GetComponent<SaveManager>())
                transform.GetChild(i).transform.parent = null;
            else
                Destroy(transform.GetChild(i).gameObject);
        }

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            Destroy(player);
        }

        foreach (EventTrigger trigger in FindObjectsOfType<EventTrigger>())
        {
            Destroy(trigger.gameObject);
        }

        

        Destroy(GameObject.FindGameObjectWithTag("Light"));

        //if (Config.data.isOnline)
        //{
        //    PhotonNetwork.Destroy(FindObjectOfType<OnlineRoomManager>().gameObject);
        //}
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
        Destroy(this.gameObject);
    }

    public void LoadGraphicsSettings(Camera cam)
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

}
