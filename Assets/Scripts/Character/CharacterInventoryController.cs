using System.Collections.Generic;
using UnityEngine;

public class CharacterInventoryController : MonoBehaviour
{
    #region Properties and Fields

    private static CharacterInventoryController _instance;
    public static CharacterInventoryController Instance => _instance;

    [SerializeField] Item _testItem;

    [SerializeField] Canvas _inventoryCanvas;
    [SerializeField] UIInventoryItem _itemPrefab;
    [SerializeField] Inventory _handEquipped;
    [SerializeField] Inventory _topLeftPocket;
    [SerializeField] Inventory _topRightPocket;
    [SerializeField] Inventory _leftPocketInventory;
    [SerializeField] Inventory _rightPocketInventory;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _inventoryCanvas.enabled = false;
        //var item = Instantiate(_itemPrefab, _topLeftPocket.transform);
        //item.CurrentSlot = _topLeftPocket;
        //item.transform.localPosition = Vector3.zero;
        //_topLeftPocket.CurrentInventoryItem = item;

        InitInventories();
        SetItemPrefab();
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) _inventoryCanvas.enabled = !_inventoryCanvas.enabled;
        if (Input.GetKeyDown(KeyCode.F1)) TryAddItem(Instantiate(_testItem));
    }

    #endregion

    #region Public Methods

    public bool TryAddItem(Item item)
    {
        // let's first check if we have free slots in the big pockets
        if (_rightPocketInventory.HasFreeSlot())
        {
            _rightPocketInventory.AddItemToInventory(item);
            return true;
        }
        if (_leftPocketInventory.HasFreeSlot())
        {
            _leftPocketInventory.AddItemToInventory(item);
            return true;
        }

        // next, let's check the top pockets
        if (_topRightPocket.HasFreeSlot())
        {
            _topRightPocket.AddItemToInventory(item);
            return true;
        }
        if (_topLeftPocket.HasFreeSlot())
        {
            _topLeftPocket.AddItemToInventory(item);
            return true;
        }

        // last chance. check the hand
        if (_handEquipped.HasFreeSlot())
        {
            _handEquipped.AddItemToInventory(item);
            return true;
        }

        return false;
    }

    public bool HasItemEquipped(Item item)
        => _handEquipped.ContainsItem(item);

    public bool TryRemoveEquippedItem(Item item)
    {
        if (!HasItemEquipped(item)) return false;
        return _handEquipped.TryRemoveItem(item);
    }

    #endregion

    #region Private Methods

    private void InitInventories()
    {
        _leftPocketInventory.Init();
        _rightPocketInventory.Init();

        _topLeftPocket.Init();
        _topRightPocket.Init();

        _handEquipped.Init();
    }

    private void SetItemPrefab()
    {
        _leftPocketInventory.SetItemPrefab(_itemPrefab);
        _rightPocketInventory.SetItemPrefab(_itemPrefab);

        _topLeftPocket.SetItemPrefab(_itemPrefab);
        _topRightPocket.SetItemPrefab(_itemPrefab);

        _handEquipped.SetItemPrefab(_itemPrefab);
    }

    private IEnumerable<UIInventoryItem> AggregateItems()
    {
        foreach(var slot in _rightPocketInventory.UISlots)
        {
            if (slot.CurrentInventoryItem == null) continue;
            yield return slot.CurrentInventoryItem;
        }
        foreach (var slot in _leftPocketInventory.UISlots)
        {
            if (slot.CurrentInventoryItem == null) continue;
            yield return slot.CurrentInventoryItem;
        }
        foreach (var slot in _topRightPocket.UISlots)
        {
            if (slot.CurrentInventoryItem == null) continue;
            yield return slot.CurrentInventoryItem;
        }
        foreach (var slot in _topLeftPocket.UISlots)
        {
            if (slot.CurrentInventoryItem == null) continue;
            yield return slot.CurrentInventoryItem;
        }
        foreach (var slot in _handEquipped.UISlots)
        {
            if (slot.CurrentInventoryItem == null) continue;
            yield return slot.CurrentInventoryItem;
        }
    }

    #endregion
}
