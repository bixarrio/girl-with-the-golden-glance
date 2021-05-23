using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequentialDescriptionAction : MultiDescriptionAction
{
    #region Properties and Fields

    [SerializeField] SequentialMode _mode;

    private int _currentDescription = 0;

    #endregion

    #region Override Methods

    public override void Execute()
    {
        _descriptions[_currentDescription].Execute();
        HandleIncrementForSelectedMode();
    }

    #endregion

    #region Private Methods

    private void HandleIncrementForSelectedMode()
    {
        if (_mode == SequentialMode.Wrap)
        {
            // Wrap the current description back to 0 if we've reached the end of the descriptions
            _currentDescription = (_currentDescription + 1) % _descriptions.Length;
        }
        else if (_mode == SequentialMode.Clamp)
        {
            // Only increment if we haven't reached the end of the descriptions yet
            _currentDescription = Mathf.Clamp(_currentDescription + 1, 0, _descriptions.Length - 1);
        }
    }

    #endregion
}
