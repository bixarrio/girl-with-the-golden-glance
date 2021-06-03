using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Startup : MonoBehaviour
{
    #region Properties and Fields

    [SerializeField] AudioData[] _audioData;

    #endregion

    #region Unity Methods

    private void Start() => LoadAudioSettings();

    #endregion

    #region Private Methods

    private void LoadAudioSettings()
    {
        foreach (var data in _audioData)
            LoadAudioSetting(data);
    }

    private void LoadAudioSetting(AudioData data)
    {
        var volume = PlayerPrefs.GetFloat(data.GroupVolumeParameter, -5f);
        if (volume < 0f) return;

        data.MixerGroup.audioMixer.SetFloat(data.GroupVolumeParameter, Mathf.Log10(volume) * 20f);
    }

    #endregion
}

[System.Serializable]
public class AudioData
{
    public AudioMixerGroup MixerGroup;
    public string GroupVolumeParameter;
}
