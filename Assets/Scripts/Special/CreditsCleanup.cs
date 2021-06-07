using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsCleanup : MonoBehaviour
{
    #region Properties and Fields

    [SerializeField] AudioListener _listener;

    #endregion

    #region Unity Methods

    private void Start()
    {
        Destroy(CutsceneBuckyController.Instance.gameObject);
        _listener.enabled = true;
    }

    #endregion
}
