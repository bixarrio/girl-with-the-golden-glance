using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioAdjustmentButtons : MonoBehaviour
{
    #region Public Methods

    public void DoneClicked() => SceneTransition.Instance.DoTransition("Menu", null, TransitionType.Fade);

    #endregion
}
