using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Config
{
    public static SaveState data => GameObject.FindObjectOfType<SaveManager>().state;

    public static void SaveData()
    {
        SaveManager saveManager = GameObject.FindObjectOfType<SaveManager>();
        if (saveManager)
            saveManager.Save();
        else
            Debug.LogError("No SaveManager Detected!");
    }

    public static void ResetData()
    {
        SaveManager saveManager = GameObject.FindObjectOfType<SaveManager>();
        if (saveManager)
            saveManager.ResetData();
        else
            Debug.LogError("No SaveManager Detected!");
    }
}
