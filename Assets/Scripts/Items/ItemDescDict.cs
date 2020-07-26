using ThisGame.Utils;
using UnityEngine;

namespace ThisGame.Items {
    [CreateAssetMenu(fileName = "DefaultItemDescDict", menuName = "Items/Item Description Dictionary")]
    public class ItemDescDict : ScriptableObject {
        public static ItemDescDict Instance { get; private set; }

        public IdSoDict<ItemDescription> Dict { get; private set; }

        // todo
        //[RuntimeInitializeOnLoadMethod]
        private static void Init() {
            if(Instance != null)
                return;
            Instance = Resources.Load<ItemDescDict>("SOs/DefaultItemDescDict");
            var sos = Resources.LoadAll<ItemDescription>("SOs/ItemDescription");
            Instance.Dict = new IdSoDict<ItemDescription>(sos, true);
        }
    }
}
