using UnityEngine;
using UnityEngine.EventSystems;

public class OptionsInteractable : Interactable
{
    #region Properties and Fields

    [SerializeField] InteractionMenuOption[] _interactionMenuOptions;
    public InteractionMenuOption[] MenuOptions => _interactionMenuOptions;

    #endregion

    #region Unity Methods

    protected override void OnMouseDown()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;
        Messaging<OptionsInteractableClicked>.Trigger?.Invoke(this, Input.mousePosition);
    }

    #endregion

    #region Public Methods

    public virtual void StartInteraction(CharacterMovementController movementController, InteractionMenuOption option)
    {
        movementController.SetLookDirection(transform.position);
        option.Interaction.Execute();
    }


    #endregion
}