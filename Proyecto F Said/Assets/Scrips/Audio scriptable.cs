using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AudioSettings", menuName = "Audio/Audio Settings")]
public class AudioSettings : ScriptableObject
{
    public string masterVolumeParameter = "Master";
    public string musicVolumeParameter = "Music";
    public string sfxVolumeParameter = "Sfx";
    public float defaultMasterVolume = 0.75f;
    public float defaultMusicVolume = 0.75f;
    public float defaultSfxVolume = 0.75f;
}
