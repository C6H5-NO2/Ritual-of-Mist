using UnityEngine;
using System.Collections;

public class BagManager : MonoBehaviour {
    public static BagManager Instance { get; private set; }

    private void Awake() {
        if(Instance)
            Destroy(this);
        Instance = this;
    }

    // todo
    public void AddNewItem(ItemObject item) {
        Debug.Log("Add new item: " + item.itemName);
    }

    // todo
    public bool Contain() { return true; }

    // todo
    public void RefreshItems() { }

    // todo
    public void CreateNewItem() { }
}
