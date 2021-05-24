using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RadialButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    #region Properties and Fields
    
    [SerializeField] Image _icon;

    private RadialMenu _myMenu;
    private Interactable _interactable;
    private InteractionMenuOption _option;

    #endregion

    #region Public Methods

    public void SetIcon(Sprite icon) => _icon.sprite = icon;
    public void SetMyData(RadialMenu menu, Interactable interactable, InteractionMenuOption option)
    {
        _myMenu = menu;
        _interactable = interactable;
        _option = option;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CharacterMovementController.Instance.SetInteractionTarget(_interactable, _option);
        Messaging<CloseMenu>.Trigger?.Invoke();
    }
    public void OnPointerEnter(PointerEventData eventData)
        => transform.localScale = new Vector3(1.1f, 1.1f, 0f);
    public void OnPointerExit(PointerEventData eventData)
        => transform.localScale = new Vector3(1f, 1f, 0f);

    #endregion
}
