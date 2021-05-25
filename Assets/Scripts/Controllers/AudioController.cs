using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    #region Properties and Fields

    private static AudioController _instance;
    public static AudioController Instance => _instance;

    [SerializeField] AudioMixerGroup _musicMixerGroup;
    [SerializeField] AudioMixerGroup _ambienceMixerGroup;
    [SerializeField] AudioMixerGroup _narrativeMixerGroup;
    [SerializeField] AudioMixerGroup _sfxMixerGroup;

    [SerializeField] int _sfxSourceInstances = 5;

    private AudioSource _musicSource;
    private AudioSource _ambienceSource;
    private AudioSource _narrativeSource;
    private Queue<AudioSource> _sfxSources = new Queue<AudioSource>();

    #endregion

    #region Unity Methods

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start() => Init();
    private void OnEnable() => HookMessages();
    private void OnDisable() => UnhookMessages();

    #endregion

    #region Private Methods

    private void HookMessages()
    {
        Messaging<PlayAudio>.Register(PlayAudio);
        Messaging<StopAudio>.Register(StopAudio);
    }
    private void UnhookMessages()
    {
        Messaging<PlayAudio>.Unregister(PlayAudio);
        Messaging<StopAudio>.Unregister(StopAudio);
    }

    private void Init()
    {
        _musicSource = CreateAudioSource("MusicAudioSource", _musicMixerGroup, 0f, true);
        _ambienceSource = CreateAudioSource("AmbienceAudioSource", _ambienceMixerGroup, 0f, true);
        _narrativeSource = CreateAudioSource("NarrativeAudioSource", _narrativeMixerGroup, 0f, false);
        for (int i = 0; i < _sfxSourceInstances; i++)
            _sfxSources.Enqueue(CreateAudioSource($"SFXAudioSource{i:00}", _sfxMixerGroup, 1f, false));
    }

    private AudioSource CreateAudioSource(string name, AudioMixerGroup group, float spatialBlend, bool loop)
    {
        var audioSource = new GameObject(name).AddComponent<AudioSource>();

        audioSource.outputAudioMixerGroup = group;
        audioSource.spatialBlend = spatialBlend;
        audioSource.loop = loop;

        audioSource.transform.SetParent(transform);

        return audioSource;
    }

    private void PlayAudio(AudioClip audioClip, AudioGroups group, Transform audioLocation = null)
    {
        // This could be done better
        switch (group)
        {
            case AudioGroups.SFX: PlaySFX(audioClip, audioLocation); break;
            case AudioGroups.Music: PlayMusic(audioClip); break;
            case AudioGroups.Ambience: PlayAmbience(audioClip); break;
            case AudioGroups.Narrative: PlayNarrative(audioClip); break;
        }
    }
    private void StopAudio(AudioGroups group)
    {
        // This could be done better
        switch (group)
        {
            case AudioGroups.SFX: break; // Can't really stop sfx without knowing which one to stop
            case AudioGroups.Music: StopMusic(); break;
            case AudioGroups.Ambience: StopAmbience(); break;
            case AudioGroups.Narrative: StopNarrative(); break;
        }
    }

    private void PlaySFX(AudioClip audioClip, Transform audioLocation)
    {
        AudioSource audioSource;

        if (!_sfxSources.Any()) audioSource = CreateAudioSource($"SFXAudioSource{_sfxSourceInstances++:00}", _sfxMixerGroup, 1f, false);
        else audioSource = _sfxSources.Dequeue();

        audioSource.transform.position = audioLocation != null ? audioLocation.position : Vector3.zero;
        audioSource.clip = audioClip;
        audioSource.Play();

        _sfxSources.Enqueue(audioSource);
    }
    private void PlayMusic(AudioClip audioClip)
    {
        if (_musicSource.clip == audioClip) return;

        _musicSource.clip = audioClip;
        _musicSource.Play();
    }
    private void PlayAmbience(AudioClip audioClip)
    {
        if (_ambienceSource.clip == audioClip) return;

        _ambienceSource.clip = audioClip;
        _ambienceSource.Play();
    }
    private void PlayNarrative(AudioClip audioClip)
    {
        _narrativeSource.clip = audioClip;
        _narrativeSource.Play();
    }

    private void StopMusic()
    {
        _musicSource.clip = null;
        _musicSource.Stop();
    }
    private void StopAmbience()
    {
        _ambienceSource.clip = null;
        _ambienceSource.Stop();
    }
    private void StopNarrative()
    {
        _narrativeSource.clip = null;
        _narrativeSource.Stop();
    }

    #endregion
}
