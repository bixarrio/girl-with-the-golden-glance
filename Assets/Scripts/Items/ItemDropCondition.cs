using UnityEngine;

[CreateAssetMenu(menuName = "GWTGG/Item Drop Condition")]
public class ItemDropCondition : ScriptableObject
{
    #region Properties and Fields

    [SerializeField] protected Interaction _successInteraction;
    [SerializeField] protected Interaction _failInteraction;

    #endregion

    #region Public Methods

    public virtual bool CanDrop() => true;

    #endregion
}
