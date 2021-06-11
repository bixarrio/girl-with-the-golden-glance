using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionGroup : Action
{
    #region Properties and Fields

    [SerializeField] Action[] _actions;

    #endregion

    #region Override Methods

    public override void Execute() => _actions.RunActions();

    #endregion
}
