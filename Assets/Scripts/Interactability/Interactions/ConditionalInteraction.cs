using UnityEngine;

public class ConditionalInteraction : Interaction
{
    #region Properties and Fields

    [SerializeField] InteractionCondition _condition;
    [SerializeField] Interaction _true;
    [SerializeField] Interaction _false;

    #endregion

    public override void Execute()
    {
        if (_condition.ConditionMet()) _true.Execute();
        else _false.Execute();
    }
}
