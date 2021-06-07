using UnityEngine;

public class EndingCleanup : MonoBehaviour
{
    #region Unity Methods

    private void Start() => Cleanup();

    #endregion

    #region Private Methods

    private void Cleanup()
    {
        GameEventController.Instance.ResetEvents(); // Clean the events
        Destroy(UIHud.Instance.gameObject); // Kill the HUD
        Destroy(CharacterMovementController.Instance.gameObject);
        Destroy(UIInventoryGroup.Instance.gameObject);
    }

    #endregion
}
