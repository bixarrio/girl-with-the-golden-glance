using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionConditionGroup : InteractionCondition
{
    #region Properties and Fields

    [SerializeField] List<InteractionCondition> _conditions;

    #endregion

    #region Override Methods

    public override bool ConditionMet()
        => EvaluateWithNot(() => _conditions.All(c => c.ConditionMet()));

    #endregion
}
