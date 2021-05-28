using UnityEngine;

[CreateAssetMenu(menuName = "GWTGG/Game Event Item Drop Condition")]
public class GameEventItemDropCondition : ItemDropCondition
{
    #region Properties and Fields

    [SerializeField] string _eventName;
    [SerializeField] bool _not;

    #endregion

    #region Override Methods

    public override bool CanDrop()
    {
        var result = GameEventController.Instance.GameEventOccurred(_eventName);
        return _not ? !result : result;
    }

    #endregion
}
