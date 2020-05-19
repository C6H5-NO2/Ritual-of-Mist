using UnityEngine;

/// <remarks> add to GameManager </remarks>
public class BagManager : MonoBehaviour {
    public BagStorage bagStorage;
    public BagUI bagUI;

    public static BagManager Instance { get; private set; }

    //public bool IsDirty { get; private set; }

    public void AddItem(ItemObject itemObject, int num = 1) {
        if(itemObject.HeldCount == 0)
            bagStorage.itemList.Add(itemObject);
        itemObject.HeldCount += num;
        //IsDirty = true;
        bagUI.RefreshGrid();
    }

    public void RemoveItem(ItemObject itemObject, int num = 1) {
        if(itemObject.HeldCount < num)
            return;
        itemObject.HeldCount -= num;
        if(itemObject.HeldCount == 0)
            bagStorage.itemList.Remove(itemObject);
        //IsDirty = true;
        bagUI.RefreshGrid();
    }

    [ContextMenu("Clear items in bag")]
    public void ClearAll() {
        bagStorage.itemList.ForEach(item => item.HeldCount = 0);
        bagStorage.itemList.Clear();
        //IsDirty = true;
        bagUI.RefreshGrid();
    }

    private void Awake() {
        if(Instance) {
            Destroy(this);
            return;
        }

        Instance = this;
    }
}
