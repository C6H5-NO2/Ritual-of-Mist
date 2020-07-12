using System.Collections.Generic;
using ThisGame.Items;
using UnityEngine;

namespace ThisGame.Adventure {
    [CreateAssetMenu(fileName = "PlaceHolderEvent", menuName = "Adventure/Event/Place Holder Event")]
    public class LocationEvent : Utils.IdScriptableObject {
        public ItemDescription[] loot;

        // todo: split data & logic
        ///// <param name="count"># of each loot if return true. Otherwise temp value. Modified in the function.</param>
        public virtual bool IsSuccess(Dictionary<ItemDescription, int> along, TimeWeather dtw, int[] count) {
            for(var i = count.Length - 1; i >= 0; --i)
                count[i] = 0;
            return false;
        }
    }
}
