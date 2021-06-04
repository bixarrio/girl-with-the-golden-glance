using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region Properties and Fields

    [SerializeField] Image _image;

    private Item _item;

    public UIInventorySlot CurrentSlot { get; set; }

    private Canvas _canvas;
    private GraphicRaycaster _raycaster;

    #endregion

    #region Unity Methods

    private void Start()
    {
        _canvas = GetComponentInParent<Canvas>();
        Debug.Assert(_canvas != null);
        _raycaster = _canvas.GetComponent<GraphicRaycaster>();
        Debug.Assert(_raycaster != null);
    }

    #endregion

    #region Public Methods

    public void SetItem(Item item)
    {
        _item = item;
        _image.sprite = _item?.InventoryIcon;
    }
    public Item GetItem() => _item;
    public bool HasItem(Item item) => _item == item;

    public bool CanDropItem() => _item.DropCondition?.CanDrop() ?? true;

    public void OnBeginDrag(PointerEventData eventData)
    {
        // only drag with the left mouse button
        if (eventData.button != PointerEventData.InputButton.Left) return;

        // move the item out of the slot and into the canvas
        transform.SetParent(_canvas.transform);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // only drag with the left mouse button
        if (eventData.button != PointerEventData.InputButton.Left) return;

        transform.localPosition += (Vector3)GetDragDelta(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // only drag with the left mouse button
        if (eventData.button != PointerEventData.InputButton.Left) return;

        var result = new List<RaycastResult>();
        _raycaster.Raycast(eventData, result);
        HandleDragEnd(result);
    }

    #endregion

    #region Private Methods

    private Vector2 GetDragDelta(PointerEventData eventData) => eventData.delta / transform.lossyScale.x;

    private void HandleDragEnd(List<RaycastResult> result)
    {
        var dropped = false;
        foreach (var hit in result)
        {
            // Check if we're dropping the item
            var group = hit.gameObject.GetComponent<UIInventoryGroup>();
            if (group != null)
            {
                if (_item.DropCondition?.CanDrop() ?? true) // no condition == true
                {
                    dropped = CharacterInventoryController.Instance.TryRemoveItem(_item, true);
                    continue;
                }
            }

            // Now, check if we're moving stuff around
            var slot = hit.gameObject.GetComponent<UIInventorySlot>();
            if (slot == null) continue; // the hit was not on a slot
            if (slot.Filled) continue; // We could be combining items here

            CurrentSlot.CurrentInventoryItem = null; // remove this item from its slot
            CurrentSlot = slot; // set the new slot
            CurrentSlot.CurrentInventoryItem = this; // put this item in the new slot

            break; // nothing more to do
        }

        if (!dropped)
        {
            transform.SetParent(CurrentSlot.transform); // reset this item's parent
            transform.localPosition = Vector3.zero; // and position in the parent
        }
    }

    #endregion
}
