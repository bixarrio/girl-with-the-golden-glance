using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Interactable : MonoBehaviour
{
    #region Properties and Fields

    [SerializeField] protected Sprite _overCursor;
    [SerializeField, Min(0f)] protected float _interactRadius = 1.5f;
    [SerializeField] protected bool _hasFront = false;

    [SerializeField] Interaction _interaction;

    #endregion

    #region Unity Methods

    protected virtual void OnMouseDown()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;
        Messaging<InteractableClicked>.Trigger?.Invoke(this, Input.mousePosition); // not sure we need this
        CharacterMovementController.Instance.SetInteractionTarget(this, _interaction);
    }

    protected virtual void OnMouseOver()
    {
        // Change the cursor
        if (_overCursor == null) return;
        Messaging<SetCursor>.Trigger?.Invoke(_overCursor);
    }

    protected virtual void OnMouseExit()
    {
        // Change the cursor back
        if (_overCursor == null) return;
        Messaging<SetCursor>.Trigger?.Invoke(null);
    }

    private void OnDrawGizmosSelected()
    {
#if UNITY_EDITOR

        if (_hasFront)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(InteractTarget(transform), 0.125f);
        }
        else
        {
            Handles.color = Color.cyan;
            Handles.DrawWireDisc(transform.position, Vector3.up, _interactRadius, 2f);
        }

#endif
    }

    #endregion

    #region Public Methods

    public virtual Vector3 InteractTarget(Transform source)
    {
        // if this interactable has a front, calculate a point on the interact radius *in front* of the interactable
        if (_hasFront) return transform.position + transform.forward * _interactRadius;
        // If there is no front, calculate a point on the interact radius directly between the source and the interactable
        return transform.position + (source.position - transform.position).normalized * _interactRadius;
    }

    public virtual void StartInteraction(CharacterMovementController movementController, Interaction interaction)
    {
        movementController.SetLookDirection(transform.position);
        interaction.Execute();
    }


    #endregion
}
