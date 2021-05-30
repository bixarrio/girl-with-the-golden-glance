using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEquippedCondition : InteractionCondition
{
    #region Properties and Fields

    [SerializeField] Item _requiredItem;

    #endregion

    #region Override Methods

    public override bool ConditionMet()
        => EvaluateWithNot(() => CharacterInventoryController.Instance.HasItemEquipped(_requiredItem));

    #endregion
}
