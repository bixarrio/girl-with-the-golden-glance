using UnityEngine;

public class GameEventCondition : InteractionCondition
{
    #region Properties and Fields

    [SerializeField] string _eventName;

    #endregion

    #region Override Methods

    public override bool ConditionMet()
        => EvaluateWithNot(() => GameEventController.Instance.GameEventIsSet(_eventName));

    #endregion
}
