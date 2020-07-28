using ThisGame.Items;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ThisGame.Brew {
    /// <summary> add to PotArea </summary>
    public class PotDrop : MonoBehaviour, IDropHandler {
        public BrewUI brewUI;

        public void OnDrop(PointerEventData eventData) {
            var go = eventData.pointerDrag;
            if(go.CompareTag("DragItem")) {
                var holder = go.GetComponent<ItemDescHolder>();
                brewUI.OnDropToPot(holder);
            }
        }
    }
}
