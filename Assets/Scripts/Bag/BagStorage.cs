using System.Collections.Generic;
using UnityEngine;

namespace ThisGame.Bag {
    [CreateAssetMenu(fileName = "DefaultBagStorage", menuName = "Items (new)/Bag Storage")]
    public class BagStorage : ScriptableObject {
        // todo: write to file
        [SerializeField] public Dictionary<Items.ItemDescription, int> inBag;
        public int gold;
    }
}
