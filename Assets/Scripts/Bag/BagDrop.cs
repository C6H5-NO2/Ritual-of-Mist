using ThisGame.Items;
using ThisGame.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ThisGame.Bag {
    //public class BagDrop : DroppableUI {
    //    public override void OnAfterDrop(DragDropUI scrp) {
    //        base.OnAfterDrop(scrp);
    //        var desc = scrp.GetComponent<ItemDescHolder>().Description;
    //        BagManager.Instance.InBag.EaddNset(desc, 1);
    //        Destroy(scrp.gameObject);
    //    }
    //}

    public class BagDrop : MonoBehaviour, IDropHandler {
        public void OnDrop(PointerEventData eventData) {
            var go = eventData.pointerDrag;
            if(go.CompareTag("DragItem")) {
                var desc = go.GetComponent<ItemDescHolder>().Description;
                BagManager.Instance.InBag.EaddNset(desc, 1);
                Destroy(go);
            }
        }
    }
}
