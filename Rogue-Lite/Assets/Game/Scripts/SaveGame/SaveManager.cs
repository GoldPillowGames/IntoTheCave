using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { set; get; }
    public SaveType saveType;
    [HideInInspector] public SaveState state;

    private void Awake()
    {
        // DontDestroyOnLoad(gameObject);
        Instance = this;

        Load();

        Debug.Log(Helper.Serialize<SaveState>(state));
        Debug.Log("Level: " + state.level + ", " + "Current EXP: " + state.levelProgress);
    }

    public void Save()
    {
        PlayerPrefs.SetString("save", Helper.Serialize<SaveState>(state));
        print("Data saved...");
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("save"))
        {
            state = Helper.Deserialize<SaveState>(PlayerPrefs.GetString("save"));
        }
        else
        {
            state = new SaveState();
            Save();
            Debug.Log("No save file found, creating...");
        }
    }

    public void ResetData()
    {
        state = new SaveState();
        Save();
    }
}
