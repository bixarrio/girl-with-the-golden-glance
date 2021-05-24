using UnityEngine;

public class AmbiencePlayer : MonoBehaviour
{
    #region Properties and Fields

    [SerializeField] AudioClip _sceneAmbienceClip;

    #endregion

    #region Unity Methods

    private void Start()
        => Messaging<PlayAudio>.Trigger?.Invoke(_sceneAmbienceClip, AudioGroups.Ambience);

    #endregion
}
