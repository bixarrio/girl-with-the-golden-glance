using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeInteraction : Interaction
{
    #region Properties and Fields
    
    [SerializeField] DescriptionInteraction _description;
    [SerializeField] AudioInteraction _audio;

    #endregion

    #region Override Methods

    public override void Execute() => new Interaction[] { _description, _audio }.RunInteractions();

    #endregion
}
