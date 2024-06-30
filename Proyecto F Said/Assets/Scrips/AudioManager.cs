using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioMixer audioMixer;
    public AudioSettings audioSettings;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadAudioSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetMasterVolume(PlayerPrefs.GetFloat(audioSettings.masterVolumeParameter, audioSettings.defaultMasterVolume));
        SetMusicVolume(PlayerPrefs.GetFloat(audioSettings.musicVolumeParameter, audioSettings.defaultMusicVolume));
        SetSFXVolume(PlayerPrefs.GetFloat(audioSettings.sfxVolumeParameter, audioSettings.defaultSfxVolume));
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat(audioSettings.masterVolumeParameter, volume);
        PlayerPrefs.SetFloat(audioSettings.masterVolumeParameter, volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat(audioSettings.musicVolumeParameter, volume);
        PlayerPrefs.SetFloat(audioSettings.musicVolumeParameter, volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat(audioSettings.sfxVolumeParameter, volume);
        PlayerPrefs.SetFloat(audioSettings.sfxVolumeParameter, volume);
    }

    private void LoadAudioSettings()
    {
        if (PlayerPrefs.HasKey(audioSettings.masterVolumeParameter))
        {
            SetMasterVolume(PlayerPrefs.GetFloat(audioSettings.masterVolumeParameter));
        }

        if (PlayerPrefs.HasKey(audioSettings.musicVolumeParameter))
        {
            SetMusicVolume(PlayerPrefs.GetFloat(audioSettings.musicVolumeParameter));
        }

        if (PlayerPrefs.HasKey(audioSettings.sfxVolumeParameter))
        {
            SetSFXVolume(PlayerPrefs.GetFloat(audioSettings.sfxVolumeParameter));
        }
    }
}
