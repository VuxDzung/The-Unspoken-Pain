using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
public class UISettingsView : MonoBehaviour
{
    #region SERIALIZE_FIELDS
    [SerializeField] private AudioMixer effectMixer;
    [SerializeField] private AudioMixer BGMMixer;
    [SerializeField] private Slider effectSlider;
    [SerializeField] private Slider BGMSlider;
    [SerializeField] private TMP_Dropdown resolutionDropDown;
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Toggle isFullScreen;
    [SerializeField] private Button saveButton;
    #endregion
    private Resolution[] resolutions;
    #region UNITY_METHOD
    private void Awake()
    {
        if (resolutionDropDown != null)
            LoadResolution();

        if (saveButton != null)
            saveButton.onClick.AddListener(SaveSettings);

        //Load previous settings
        SettingsData settings = new SettingsData();
        settings = SaveLoadSystem.LoadSettings();
        if (settings != null)
        {
            effectSlider.value = settings.effectSliderValue;
            BGMSlider.value = settings.BGMSliderValue;
            resolutionDropDown.value = settings.resolutionDropDownValue;
            qualityDropdown.value = settings.qualityDropdownValue;
        }
    }

    #endregion
    #region Settings Methods
    public void SetEffectVolume(float volume) => effectMixer.SetFloat("volume_effect", volume);

    public void SetBGMVolume(float volume) => BGMMixer.SetFloat("volumeBGM", volume);

    public void SetQuality(int qualityIndex) => QualitySettings.SetQualityLevel(qualityIndex);

    public void SetFullScreen(bool isFullScreen) => Screen.fullScreen = isFullScreen;

    public void SetResolution(int resolutionIndex)
    {
        try
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
        catch
        {
            Debug.LogError("SetResolution");
        }
    }

    public void LoadResolution()
    {
        resolutions = Screen.resolutions;
        resolutionDropDown.ClearOptions();
        List<string> options = new List<string>();

        foreach (var resolution in resolutions)
        {
            string option = string.Format("{0} x {1}", resolution.width, resolution.height);
            options.Add(option);
        }

        resolutionDropDown.AddOptions(options);
    }

    public void SaveSettings()
    {
        SettingsData settings = new SettingsData();
        settings.effectSliderValue = effectSlider.value;
        settings.BGMSliderValue=  BGMSlider.value;
        settings.resolutionDropDownValue = resolutionDropDown.value;
        settings.qualityDropdownValue = qualityDropdown.value;

        SaveLoadSystem.SaveSettings(settings);
    }
    #endregion
}

//model of settings data
[System.Serializable]
public class SettingsData
{
    public float effectSliderValue;
    public float BGMSliderValue;
    public int resolutionDropDownValue;
    public int qualityDropdownValue;
    public bool isFullScreen;
}

