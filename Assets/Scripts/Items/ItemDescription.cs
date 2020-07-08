using UnityEngine;

namespace ThisGame.Items {
    public enum ItemProperty : byte {
        Metal,
        Spirit,
        Unnamed1,
        Unnamed2,
        Count
    }


    [CreateAssetMenu(fileName = "Item", menuName = "Items (new)/Item Description")]
    public class ItemDescription : Utils.IdScriptableObject {
        public byte[] properties = new byte[(int)ItemProperty.Count];
        public bool isExhaust;
        public GameObject prefabTemplate;


        public GameObject Instantiate(Transform canvas) {
            var go = Instantiate(prefabTemplate, canvas, false);
            var holder = go.GetComponent<ItemDescHolder>();
            if(holder != null) {
                holder.Description = this;
                holder.UpdateDesc();
            }
            return go;
        }
    }
}
