using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (_inventoryCanvas == null) return;

        _inventoryCanvas.enabled = false;

        InitInventories();
        SetItemPrefab();
    }

    private void LateUpdate()
    {
        if (!CharacterController.Instance.IsInControl) return;
        if (SceneManager.GetActiveScene().name == "Menu") return;

        if (Input.GetKeyDown(KeyCode.Tab)) _inventoryCanvas.enabled = !_inventoryCanvas.enabled;
    }

#if UNITY_EDITOR

    private void OnGUI()
    {
        using (new GUILayout.AreaScope(new Rect(10, 50, 200, 500)))
        {
            foreach (var slot in _handEquipped.IterateSlots())
                GUILayout.Label($"Hand: {slot.Item?.ItemName ?? "Empty"}");
            foreach (var slot in _topLeftPocket.IterateSlots())
                GUILayout.Label($"Top Left Pocket: {slot.Item?.ItemName ?? "Empty"}");
            foreach (var slot in _topRightPocket.IterateSlots())
                GUILayout.Label($"Top Right Pocket: {slot.Item?.ItemName ?? "Empty"}");
            foreach (var slot in _leftPocketInventory.IterateSlots())
                GUILayout.Label($"Left Pocket: {slot.Item?.ItemName ?? "Empty"}");
            foreach (var slot in _rightPocketInventory.IterateSlots())
                GUILayout.Label($"Right Pocket: {slot.Item?.ItemName ?? "Empty"}");
        }
    }

#endif

    #endregion

    #region Public Methods

    public void Clear()
    {
        _handEquipped.Clear();
        _leftPocketInventory.Clear();
        _rightPocketInventory.Clear();
        _topLeftPocket.Clear();
        _topRightPocket.Clear();
    }

    public bool TryAddItem(Item item)
    {
        Debug.Log($"Adding item {item}");

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

    public bool TryRemoveItem(Item item, bool checkConditions = false)
    {
        // If we don't have it, we can't drop it
        if (!HasItem(item)) return false;
        var slot = GetItemSlot(item);
        if (slot == null) return false;

        // Create a world version
        var world = GameObject.Find("World");
        var holding = slot.CurrentInventoryItem.GetItem();
        var dropPos = transform.position + transform.forward + transform.up;  // new Vector3(transform.position.x, 1f, transform.position.z + 0.3f);
        Debug.Log($"Dropping at {dropPos}");
        Instantiate(holding.ItemPrefab, dropPos, Random.rotation, world.transform);

        // remove from inventory
        slot.TryRemoveItem(checkConditions);
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
