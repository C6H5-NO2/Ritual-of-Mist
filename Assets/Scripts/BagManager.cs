using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BagManager : MonoBehaviour {
    public BagStorage bagStorage;
    public GameObject bagPanelGrid;
    public Text itemDescription;
    public GameObject slotPrefab;

    public static BagManager Instance { get; private set; }

    public bool IsDirty { get; private set; }

    public void AddItem(ItemObject itemObject, int num = 1) {
        if(itemObject.itemHeldCount == 0)
            bagStorage.itemList.Add(itemObject);
        itemObject.itemHeldCount += num;
        IsDirty = true;
        RefreshBagUI();
    }

    public void RemoveItem(ItemObject itemObject, int num = 1) {
        itemObject.itemHeldCount -= num;
        if(itemObject.itemHeldCount <= 0)
            bagStorage.itemList.Remove(itemObject);
        IsDirty = true;
        RefreshBagUI();
    }

    [ContextMenu("Clear items in bag")]
    public void ClearAll() {
        bagStorage.itemList.ForEach(item => item.itemHeldCount = 0);
        bagStorage.itemList.Clear();
        IsDirty = true;
        RefreshBagUI();
    }

    public void RefreshBagUI() {
        if(!bagPanelGrid.activeInHierarchy) return;
        itemDescription.text = "";
        if(!IsDirty) return;

        var gridChildren = new List<GameObject>(from Transform child in bagPanelGrid.transform select child.gameObject);
        gridChildren.ForEach(Destroy);

        foreach(var itemObject in bagStorage.itemList) {
            var slot = Instantiate(slotPrefab, bagPanelGrid.transform);
            var slotHandle = slot.GetComponent<SlotHandle>();
            slotHandle.ItemObj = itemObject;
        }

        IsDirty = false;
    }

    private void Awake() {
        if(Instance) {
            Destroy(this);
            return;
        }

        Instance = this;
    }
}
