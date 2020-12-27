using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SaveType
{
    Test,
    Release
}
[System.Serializable]
public class SaveState
{
    public bool isTactile = false;
    public bool isDebug = false;
    public float gamma = 1;
    public float language = 0;
    public float canvasScale = 1f;
}
