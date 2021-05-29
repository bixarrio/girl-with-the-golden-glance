using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedTrigger : Trigger
{
    #region Properties and Fields

    [SerializeField] float _delayInSeconds;
    [SerializeField] bool _canRetrigger = true;
    [SerializeField] bool _canRetriggerWhenActive = false;
    [SerializeField] bool _resetOnRetrigger = false;

    private bool _triggered = false;
    private Coroutine _delayRoutine;

    #endregion

    #region Override Methods

    public override void Fire()
    {
        if (_triggered && !_canRetrigger) return;
        if (_delayRoutine != null && !_canRetriggerWhenActive) return;
        if (_delayRoutine != null && _resetOnRetrigger) StopCoroutine(_delayRoutine);
        _delayRoutine = StartCoroutine(Delay(_delayInSeconds));
    }

    #endregion

    #region Private Methods

    private IEnumerator Delay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        base.Fire();
        _triggered = true;
    }

    #endregion
}
