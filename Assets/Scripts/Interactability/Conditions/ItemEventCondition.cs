using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEventCondition : InteractionCondition
{
    #region Properties and Fields

    [SerializeField] string _eventName;
    [SerializeField] ItemEventManager _eventManager;

    #endregion

    public override bool ConditionMet()
        => EvaluateWithNot(() => _eventManager.ItemEventIsSet(_eventName));
}
