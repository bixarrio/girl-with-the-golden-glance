using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryGroup : MonoBehaviour
{
    #region Properties and Fields

    private static UIInventoryGroup _instance;
    public static UIInventoryGroup Instance => _instance;

    private Canvas _canvas;

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

    private void Start()
    {
        _canvas = GetComponent<Canvas>();
        Debug.Assert(_canvas != null);
    }

    #endregion

    #region Public Methods

    public void CloseInventory() => _canvas.enabled = false;

    #endregion
}
