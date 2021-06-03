using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightnessButtons : MonoBehaviour
{
    #region Public Methods

    public void DoneClicked()
    {
        if (GameEventController.Instance.GameEventIsSet("From Menu"))
            SceneTransition.Instance.DoTransition("Menu", null, TransitionType.Fade);
        else
            SceneTransition.Instance.DoTransition("Intro Cutscene", null, TransitionType.Fade);
    }

    #endregion
}
