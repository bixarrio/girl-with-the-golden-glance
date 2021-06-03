using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryNeighborAnimationEventHandler : MonoBehaviour
{
    #region Properties and Fields

    [SerializeField] AudioClip _rifleShot;

    #endregion

    #region Public Methods

    public void PlayRifleShot()
    {
        Messaging<PlayAudio>.Trigger?.Invoke(_rifleShot, AudioGroups.SFX, transform);
    }

    public void KillBucky()
    {
        CharacterObjects.Instance.Ragdoll.SetActive(true);
        CharacterObjects.Instance.Bucky.SetActive(false);
    }

    #endregion
}
