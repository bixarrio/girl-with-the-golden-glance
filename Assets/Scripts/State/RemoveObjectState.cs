using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveObjectState : ObjectState
{
    #region Properties and Fields

    [SerializeField] GameObject _objectToRemove;
    [SerializeField] float _delay;
    [SerializeField] InteractionCondition _condition;

    #endregion

    #region Override Methods

    public override void HandleState()
    {
        if (_condition.ConditionMet()) Destroy(_objectToRemove, _delay);
    }

    #endregion
}
