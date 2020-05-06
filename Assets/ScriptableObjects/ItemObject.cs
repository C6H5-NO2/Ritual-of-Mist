using UnityEngine;

[CreateAssetMenu(fileName = "Item Object", menuName = "Items")]
public class ItemObject : ScriptableObject {
    public string itemName;
    public Sprite itemImage;
    public int itemHeldCount;
    [TextArea] public string itemInfo;
    public bool equip;
}
