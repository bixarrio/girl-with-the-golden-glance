public class GetItemInteraction : ItemInteraction
{
    #region Override Methods

    public override void Execute() => ReceiveItem();

    #endregion

    #region Private Methods

    private void ReceiveItem()
    {
        if (CharacterInventoryController.Instance.TryAddItem(_item)) PlaySuccess();
        else PlayFail();
    }

    #endregion
}
