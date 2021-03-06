using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    #region Properties and Fields

    [SerializeField] GameObject _bucky;
    [SerializeField] GameObject _cutty;

    #endregion

    #region Unity Methods

    private void OnEnable()
    {
        var bucky = GameObject.Find("Buckshot Jones");
        if (bucky == null)
            Instantiate(_bucky, Vector3.zero, Quaternion.identity, transform);
    }

    #endregion

    #region Public Methods

    public void PlayClicked()
    {
        // Cutscene should go hete
        SceneTransition.Instance.DoTransition("Exterior - L00", null, TransitionType.Fade);
    }
    public void AudioClicked()
    {
        SceneTransition.Instance.DoTransition("Audio", KillBucky, TransitionType.Fade);
    }
    public void BrightnessClicked()
    {
        Messaging<GameEvent>.Trigger?.Invoke("From Menu", true);
        SceneTransition.Instance.DoTransition("Brightness", KillBucky, TransitionType.Fade);
    }
    public void IntroClicked()
    {
        void Cleanup()
        {
            Instantiate(_cutty, Vector3.zero, Quaternion.identity, transform);
            var bucky = GameObject.Find("Buckshot Jones");
            Destroy(bucky);

            Messaging<StopAudio>.Trigger?.Invoke(AudioGroups.Music);
        }

        SceneTransition.Instance.DoTransition("Intro Cutscene", Cleanup, TransitionType.Fade);
    }

    public void QuitClicked()
    {
        if (Application.platform != RuntimePlatform.WebGLPlayer)
            Application.Quit();
    }

    #endregion

    #region Private Methods

    private void KillBucky()
    {
        Destroy(CharacterMovementController.Instance.gameObject);
        Destroy(UIInventoryGroup.Instance.gameObject);
    }

    #endregion
}
