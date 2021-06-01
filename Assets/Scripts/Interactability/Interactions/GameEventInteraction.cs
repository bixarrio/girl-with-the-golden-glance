using UnityEngine;

public class GameEventInteraction : Interaction
{
    #region Properties and Fields

    [SerializeField] string _eventName;
    [SerializeField] bool _value = true;

    #endregion

    #region Override Methods

    public override void Execute() => Messaging<GameEvent>.Trigger?.Invoke(_eventName, _value);

    #endregion
}
