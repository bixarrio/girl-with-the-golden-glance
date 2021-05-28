using UnityEngine;

[CreateAssetMenu(menuName = "GWTGG/Never Drop Item Condition")]
public class NeverDropItemCondition : ScriptableObject
{
    #region Public Methods

    public virtual bool CanDrop() => false;

    #endregion
}
