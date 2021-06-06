using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneButtons : MonoBehaviour
{
    #region Properties and Fields

    [SerializeField] string _targetScene;
    [SerializeField] TransitionType _transitionType;

    #endregion

    #region Public Methods

    public void SkipCutscene()
    {
        CharacterController.Instance.RegainControl();
        SceneTransition.Instance.DoTransition(_targetScene, null, _transitionType);
    }

    #endregion
}
