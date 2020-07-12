using System.Collections.Generic;
using UnityEngine;

namespace ThisGame.Adventure {
    //[CreateAssetMenu(fileName = "Location", menuName = "Adventure/Location/Location Data")]
    public abstract class LocationData : Utils.IdScriptableObject {
        public Vector2 locSymbolPos;
        public int goldCost;
        public float timeCost;

        public LocationEvent[] events;


        // todo: write to file
        public int VisitTimes { get; private set; }

        public void Visit() {
            ++VisitTimes;
        }

        // todo: split data & logic
        public abstract (Dictionary<LocationEvent, bool> states, Dictionary<Items.ItemDescription, int> loots)
            ProcessEvents(Dictionary<Items.ItemDescription, int> items, TimeWeather timeweather);
    }
}
