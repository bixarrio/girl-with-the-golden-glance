using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatableObjectState : ObjectState
{
    #region Properties and Fields

    [SerializeField] Activatable _activatable;
    [SerializeField] InteractionCondition _condition;

    #endregion

    #region Override Methods

    public override void HandleState()
        => _activatable.Activate(_condition.ConditionMet());

    #endregion
}
