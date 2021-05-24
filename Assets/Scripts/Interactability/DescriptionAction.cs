using UnityEngine;

public class DescriptionAction : Interaction
{
    #region Properties and Fields
    
    [Header("Narrative Text")]
    [SerializeField] string _description;
    [Header("Narrative Audio")]
    [SerializeField] AudioClip _audioClip;

    #endregion

    #region Override Methods
    
    public override void Execute()
    {
        if (_audioClip != null) Messaging<PlayAudio>.Trigger?.Invoke(_audioClip, AudioGroups.Narrative);
        Messaging<ShowNarrative>.Trigger?.Invoke(_description);
    }

    #endregion
}
