using UnityEngine;

public delegate void ShowNarrative(string richTextNarrative, float displaySeconds = -1f);
public delegate void PlayAudio(AudioClip audioClip, AudioGroups audioGroup, Transform audioLocation = null);
public delegate void StopAudio(AudioGroups audioGroup);
public delegate void InteractableClicked(Interactable interactable, Vector3 mousePosition);
public delegate void OptionsInteractableClicked(OptionsInteractable interactable, Vector3 mousePosition);
public delegate void CloseMenu();
public delegate void SetCursor(Sprite cursor);
public delegate void GameEvent(string eventName, bool value);
public delegate void ItemEvent(string itemName, string eventName, bool value);
public delegate void StopTimeline();