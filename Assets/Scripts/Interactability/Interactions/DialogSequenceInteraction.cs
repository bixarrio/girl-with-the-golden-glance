using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSequenceInteraction : Interaction
{
    #region Properties and Fields

    [SerializeField] DialogInteraction[] _dialog;

    #endregion

    #region Override Methods

    public override void Execute() => StartCoroutine(RunDialogSequence());

    #endregion

    #region Private Methods

    private IEnumerator RunDialogSequence()
    {
        foreach(var line in _dialog)
        {
            line.Dialog.Execute();
            yield return new WaitForSeconds(line.Duration);
        }
    }

    #endregion
}

[System.Serializable]
public struct DialogInteraction
{
    public NarrativeInteraction Dialog;
    public float Duration;
}
