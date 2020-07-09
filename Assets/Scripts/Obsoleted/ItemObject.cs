using UnityEngine;

[CreateAssetMenu(fileName = "ItemObject", menuName = "Items/Item Object")]
public class ItemObject : ScriptableObject {
    public new string name;
    public int[] properties = new int[4];
    public bool isExhaust;
    [TextArea] public string info;
    public Sprite image;
    public GameObject prefab;
    public int HeldCount { get; set; }
}
