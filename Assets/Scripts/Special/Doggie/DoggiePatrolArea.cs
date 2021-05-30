using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DoggiePatrolArea : MonoBehaviour
{
    #region Properties and Fields

    [SerializeField] float _radius = 5f;
    [SerializeField] bool _reduce = true;

    #endregion

    #region Unity Methods

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        Handles.color = Color.blue;
        var pos = new Vector3(transform.position.x, 0f, transform.position.z);
        Handles.DrawWireDisc(pos, transform.up, _radius);
#endif
    }

    #endregion

    #region Public Methods

    public Vector3 GetAggressionPoint(Vector3 target)
    {
        if (_reduce && Vector3.Distance(transform.position, target) < _radius)
            return target;

        return transform.position + ((target - transform.position).normalized * _radius);
    }

    #endregion
}
