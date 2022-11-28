using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown graphicsDropdown;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    
    //Musics
    [SerializeField] private Slider volumeSlider;
    public AudioMixer audioMixer;
    
    //FX
    [SerializeField] private Slider volumeSliderFX;
    public AudioMixer audioMixerFX;

    Resolution[] resolutions;
    
    //Show FPS
    [SerializeField] private Canvas fpsCanvas;
    [SerializeField] private Toggle fpsToggle;
    
    private void Start()
    {

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        
        var currentResolutionIndex = 0;
        List<string> options = resolutions.Select(t => t.width + " x " + t.height).ToList();
        
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void Update()
    {
        graphicsDropdown.value = QualitySettings.GetQualityLevel();
        graphicsDropdown.RefreshShownValue();
        
        resolutionDropdown.RefreshShownValue();
    }

    public void SetFps()
    {
        fpsCanvas.enabled = fpsToggle.isOn;
    }
    
    public void SetResolution()
    {
        Resolution resolution = resolutions[resolutionDropdown.value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    
    public void SetVolume()
    {
        audioMixer.SetFloat("Volumes", volumeSlider.value);
    }
    
    public void SetVolumeFX()
    {
        audioMixerFX.SetFloat("VolumesFX", volumeSliderFX.value);
    }
    
    /// <summary>
    /// Sync the volume of the main menu with the volume of escape menu
    /// </summary>
    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("Volumes", volumeSlider.value);
        PlayerPrefs.SetFloat("VolumesFX", volumeSliderFX.value);
        PlayerPrefs.SetInt("Quality", graphicsDropdown.value);
        PlayerPrefs.SetInt("Resolution", resolutionDropdown.value);
        PlayerPrefs.SetInt("Fps", fpsToggle.isOn ? 1 : 0);
        PlayerPrefs.Save();
    }
    
    /// <summary>
    /// Sync the volume of the main menu with the volume of escape menu
    /// </summary>
    public void LoadSettings()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volumes");
        volumeSliderFX.value = PlayerPrefs.GetFloat("VolumesFX");
        graphicsDropdown.value = PlayerPrefs.GetInt("Quality");
        resolutionDropdown.value = PlayerPrefs.GetInt("Resolution");
        fpsToggle.isOn = PlayerPrefs.GetInt("Fps") == 0 ? false : true;
    }

    public void SetFullScreen()
    { 
        Screen.fullScreen = true;
    }
    
    public void SetWindowed()
    {
        Screen.fullScreen = false;
    }
    
    public void SetQuality()
    {
        QualitySettings.SetQualityLevel(graphicsDropdown.value);
    }
    
}
