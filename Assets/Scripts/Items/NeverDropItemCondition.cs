using UnityEngine;

[CreateAssetMenu(menuName = "GWTGG/Never Drop Item Condition")]
public class NeverDropItemCondition : ItemDropCondition
{
    #region Public Methods

    public override bool CanDrop() => false;

    #endregion
}
