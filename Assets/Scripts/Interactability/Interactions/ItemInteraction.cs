using UnityEngine;

public abstract class ItemInteraction : Interaction
{
    #region Properties and Fields

    [SerializeField] protected Item _item;
    [SerializeField] protected Interaction _success;
    [SerializeField] protected Interaction _fail;

    #endregion

    #region Private Methods

    protected void PlayFail() => _fail?.Execute();
    protected void PlaySuccess() => _success?.Execute();

    #endregion
}
