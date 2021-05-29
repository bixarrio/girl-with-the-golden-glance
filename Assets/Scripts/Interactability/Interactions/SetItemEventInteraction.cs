using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetItemEventInteraction : Interaction
{
    #region Properties and Fields

    [SerializeField] string _eventName;
    [SerializeField] bool _value = true;
    [SerializeField] ItemEventManager _eventManager;

    #endregion

    #region Override Methods

    public override void Execute()
        => _eventManager.SetItemEvent(_eventName, _value);

    #endregion
}
