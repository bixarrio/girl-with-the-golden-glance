using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CarFollow : MonoBehaviour
{
    #region Properties and Fields

    [SerializeField] float _speed = 2f;
    [SerializeField] float _smoothness = 0.01f;
    [SerializeField] Transform[] _waypoints;

    private bool _moving = false;
    private int _currentWaypoint = 0;
    private float _currentPoint = 0f;

    private Vector3[] _points => _waypoints.Select(wp => wp.position).ToArray();

    #endregion

    #region Unity Methods

    private void Start()
    {
        transform.position = _waypoints[0].position;
        transform.rotation = Quaternion.LookRotation(_waypoints[1].position - _waypoints[0].position);
        Invoke(nameof(StartCar), Random.Range(1f, 5f));
    }

    private void Update()
    {
        if (!_moving) return;

        var newPos = GetBezierPoint(_points, _currentPoint);
        _currentPoint += _smoothness * _speed * Time.deltaTime;
        _moving = _currentPoint < 1f;

        var nextPos = GetBezierPoint(_points, _currentPoint);
        transform.position = newPos;
        transform.rotation = Quaternion.LookRotation(nextPos - newPos);
    }

    private void OnDrawGizmos()
    {

        for (var i = 0f; i < 1f; i += 0.05f)
        {
            Gizmos.DrawSphere(GetBezierPoint(_points, i), 0.2f);
        }
    }

    #endregion

    #region Private Methods

    private void StartCar() => _moving = true;

    private Vector3 GetBezierPoint(Vector3[] points, float t)
        {
            // Lazy
            var a = Vector3.Lerp(points[0], points[1], t);
            var b = Vector3.Lerp(points[1], points[2], t);
            var c = Vector3.Lerp(points[2], points[3], t);

            var d = Vector3.Lerp(a, b, t);
            var e = Vector3.Lerp(b, c, t);

            return Vector3.Lerp(d, e, t);
        }

    #endregion
}
