using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenuSpawner : MonoBehaviour
{
    #region Properties and Fields

    [SerializeField] RadialMenu _menuPrefab;

    #endregion

    #region Unity Methods

    private void OnEnable() => HookMessages();
    private void OnDisable() => UnhookMessages();

    #endregion

    #region Private Methods

    private void HookMessages()
    {
        Messaging<InteractableClicked>.Register(InteractableClicked);
    }
    private void UnhookMessages()
    {
        Messaging<InteractableClicked>.Unregister(InteractableClicked);
    }

    private void InteractableClicked(Interactable interactable, Vector3 mousePosition)
    {
        Messaging<CloseMenu>.Trigger?.Invoke();

        var menu = Instantiate(_menuPrefab) as RadialMenu;
        menu.transform.SetParent(transform, false);
        menu.transform.position = mousePosition;
        menu.SpawnButtons(interactable);
    }

    #endregion
}
