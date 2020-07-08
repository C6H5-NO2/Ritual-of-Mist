using ThisGame.Items;
using UnityEngine;

namespace ThisGame.Utils {
    [System.Serializable]
    public class ItemWithCount {
        public ItemWithCount(ItemDescription item = null) {
            if(this.item == null)
                this.item = item;
            Count = 0;
        }

        [SerializeField] private ItemDescription item;
        public ItemDescription Item => item;

        public int Count { get; set; }
    }
}
