using UnityEngine;
using UnityEngine.UI;

public class CursorFollow : MonoBehaviour
{
    #region Properties and Fields

    [SerializeField] Sprite _defaultCursor;
    [SerializeField] Image _cursorImage;

    #endregion

    #region Unity Methods

    private void Start()
    {
        Cursor.visible = false;
        SetCursor(_defaultCursor);
    }

    private void Update() => transform.position = Input.mousePosition;

    private void OnEnable() => HookMessages();
    private void OnDisable() => UnhookMessages();

    #endregion

    #region Private Methods

    private void HookMessages()
    {
        Messaging<SetCursor>.Register(SetCursor);
    }
    private void UnhookMessages()
    {
        Messaging<SetCursor>.Unregister(SetCursor);
    }

    private void SetCursor(Sprite cursor)
    {
        if (cursor == null) _cursorImage.sprite = _defaultCursor;
        else _cursorImage.sprite = cursor;
    }

    #endregion
}
