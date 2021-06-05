using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioAdjustmentButtons : MonoBehaviour
{
    #region Properties and Fields

    [SerializeField] Text _descriptionText;
    [SerializeField] Text _valueText;

    #endregion

    #region Unity Methods

    private void Start()
    {
        _descriptionText.text = string.Empty;
        _valueText.text = string.Empty;
    }

    #endregion

    #region Public Methods

    public void DoneClicked() => SceneTransition.Instance.DoTransition("Menu", null, TransitionType.Fade);

    #endregion
}
