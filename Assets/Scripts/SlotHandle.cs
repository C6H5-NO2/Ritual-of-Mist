using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotHandle : MonoBehaviour, IPointerClickHandler {
    public ItemObject ItemObj { get; set; }

    private Image itemImg;
    private Text itemCnt;

    private void Start() {
        itemImg = GetComponent<Image>();
        itemCnt = GetComponentInChildren<Text>();

        itemImg.sprite = ItemObj.itemImage;
        itemCnt.text = ItemObj.itemHeldCount.ToString();
    }

    public void OnPointerClick(PointerEventData eventData) {
        BagManager.Instance.itemDescription.text = ItemObj.itemInfo;
    }
}
