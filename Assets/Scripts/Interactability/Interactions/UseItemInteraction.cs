using UnityEngine;

public class UseItemInteraction : ItemInteraction
{
    #region Properties and Fields

    [SerializeField] protected bool _removeItem = true;

    #endregion

    #region Override Methods

    public override void Execute() => UseItem();

    #endregion

    #region Private Methods

    private void UseItem()
    {
        if (CharacterInventoryController.Instance.HasItemEquipped(_item))
        {
            if (!_removeItem)
            {
                PlaySuccess();
                return;
            }

            if (CharacterInventoryController.Instance.TryRemoveEquippedItem(_item))
            {
                PlaySuccess();
                return;
            }
        }

        PlayFail();
    }

    #endregion
}
