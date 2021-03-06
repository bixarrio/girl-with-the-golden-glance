using UnityEngine;
using UnityEngine.EventSystems;

public class OptionsInteractable : Interactable
{
    #region Properties and Fields

    [SerializeField] InteractionMenuOption[] _interactionMenuOptions;
    [SerializeField] InteractionCondition _optionsAvailableCondition;

    public InteractionMenuOption[] MenuOptions => _interactionMenuOptions;


    #endregion

    #region Unity Methods

    protected override void OnMouseDown()
    {
        if (!_optionsAvailableCondition?.ConditionMet() ?? false) return;
        if (!HasOptionsAvailable()) return;
        if (!Input.GetMouseButtonDown(0)) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;
        Messaging<OptionsInteractableClicked>.Trigger?.Invoke(this, Input.mousePosition);
    }

    protected override void OnMouseOver()
    {
        if (!_optionsAvailableCondition?.ConditionMet() ?? false) return;
        if (!HasOptionsAvailable()) return;
        base.OnMouseOver();
    }

    protected override void OnMouseExit()
    {
        if (!_optionsAvailableCondition?.ConditionMet() ?? false) return;
        if (!HasOptionsAvailable()) return;
        base.OnMouseExit();
    }

    #endregion

    #region Public Methods

    public virtual void StartInteraction(CharacterMovementController movementController, InteractionMenuOption option)
    {
        movementController.SetLookDirection(transform.position);
        option.Interaction.Execute();
    }


    #endregion

    #region Private Methods

    private bool HasOptionsAvailable()
    {
        foreach(var option in _interactionMenuOptions)
        {
            if (option.Condition?.ConditionMet() ?? true)
                return true;
        }
        return false;
    }

    #endregion
}