using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DoggieRope : MonoBehaviour
{
    #region Properties and Fields

    [SerializeField] Transform _anchor;

    private LineRenderer _rope;

    #endregion

    #region Unity Methods

    private void Start()
    {
        _rope = GetComponent<LineRenderer>();
    }

    private void LateUpdate()
    {
        _rope.positionCount = 2;
        _rope.SetPosition(0, _anchor.position);
        _rope.SetPosition(1, transform.position);
    }

    #endregion
}
