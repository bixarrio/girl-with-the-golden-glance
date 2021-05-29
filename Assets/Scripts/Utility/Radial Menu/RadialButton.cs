using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RadialButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    #region Properties and Fields

    [SerializeField] Image _icon;
    [SerializeField] Text _descriptionText;

    private RadialMenu _myMenu;
    private OptionsInteractable _interactable;
    private InteractionMenuOption _option;

    #endregion

    #region Unity Methods

    private void Start() => _descriptionText.text = string.Empty;

    #endregion

    #region Public Methods

    public void SetIcon(Sprite icon) => _icon.sprite = icon;
    public void SetMyData(RadialMenu menu, OptionsInteractable interactable, InteractionMenuOption option)
    {
        _myMenu = menu;
        _interactable = interactable;
        _option = option;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CharacterMovementController.Instance.SetInteractionTarget(_interactable, _option.Interaction);
        Messaging<CloseMenu>.Trigger?.Invoke();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 0f);
        _descriptionText.text = _option.Description;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1f, 1f, 0f);
        _descriptionText.text = string.Empty;
    }

    #endregion
}
