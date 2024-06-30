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
        masterVolumeSlider.value = PlayerPrefs.GetFloat(audioSettings.masterVolumeParameter, audioSettings.defaultMasterVolume);
        musicVolumeSlider.value = PlayerPrefs.GetFloat(audioSettings.musicVolumeParameter, audioSettings.defaultMusicVolume);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat(audioSettings.sfxVolumeParameter, audioSettings.defaultSfxVolume);
    }

    public void UpdateMasterVolume()
    {
        AudioManager.Instance.SetMasterVolume(masterVolumeSlider.value);
    }

    public void UpdateMusicVolume()
    {
        AudioManager.Instance.SetMusicVolume(musicVolumeSlider.value);
    }

    public void UpdateSFXVolume()
    {
        AudioManager.Instance.SetSFXVolume(sfxVolumeSlider.value);
    }
}
