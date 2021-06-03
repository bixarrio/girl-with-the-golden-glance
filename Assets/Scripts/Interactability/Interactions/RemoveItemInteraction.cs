using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveItemInteraction : Interaction
{
    #region Properties and Fields

    [SerializeField] Transform _objectToDestroy;
    [SerializeField] float _delay = 0f;

    #endregion

    #region Override Methods

    public override void Execute()
        => Destroy(_objectToDestroy.gameObject, _delay);

    #endregion
}
