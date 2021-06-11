using UnityEngine;

[CreateAssetMenu(menuName = "GWTGG/Item")]
public class Item : ScriptableObject
{
    #region Properties and Fields

    public string ItemName;
    [TextArea] public string ItemDescription;
    public AudioClip NarrativeAudio;
    public Sprite InventoryIcon;
    public GameObject ItemPrefab;
    public ItemDropCondition DropCondition;

    #endregion
}
