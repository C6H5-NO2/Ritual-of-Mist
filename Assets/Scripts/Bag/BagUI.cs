using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <remarks> add to BagPanelUI </remarks>
public class BagUI : MonoBehaviour {
    public GameObject slotPrefab;

    private Text itemInfo, itemProperties;
    private Transform gridTrans;
    private Button takeOutButton;

    private SlotHandle focusedSlot;
    public SlotHandle FocusedSlot {
        set {
            focusedSlot = value;
            UpdateInfoUI();
        }
    }

    public void UpdateInfoUI() {
        if(!gameObject.activeInHierarchy)
            return;
        if(focusedSlot == null)
            ClearInfoUI();

        itemInfo.text = focusedSlot.ItemObj.info;

        var props = focusedSlot.ItemObj.properties;
        var sb = new StringBuilder();
        sb.AppendLine("Wasser: " + props[0]);
        sb.AppendLine("Erde: " + props[1]);
        sb.AppendLine("Feuer: " + props[2]);
        sb.AppendLine("Luft: " + props[3]);
        itemProperties.text = sb.ToString();
    }

    public void ClearInfoUI() {
        itemInfo.text = "";
        itemProperties.text = "";
    }

    // todo: optimize
    public void RefreshGrid() {
        if(!gameObject.activeInHierarchy)
            return;

        var gridChildren = new List<GameObject>(from Transform child in gridTrans select child.gameObject);
        gridChildren.ForEach(Destroy);

        foreach(var itemObject in BagManager.Instance.bagStorage.itemList) {
            var slot = Instantiate(slotPrefab, gridTrans);
            var slotHandle = slot.GetComponent<SlotHandle>();
            slotHandle.ItemObj = itemObject;
        }
    }

    private void OnTakeOutButtonClicked() {
        if(focusedSlot == null)
            return;
        BagManager.Instance.RemoveItem(focusedSlot.ItemObj);

        Instantiate(focusedSlot.ItemObj.prefab, 2.4f * Random.insideUnitCircle, Quaternion.identity);
        if(focusedSlot.ItemObj.HeldCount == 0) {
            focusedSlot = null;
            ClearInfoUI();
        }
    }

    private void OnEnable() {
        ClearInfoUI();
        RefreshGrid();
        takeOutButton.onClick.AddListener(OnTakeOutButtonClicked);
    }

    private void OnDisable() {
        takeOutButton.onClick.RemoveAllListeners(); // only listen to this
    }

    private void Awake() {
        itemInfo = transform.Find("ItemInfo").GetComponent<Text>();
        itemProperties = transform.Find("ItemProperties").GetComponent<Text>();
        gridTrans = transform.Find("Grid");
        takeOutButton = transform.Find("TakeOut").GetComponent<Button>();
    }
}
