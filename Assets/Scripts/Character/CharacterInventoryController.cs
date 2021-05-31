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
        // let's first check the hand
        if (_handEquipped.HasFreeSlot())
        {
            _handEquipped.AddItemToInventory(item);
            return true;
        }

        // then check if we have free slots in the big pockets
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

        // lastly, let's check the top pockets
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

        return false;
    }

    public bool HasItemEquipped(Item item)
        => _handEquipped.ContainsItem(item);

    public bool HasItem(Item item)
    {
        foreach (var uiitem in AggregateItems())
        {
            if (uiitem.HasItem(item)) return true;
        }
        return false;
    }

    public bool TryRemoveEquippedItem(Item item)
    {
        // If we don't have it, we can't remove it
        if (!HasItemEquipped(item)) return false;
        return _handEquipped.TryRemoveItem(item);
    }

    public bool TryRemoveItem(Item item)
    {
        // If we don't have it, we can't drop it
        if (!HasItem(item)) return false;
        var slot = GetItemSlot(item);
        if (slot == null) return false;

        // Create a world version
        var world = GameObject.Find("World");
        var holding = slot.CurrentInventoryItem.GetItem();
        var dropPos = transform.TransformVector(new Vector3(transform.position.x, 1f, transform.position.z + 0.3f));
        Debug.Log($"Dropping at {dropPos}");
        Instantiate(holding.ItemPrefab, dropPos, Random.rotation, world.transform);

        // remove from inventory
        slot.TryRemoveItem();
        return true;
    }

    #endregion

    #region Private Methods

    private void InitInventories()
    {
        _handEquipped.Init();

        _leftPocketInventory.Init();
        _rightPocketInventory.Init();

        _topLeftPocket.Init();
        _topRightPocket.Init();
    }

    private void SetItemPrefab()
    {
        _handEquipped.SetItemPrefab(_itemPrefab);

        _leftPocketInventory.SetItemPrefab(_itemPrefab);
        _rightPocketInventory.SetItemPrefab(_itemPrefab);

        _topLeftPocket.SetItemPrefab(_itemPrefab);
        _topRightPocket.SetItemPrefab(_itemPrefab);
    }

    private UIInventorySlot GetItemSlot(Item item)
    {
        foreach (var slot in AggregateSlots())
            if (slot.CurrentInventoryItem?.HasItem(item) ?? false)
                return slot;
        return null;
    }

    private IEnumerable<UIInventorySlot> AggregateSlots()
    {
        foreach (var slot in _handEquipped.UISlots)
            yield return slot;

        foreach (var slot in _rightPocketInventory.UISlots)
            yield return slot;

        foreach (var slot in _leftPocketInventory.UISlots)
            yield return slot;

        foreach (var slot in _topRightPocket.UISlots)
            yield return slot;

        foreach (var slot in _topLeftPocket.UISlots)
            yield return slot;
    }

    private IEnumerable<UIInventoryItem> AggregateItems()
    {
        foreach (var slot in AggregateSlots())
        {
            if (slot.CurrentInventoryItem == null) continue;
            yield return slot.CurrentInventoryItem;
        }
    }

    #endregion
}
