using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttyObjects : MonoBehaviour
{
    #region Properties and Fields

    private static CuttyObjects _instance;
    public static CuttyObjects Instance => _instance;

    [SerializeField] GameObject _bucky;
    [SerializeField] GameObject _ragdoll;

    public GameObject Bucky => _bucky;
    public GameObject Ragdoll => _ragdoll;

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
    }

    private void OnDestroy() => _instance = null;

    #endregion
}
