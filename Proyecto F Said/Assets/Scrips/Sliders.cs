using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Sliders : MonoBehaviour
{
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    public AudioSettings audioSettings; 

    private void Start()
    {
        masterVolumeSlider.value = audioSettings.defaultMasterVolume;
        musicVolumeSlider.value = audioSettings.defaultMusicVolume;
        sfxVolumeSlider.value = audioSettings.defaultSfxVolume;
    }

    public void UpdateMasterVolume()
    {
        AudioManager.Instance.SetMasterVolume(masterVolumeSlider.value);
    }

    public void UpdateMusicVolume()
    {
        AudioManager.Instance.SetMusicVolume(musicVolumeSlider.value);
    }

    public void UpdateMenuVolume()
    {
        AudioManager.Instance.SetSFXVolume(sfxVolumeSlider.value);
    }
}
