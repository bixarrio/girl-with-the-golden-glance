using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceObjectState : ObjectState
{
    #region Properties and Fields
    
    [SerializeField] GameObject _thisObjectReference;
    [SerializeField] GameObject _newObjectPrefab;
    [SerializeField] Transform _spawnTransform;

    [SerializeField] InteractionCondition _condition;

    #endregion

    #region Override Methods

    public override void HandleState()
    {
        if (!_condition.ConditionMet()) return;

        Instantiate(_newObjectPrefab, _spawnTransform.position, _spawnTransform.rotation, _thisObjectReference.transform.parent);

        _thisObjectReference.transform.SetParent(null);
        Destroy(_thisObjectReference);
    }

    #endregion
}
