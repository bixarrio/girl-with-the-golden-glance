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

    private bool _isInCutscene = false;
    public bool IsInCutscene => _isInCutscene;

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

    public void RelinquishControl(bool cutscene = false)
    {
        _isInControl = false;
        if (cutscene) _isInCutscene = true;
    }

    public void RegainControl(bool cutscene = false)
    {
        _isInControl = true;
        if (cutscene) _isInCutscene = false;
    }

    #endregion
}
