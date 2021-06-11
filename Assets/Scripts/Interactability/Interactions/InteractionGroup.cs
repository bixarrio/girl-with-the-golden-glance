using UnityEngine;

public class InteractionGroup : Interaction
{
    #region Properties and Fields

    [SerializeField] protected Interaction[] _interactions;

    #endregion

    #region Override Methods
    
    public override void Execute() => _interactions.RunInteractions();

    #endregion
}
