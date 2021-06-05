using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryGroup : MonoBehaviour
{
    #region Properties and Fields

    private static UIInventoryGroup _instance;
    public static UIInventoryGroup Instance => _instance;

    [SerializeField] Text _itemNameText;
    [SerializeField] Text _itemDescriptionText;

    private Canvas _canvas;

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
        _canvas = GetComponent<Canvas>();
        Debug.Assert(_canvas != null);
    }

    #endregion

    #region Public Methods

    public void ShowItemInfo(Item item)
    {
        _itemNameText.text = item.ItemName;
        _itemDescriptionText.text = item.ItemDescription;
    }

    public void ClearItemInfo()
    {
        _itemNameText.text = string.Empty;
        _itemDescriptionText.text = string.Empty;
    }

    public void CloseInventory() => _canvas.enabled = false;

    #endregion
}
