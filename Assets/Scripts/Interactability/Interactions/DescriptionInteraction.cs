using UnityEngine;

public class DescriptionInteraction : Interaction
{
    #region Properties and Fields
    
    [SerializeField] string _description;

    #endregion

    #region Override Methods

    public override void Execute() => Messaging<ShowNarrative>.Trigger?.Invoke(_description);

    #endregion
}
