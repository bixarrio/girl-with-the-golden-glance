using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class EndCutsceneSignalHandler : MonoBehaviour
{
    #region Public Methods

    public void ChangeScene(string sceneName)
        => SceneTransition.Instance.DoTransition(sceneName, null, TransitionType.Fade);

    #endregion
}
