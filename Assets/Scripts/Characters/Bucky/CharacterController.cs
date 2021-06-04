using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    #region Properties and Fields

    private static CharacterController _instance;
    public static CharacterController Instance => _instance;

    private bool _isInControl = true;
    public bool IsInControl => _isInControl;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    #region Public Methods

    public void RelinquishControl() => _isInControl = false;
    public void RegainControl() => _isInControl = true;

    #endregion
}
