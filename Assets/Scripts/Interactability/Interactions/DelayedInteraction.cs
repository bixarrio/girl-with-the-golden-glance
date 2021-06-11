using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedInteraction : Interaction
{
    #region Properties and Fields

    [SerializeField] float _delay;
    [SerializeField] Interaction _interaction;

    #endregion

    #region Override Methods

    public override void Execute() => StartCoroutine(ExecuteAfter(_delay));

    #endregion

    #region Private Methods

    private IEnumerator ExecuteAfter(float delay)
    {
        yield return new WaitForSeconds(delay);
        _interaction.Execute();
    }

    #endregion
}
