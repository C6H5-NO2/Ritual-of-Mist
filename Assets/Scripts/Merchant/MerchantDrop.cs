using ThisGame.Bag;
using ThisGame.Items;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ThisGame.Merchant {
    public class MerchantDrop : MonoBehaviour, IDropHandler {
        public void OnDrop(PointerEventData eventData) {
            var go = eventData.pointerDrag;
            if(go.CompareTag("DragItem")) {
                var price = go.GetComponent<ItemDescHolder>().Description.sellPrice;
                if(price >= 0) {
                    BagManager.Instance.Gold += price;
                    Destroy(go);
                }
            }
        }
    }
}
