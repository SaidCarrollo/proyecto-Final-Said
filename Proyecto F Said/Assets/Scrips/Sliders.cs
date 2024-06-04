using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Sliders : MonoBehaviour
{
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    private void Start()
    {
        float currentVolume;
        AudioManager.Instance.audioMixer.GetFloat(AudioManager.Instance.masterVolumeParameter, out currentVolume);
        masterVolumeSlider.value = currentVolume;

        AudioManager.Instance.audioMixer.GetFloat(AudioManager.Instance.musicVolumeParameter, out currentVolume);
        musicVolumeSlider.value = currentVolume;

        AudioManager.Instance.audioMixer.GetFloat(AudioManager.Instance.sfxVolumeParameter, out currentVolume);
        sfxVolumeSlider.value = currentVolume;
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
