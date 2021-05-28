using System;
using UnityEngine;

public abstract class InteractionCondition : MonoBehaviour
{
    #region Properties and Fields

    [SerializeField] protected bool _not;

    #endregion

    #region Public Methods

    public abstract bool ConditionMet();

    #endregion

    #region Private Methods

    protected bool EvaluateWithNot(Func<bool> condition)
    {
        var result = condition?.Invoke() ?? true;
        return _not ? !result : result;
    }

    #endregion
}
