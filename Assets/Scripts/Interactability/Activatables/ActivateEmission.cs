using UnityEngine;

public class ActivateEmission : Activatable
{
    #region Properties and Fields

    [SerializeField] Renderer _renderer;

    #endregion

    #region Override Methods
    
    public override void Activate(bool activate)
    {
        if (activate) _renderer.material.EnableKeyword("_EMISSION");
        else _renderer.material.DisableKeyword("_EMISSION");
    }

    #endregion
}
