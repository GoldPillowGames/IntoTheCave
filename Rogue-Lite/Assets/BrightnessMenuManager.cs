using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class BrightnessMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _timeline;
    [SerializeField] private Slider _brightnessSlider;
    private TextMeshProUGUI _brightnessText;
    private MenuManager _menuManager;
    private string _originalText;

    // Start is called before the first frame update
    void Start()
    {
        
        _brightnessSlider.value = Config.data.brightness;
        _brightnessText = _brightnessSlider.GetComponentInChildren<TextMeshProUGUI>();
        _menuManager = GetComponentInParent<MenuManager>();
    }

    // Update is called once per frame
    void Update()
    {
        _originalText = TableReader.GetString("Menus/MainMenu", 1, 3);
        PostProcess.SetPostExposure(_brightnessSlider.value);
        float tmpBrightnessValue = Mathf.Round(_brightnessSlider.value * 100f) / 100f + 2f;
        _brightnessText.text = _originalText + ": " + tmpBrightnessValue.ToString();
    }

    public void SaveBrightness()
    {
        Config.data.brightness = _brightnessSlider.value;
        if(Config.data.firstTimeLoaded)
        {
            Config.data.firstTimeLoaded = false;
            
        }
        _timeline.GetComponent<PlayableDirector>().Play();
        // _timeline.GetComponent<PlayableDirector>().Evaluate();

        Config.SaveData();
        _menuManager.ShowMenu(MainMenuType.MAIN_MENU);
    }
}
