using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BrightnessMenuManager : MonoBehaviour
{
    [SerializeField] private Slider brightnessSlider;
    private TextMeshProUGUI _brightnessText;
    private MenuManager _menuManager;

    // Start is called before the first frame update
    void Start()
    {
        _brightnessText = brightnessSlider.GetComponentInChildren<TextMeshProUGUI>();
        _menuManager = GetComponentInParent<MenuManager>();
    }

    // Update is called once per frame
    void Update()
    {
        PostProcess.SetPostExposure(brightnessSlider.value);
        float tmpBrightnessValue = Mathf.Round(brightnessSlider.value * 100f) / 100f + 2f;
        _brightnessText.text = tmpBrightnessValue.ToString();
    }

    public void SaveBrightness()
    {
        Config.data.brightness = brightnessSlider.value;
        Config.data.firstTimeLoaded = false;
        Config.SaveData();
        _menuManager.ShowMenu(MainMenuType.MAIN_MENU);
    }
}
