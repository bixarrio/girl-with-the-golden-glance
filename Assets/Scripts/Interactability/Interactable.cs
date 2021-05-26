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

    [SerializeField] Sprite _overCursor;
    [SerializeField, Min(0f)] float _interactRadius = 1.5f;
    [SerializeField] bool _hasFront = false;

    [SerializeField] InteractionMenuOption[] _interactionMenuOptions;

    public InteractionMenuOption[] MenuOptions => _interactionMenuOptions;

    #endregion

    #region Unity Methods

    private void OnMouseDown()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;
        Messaging<InteractableClicked>.Trigger?.Invoke(this, Input.mousePosition);
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

    private void OnMouseOver()
    {
        Debug.Log("Over");
        // Change the cursor
        if (_overCursor == null) return;
        Cursor.SetCursor(_overCursor.texture, Vector2.zero, CursorMode.ForceSoftware);
    }

    private void OnMouseExit()
    {
        Debug.Log("Not Over");
        // Change the cursor back
        if (_overCursor == null) return;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    #endregion

    #region Public Methods

    public Vector3 InteractTarget(Transform source)
    {
        // if this interactable has a front, calculate a point on the interact radius *in front* of the interactable
        if (_hasFront) return transform.position + transform.forward * _interactRadius;
        // If there is no front, calculate a point on the interact radius directly between the source and the interactable
        return transform.position + (source.position - transform.position).normalized * _interactRadius;
    }

    public void StartInteraction(CharacterMovementController movementController, InteractionMenuOption option)
    {
        movementController.SetLookDirection(transform.position);
        option.Interactions.RunInteractions();
    }


    #endregion
}