using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MultiDescriptionAction : Action
{
    #region Properties and Fields

    [SerializeField] protected DescriptionAction[] _descriptions;

    #endregion
}
