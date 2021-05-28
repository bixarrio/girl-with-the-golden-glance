using UnityEngine;

public class GameEventInteraction : Interaction
{
    #region Properties and Fields

    [SerializeField] string _eventName;

    #endregion

    #region Override Methods

    public override void Execute() => Messaging<GameEvent>.Trigger?.Invoke(_eventName);

    #endregion
}
