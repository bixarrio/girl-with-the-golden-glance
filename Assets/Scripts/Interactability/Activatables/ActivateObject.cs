using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObject : Activatable
{
    #region Properties and Fields

    [SerializeField] GameObject _object;

    #endregion

    public override void Activate(bool activate) => _object.gameObject.SetActive(activate);
}
