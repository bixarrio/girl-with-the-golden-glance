using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    #region Properties and Fields

    [SerializeField] AudioClip[] _music;

    #endregion

    #region Unity Methods

    private void Start()
    {
        var song = _music[Random.Range(0, _music.Length)];
        Messaging<PlayAudio>.Trigger?.Invoke(song, AudioGroups.Music);
    }

    #endregion
}
