using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionConditionGroup : InteractionCondition
{
    #region Properties and Fields

    [SerializeField] List<InteractionCondition> _conditions;
    [SerializeField] ConditionGroupOperator _operator;

    #endregion

    #region Override Methods

    public override bool ConditionMet()
    {
        return EvaluateWithNot(() =>
        {
            if (_operator == ConditionGroupOperator.And) return _conditions.All(c => c.ConditionMet());
            
            else if (_operator == ConditionGroupOperator.Or) return _conditions.Any(c => c.ConditionMet());

            return false; // won't get here
        });
    }

    #endregion
}

public enum ConditionGroupOperator { And, Or }