using UnityEngine;

namespace ThisGame.Items {
    public enum ItemProperty : byte { Metal, Spirit, Energy, Food, Count }


    [CreateAssetMenu(fileName = "Item", menuName = "Items/Item Description")]
    public class ItemDescription : Utils.IdScriptableObject {
        public byte[] properties = new byte[(int)ItemProperty.Count];
        public bool isExhaust;
        public int sellPrice;
        public GameObject prefabTemplate;

        public GameObject Instantiate(Transform canvas) {
            var go = Instantiate(prefabTemplate, canvas, false);
            var holder = go.GetComponent<ItemDescHolder>();
            holder.Description = this;
            holder.UpdateDesc();
            return go;
        }
    }
}
