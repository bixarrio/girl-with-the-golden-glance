public class UseItemInteraction : ItemInteraction
{
    #region Override Methods

    public override void Execute() => UseItem();

    #endregion

    #region Private Methods

    private void UseItem()
    {
        if (CharacterInventoryController.Instance.TryRemoveEquippedItem(_item))
            PlaySuccess();
        else
            PlayFail();
    }

    #endregion
}
