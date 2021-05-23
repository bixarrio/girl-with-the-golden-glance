using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenu : MonoBehaviour
{
    #region Properties and Fields

    [SerializeField] float _buttonRadius = 16f;
    [SerializeField] RadialButton _buttonPrefab;

    #endregion

    #region Unity Methods

    private void OnEnable() => HookMessages();
    private void OnDisable() => UnhookMessages();

    #endregion

    #region Public Methods

    public void SpawnButtons(Interactable interactable)
    {
        for (int i = 0; i < interactable.MenuOptions.Length; i++)
        {
            var button = Instantiate(_buttonPrefab) as RadialButton;
            button.transform.SetParent(transform);

            var theta = (2f * Mathf.PI / interactable.MenuOptions.Length) * i;
            var xPos = Mathf.Cos(theta);
            var yPos = Mathf.Sin(theta);
            button.transform.localPosition = new Vector3(xPos, yPos, 0f) * _buttonRadius;

            button.SetIcon(interactable.MenuOptions[i].MenuIcon);
            button.SetMyData(this, interactable, interactable.MenuOptions[i]);
        }
    }

    #endregion

    #region Private Methods

    private void HookMessages()
    {
        Messaging<CloseMenu>.Register(CloseMenu);
    }
    private void UnhookMessages()
    {
        Messaging<CloseMenu>.Unregister(CloseMenu);
    }

    private void CloseMenu() => Destroy(gameObject);

    #endregion
}
