using System.Collections.Generic;
using UnityEngine;

namespace ThisGame.Adventure {
    [CreateAssetMenu(fileName = "LocationEvent", menuName = "Location/Event/LocationEvent")]
    public class LocationEvent : Utils.IdScriptableObject {
        //public LocationData location;
        public Utils.ItemWithCount[] loot;

        public virtual bool IsSuccess(List<Items.ItemDescription> along, TimeWeather dtw) {
            return false;
        }
    }
}
