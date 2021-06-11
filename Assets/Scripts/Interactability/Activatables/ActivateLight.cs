using UnityEngine;

public class ActivateLight : Activatable
{
    #region Properties and Fields

    [SerializeField] Light _light;

    #endregion

    #region Override Methods

    public override void Activate(bool activate) => _light.enabled = activate;

    #endregion

}
