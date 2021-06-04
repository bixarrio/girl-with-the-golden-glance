using UnityEngine;

public class StartPosition : MonoBehaviour
{
    #region Properties and Fields

    [SerializeField] Transform _startPosition;

    #endregion

    #region Unity Methods

    private void Start()
    {
        if (GameEventController.Instance.GameEventIsSet("Started")) return;
        CharacterMovementController.Instance.Teleport(_startPosition.position, _startPosition.forward);
        Messaging<GameEvent>.Trigger?.Invoke("Started", true);
    }

    #endregion
}
