using UnityEngine;

public class ResurrectBucky : MonoBehaviour
{
    #region Unity Methods

    private void Start() => Resurrect();

    #endregion

    #region Private Methods
    
    private void Resurrect()
    {
        GameEventController.Instance.ResetEvents();
        CharacterInventoryController.Instance.Clear();
        CharacterObjects.Instance.Bucky.SetActive(true);
        CharacterObjects.Instance.Ragdoll.SetActive(false);
        CharacterController.Instance.RegainControl();
    }

    #endregion

}
