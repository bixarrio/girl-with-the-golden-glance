using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateInteraction : Interaction
{
    #region MyRegion

    [SerializeField] Activatable _activatable;
    [SerializeField] bool _activate;

    #endregion

    #region Override Methods

    public override void Execute() => _activatable.Activate(_activate);

    #endregion
}
