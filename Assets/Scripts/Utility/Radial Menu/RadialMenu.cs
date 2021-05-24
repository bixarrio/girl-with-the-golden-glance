using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenu : MonoBehaviour
{
    #region Properties and Fields

    [SerializeField] float _buttonRadius = 16f;
    [SerializeField] float _flyoutSpeed = 0.1f;
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

            button.SetIcon(interactable.MenuOptions[i].MenuIcon);
            button.SetMyData(this, interactable, interactable.MenuOptions[i]);

            StartCoroutine(FlyoutButton(button, GetButtonPosition(i, interactable.MenuOptions.Length)));
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

    private Vector2 GetButtonPosition(int index, int count)
    {
        const float TAU = 6.28318530718f; // Circumference of a circle in radians
        const float RAD90 = TAU / 4f; // 90 degrees in radians

        var theta = (TAU / count) * index + RAD90; // offset by 90 to put the first item on top
        var xPos = Mathf.Cos(theta);
        var yPos = Mathf.Sin(theta);
        return new Vector2(xPos, yPos) * _buttonRadius;
    }

    private IEnumerator FlyoutButton(RadialButton button, Vector3 targetPos)
    {
        var timer = 0f;
        button.transform.localPosition = Vector3.zero;
        button.transform.localScale = Vector3.zero;
        var time = 0f;
        while ((time = timer / _flyoutSpeed) <= 1f)
        {
            var pos = Vector3.Lerp(Vector3.zero, targetPos, time);
            var size = Vector3.Lerp(Vector3.zero, Vector3.one, time);
            
            button.transform.localPosition = pos;
            button.transform.localScale = size;

            timer += Time.deltaTime;
            yield return null;
        }
        button.transform.localPosition = targetPos;
        button.transform.localScale = Vector3.one;
    }

    private void CloseMenu() => Destroy(gameObject);

    #endregion
}
