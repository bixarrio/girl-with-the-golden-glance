using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DescriptionAction : Action
{
    #region Properties and Fields
    
    [Header("Narrative Text")]
    [SerializeField] string _description;
    [Header("Narrative Audio")]
    [SerializeField] AudioClip _audioClip;
    [SerializeField] AudioGroups _group;

    #endregion

    #region Override Methods
    
    public override void Execute()
    {
        if (_audioClip != null) Messaging<PlayAudio>.Trigger(_audioClip, _group);
        Messaging<ShowNarrative>.Trigger(_description);
    }

    #endregion
}
