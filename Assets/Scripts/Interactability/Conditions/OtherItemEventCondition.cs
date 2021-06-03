using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherItemEventCondition : InteractionCondition
{
    #region Properties and Fields

    [SerializeField] string itemName;
    [SerializeField] string eventName;

    #endregion

    public override bool ConditionMet()
        => EvaluateWithNot(() => GameEventController.Instance.ItemEventIsSet(itemName, eventName));
}
