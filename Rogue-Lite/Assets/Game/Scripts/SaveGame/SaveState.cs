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

[System.Serializable]
public class SaveState
{
    public bool isTactile = false;
    public bool isDebug = false;
    public bool firstTimeLoaded = true;
    public float brightness = 0;
    public Language language = 0;
    public float canvasScale = 1f;
    
}
