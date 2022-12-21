using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{

    [SerializeField] private TMPro.TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMPro.TMP_Dropdown graphicsDropdown;
    [SerializeField] private Toggle fsToggle;
    [SerializeField] private AudioMixer audioMixer;

    private Resolution[] resolutions;
    
    private void Start()
    {
        fsToggle.isOn = Screen.fullScreen;
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        graphicsDropdown.value = QualitySettings.GetQualityLevel();
        graphicsDropdown.RefreshShownValue();

        List<String> options = new List<string>();

        var currentResIndex = 0;
        
        for (int i = 0; i < resolutions.Length; i++)
        {
            options.Add( resolutions[i].width + "x" + resolutions[i].height);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int index)
    {
        Screen.SetResolution(resolutions[index].width, resolutions[index].height, Screen.fullScreen);
    }

    public void SetFullscreen(bool isFs)
    {
        Screen.fullScreen = isFs;
    }

    public void setGraphics(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void SetMusicVolume(float val)
    {
        audioMixer.SetFloat("MusicVol", val);
    }

    public void SetSoundVolume(float val)
    {
        audioMixer.SetFloat("SoundsVol", val);
    }
    
}
