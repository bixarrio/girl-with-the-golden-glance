using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region Properties and Fields

    [SerializeField] Transform _target;
    [SerializeField] Vector2 _centerRotationAngle = Vector2.zero;
    [SerializeField] Vector2 _maxRotationAngle = Vector2.one;

    #endregion

    #region Unity Methods

    #endregion
}
