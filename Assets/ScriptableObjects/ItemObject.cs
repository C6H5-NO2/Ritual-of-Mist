using UnityEngine;

[CreateAssetMenu(fileName = "ItemObject", menuName = "Items/Item Object")]
public class ItemObject : ScriptableObject {
    public string itemName;
    public Sprite itemImage;
    public int itemHeldCount;
    [TextArea] public string itemInfo;
    //public bool equip;
}
