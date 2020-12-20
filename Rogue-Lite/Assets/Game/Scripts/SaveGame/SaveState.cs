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
    public int level = 1;
    public int expToLevelUp = 100;
    public int levelProgress = 0;
    public SaveType saveType = SaveType.Test;
    public int coins = 0;
    public bool firstTime = true;
    public float currentTimeToDisplayAd = 200f;
}
