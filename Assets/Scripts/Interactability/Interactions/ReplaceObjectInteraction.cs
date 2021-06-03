using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceObjectInteraction : Interaction
{
    #region Properties and Fields

    [SerializeField] GameObject _thisObjectReference;
    [SerializeField] GameObject _newObjectPrefab;
    [SerializeField] Transform _spawnTransform;

    #endregion

    #region Override Methods

    public override void Execute()
    {
        Instantiate(_newObjectPrefab, _spawnTransform.position, _spawnTransform.rotation, _thisObjectReference.transform.parent);

        _thisObjectReference.transform.SetParent(null);
        Destroy(_thisObjectReference);
    }

    #endregion
}
