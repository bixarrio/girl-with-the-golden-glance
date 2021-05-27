using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneInteraction : Interaction
{
    #region Properties and Fields

    [SerializeField] string _targetScene;
    [SerializeField] Transform _characterSpawnPosition;

    #endregion

    #region Unity Methods

    private void OnDrawGizmosSelected()
    {
        var myPos = transform.position;
        myPos.y = 0;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(myPos, _characterSpawnPosition.position);

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(_characterSpawnPosition.position, 0.125f);
        Gizmos.DrawRay(_characterSpawnPosition.position, _characterSpawnPosition.forward);
    }

    #endregion

    #region Public Methods

    public override void Execute()
    {
        SceneTransition.Instance.DoTransition(_targetScene,
            () => CharacterMovementController.Instance.Teleport(_characterSpawnPosition.position, _characterSpawnPosition.forward));
    }

    #endregion
}
