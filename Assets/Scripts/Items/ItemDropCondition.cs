using UnityEngine;

[CreateAssetMenu(menuName = "GWTGG/Item Drop Condition")]
public class ItemDropCondition : ScriptableObject
{
    #region Public Methods

    public virtual bool CanDrop() => true;

    #endregion
}
