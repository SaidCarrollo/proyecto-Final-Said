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
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetMasterVolume(audioSettings.defaultMasterVolume);
        SetMusicVolume(audioSettings.defaultMusicVolume);
        SetSFXVolume(audioSettings.defaultSfxVolume);
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat(audioSettings.masterVolumeParameter, volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat(audioSettings.musicVolumeParameter, volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat(audioSettings.sfxVolumeParameter, volume);
    }
}
