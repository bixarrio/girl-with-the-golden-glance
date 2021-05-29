using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trigger : MonoBehaviour
{
    #region Properties and Fields

    [SerializeField] Interaction _triggerAction;

    #endregion

    #region Public Methods

    public virtual void Fire() => _triggerAction.Execute();

    #endregion
}
