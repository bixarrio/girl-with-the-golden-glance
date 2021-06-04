using UnityEngine;

public class AmbiencePlayer : MonoBehaviour
{
    #region Properties and Fields

    [SerializeField] bool _stopAmbience;
    [SerializeField] AudioClip _sceneAmbienceClip;

    #endregion

    #region Unity Methods

    private void Start()
    {
        if (_stopAmbience)
            Messaging<StopAudio>.Trigger?.Invoke(AudioGroups.Ambience);

        Messaging<PlayAudio>.Trigger?.Invoke(_sceneAmbienceClip, AudioGroups.Ambience);
    }

    #endregion
}
