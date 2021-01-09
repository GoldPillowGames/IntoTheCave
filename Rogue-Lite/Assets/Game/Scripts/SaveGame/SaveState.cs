using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SaveType
{
    Test,
    Release
}

public enum Language
{
    EN = 0,
    ES = 1,
    DE = 2
}

public enum GraphicsQuality
{
    LOW = 0,
    MEDIUM = 1,
    HIGH = 2,
    CUSTOM = 3
}

public enum GeneralGraphicsQuality
{
    LOW = 0,
    MEDIUM = 1,
    HIGH = 2,
}

public enum ShadowQualityState
{
    NO_SHADOWS = 0,
    VERY_LOW = 256,
    LOW = 512,
    MEDIUM = 1024,
    HIGH = 2048,
    VERY_HIGH = 4096,
    ULTRA = 4097
}

public enum ShadowDistanceState
{
    CLOSE = 25,
    MEDIUM = 50,
    FAR = 100,
}

public enum FPSLimit
{
    NONE = 0,
    VERY_LOW = 15,
    LOW = 30,
    MEDIUM = 45,
    HIGH = 60,
    VERY_HIGH = 75,
    ULTRA = 120,
    INSANE = 240
}

public enum AntialiasingState
{
    NONE = 0,
    FXAA = 1,
    SMAA = 3,
    MSAAx2 = 2,
    MSAAx4 = 4,
    MSAAx8 = 8
}

[System.Serializable]
public class SaveState
{
    public bool firstTimeLoaded = true;

    #region UI
    public bool isTactile = false;
    public bool isDebug = false;
    public float canvasScale = 1.0f;
    #endregion

    #region Graphics
    public float brightness = 0;
    public Language language = Language.EN;
    public GraphicsQuality graphicsQuality = GraphicsQuality.HIGH;

    #region Custom Settings
    public GeneralGraphicsQuality generalGraphics = GeneralGraphicsQuality.HIGH;
    public ShadowQualityState shadowQuality = ShadowQualityState.ULTRA;
    public ShadowDistanceState shadowDistance = ShadowDistanceState.FAR;
    public AntialiasingState antialiasingQuality = AntialiasingState.MSAAx2;
    public float renderScale = 1.0f;
    public bool dynamicCelShadingEffect = true;
    #endregion

    public FPSLimit limitedFPS = FPSLimit.NONE;
    public bool vSync = false;
    public bool isOnline = false;
    #endregion

    public int gold = 0;

    public int herreroDialogueIndex = 1;
    public int armeroDialogueIndex = 1;
    public int tintoneroDialogueIndex = 1;

    public bool firstSinglePlayer = true;

    public float masterVolume;
    public float musicVolume;
    public float sfxVolume;

    public int dungeonsStarted = 0;
    public int dungeonsCompleted = 0;

    public int hpLevel = 0;
    public int strengthLevel = 0;
    public int dungeonLevel = 1;
    public int playerLevel => hpLevel + strengthLevel + 1;
}
