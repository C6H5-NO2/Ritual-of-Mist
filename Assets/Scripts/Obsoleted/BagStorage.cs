using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BagStorage", menuName = "Items/Bag Storage")]
public class BagStorage : ScriptableObject {
    public List<ItemObject> itemList = new List<ItemObject>();
}
