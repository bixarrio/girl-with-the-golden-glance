//public delegate void OnInteractableClicked(Interactable interactable);

using UnityEngine;
using UnityEngine.Audio;

public delegate void ShowNarrative(string richTextNarrative, float displaySeconds = -1f);
public delegate void PlayAudio(AudioClip audioClip, AudioGroups audioGroup, Transform audioLocation = null);