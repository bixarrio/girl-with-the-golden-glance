using UnityEngine;

public class ActivateEmission : Activatable
{
    #region Properties and Fields

    [SerializeField] Renderer _renderer;
    [SerializeField] bool _initialState = false;

    private Material _material;

    #endregion

    #region Unity Methods

    private void Start()
    {
        // We need to create an instance of the material, else
        // all models with this material will be affected
        _material = new Material(_renderer.material);
        _renderer.material = _material;
        Activate(_initialState);
    }

    #endregion

    #region Override Methods

    public override void Activate(bool activate)
    {
        if (activate) _material.EnableKeyword("_EMISSION");
        else _material.DisableKeyword("_EMISSION");
    }

    #endregion
}
