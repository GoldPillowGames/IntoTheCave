using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Config
{
    public static SaveState data => GameObject.FindObjectOfType<SaveManager>().state;

    public static void SaveGame()
    {
        SaveManager saveManager = GameObject.FindObjectOfType<SaveManager>();
        if (saveManager)
            saveManager.Save();
        else
            Debug.LogError("No SaveManager Detected!");
    }
}
