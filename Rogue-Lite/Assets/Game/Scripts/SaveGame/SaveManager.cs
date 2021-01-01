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
        Instance = this;

        Load();

        Debug.Log(Helper.Serialize<SaveState>(state));
    }

    public void Save()
    {
        PlayerPrefs.SetString("save", Helper.Serialize<SaveState>(state));
        print("<color=green>Saving Data...</color>");
    }

    private void Update()
    {
        if(Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("save"))
        {
            state = Helper.Deserialize<SaveState>(PlayerPrefs.GetString("save"));
            Debug.Log("Loading data...");
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
        Debug.Log("<color=red>Deleting Data...</color>");
        state = new SaveState();
        Save();
    }
}
