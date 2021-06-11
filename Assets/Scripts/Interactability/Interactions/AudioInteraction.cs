using UnityEngine;

public class AudioInteraction : Interaction
{
    #region Properties and Fields

    [SerializeField] AudioClip _audioClip;
    [SerializeField] AudioGroups _audioGroup;

    #endregion

    #region Override Methods

    public override void Execute()
        => Messaging<PlayAudio>.Trigger?.Invoke(_audioClip, _audioGroup);

    #endregion
}
