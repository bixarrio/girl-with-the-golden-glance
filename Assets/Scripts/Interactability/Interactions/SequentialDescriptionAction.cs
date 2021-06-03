using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequentialDescriptionAction : InteractionGroup
{
    #region Properties and Fields

    [SerializeField] SequentialMode _mode;

    private int _currentDescription = 0;

    #endregion

    #region Override Methods

    public override void Execute()
    {
        _interactions[_currentDescription].Execute();
        HandleIncrementForSelectedMode();
    }

    #endregion

    #region Private Methods

    private void HandleIncrementForSelectedMode()
    {
        if (_mode == SequentialMode.Wrap)
        {
            // Wrap the current interaction back to 0 if we've reached the end of the interactions
            _currentDescription = (_currentDescription + 1) % _interactions.Length;
        }
        else if (_mode == SequentialMode.Clamp)
        {
            // Only increment if we haven't reached the end of the interactions yet
            _currentDescription = Mathf.Clamp(_currentDescription + 1, 0, _interactions.Length - 1);
        }
    }

    #endregion
}
