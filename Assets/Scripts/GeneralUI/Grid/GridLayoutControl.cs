using System;
using UnityEngine;

namespace ThisGame.GeneralUI {
    public class GridLayoutControl : MonoBehaviour {
        public Transform gridLayout;
        public GameObject gridSlotPrefab;

        public bool HasClickedItem { get; private set; }
        public Action<GridItemControl> OnClickedItemChanged { private get; set; }
        private GridItemControl clickedItem;
        public GridItemControl ClickedItem {
            get => clickedItem;
            set {
                clickedItem = value;
                HasClickedItem = value != null;
                OnClickedItemChanged?.Invoke(value);
            }
        }

        public void Add(Items.ItemDescription desc, int count) {
            var go = Instantiate(gridSlotPrefab, gridLayout, false);
            var ctrl = go.GetComponent<GridItemControl>();
            ctrl.ParentControl = this;
            ctrl.ItemDesc = desc;
            ctrl.ItemCount = count;
        }

        public void Clear() {
            foreach(Transform slot in gridLayout)
                Destroy(slot.gameObject);
            ClickedItem = null;
        }

        public void RemoveClickedItem() {
            if(HasClickedItem)
                Destroy(ClickedItem.gameObject);
            ClickedItem = null;
        }
    }
}
