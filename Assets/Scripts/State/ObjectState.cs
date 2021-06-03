using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectState : MonoBehaviour
{
    #region Unity Methods

    protected virtual void Start() => HandleState();

    #endregion

    #region Public Methods

    public abstract void HandleState();

    #endregion
}
