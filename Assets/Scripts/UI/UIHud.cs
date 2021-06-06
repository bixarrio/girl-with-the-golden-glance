using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHud : MonoBehaviour
{
    #region Properties and Fields

    private static UIHud _instance;
    public static UIHud Instance => _instance;

    [SerializeField] GameObject _pauseMenu;

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

    #endregion

    #region Public Methods

    public void OpenInventory() => UIInventoryGroup.Instance.OpenInventory();

    public void OpenPauseMenu()
    {
        Time.timeScale = 0f;
        _pauseMenu.gameObject.SetActive(true);
    }
    public void ClosePauseMenu()
    {
        _pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneTransition.Instance.DoTransition("Menu", Cleanup, TransitionType.Radial);
    }

    #endregion

    #region Private Methods

    private void Cleanup()
    {
        KillBucky(); // Kill bucky and his inventory
        GameEventController.Instance.ResetEvents(); // Clean the events
        Destroy(gameObject); // Kill the HUD
    }

    private void KillBucky()
    {
        Destroy(CharacterMovementController.Instance.gameObject);
        Destroy(UIInventoryGroup.Instance.gameObject);
    }

    #endregion
}
