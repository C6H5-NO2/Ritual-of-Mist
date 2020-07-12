using System.Collections.Generic;
using UnityEngine;

namespace ThisGame.Adventure.Loaction {
    [CreateAssetMenu(fileName = "WitchHouse", menuName = "Adventure/Location/Witch House")]
    public class WitchHouse : LocationData {
        public override (Dictionary<LocationEvent, bool> states, Dictionary<Items.ItemDescription, int> loots)
            ProcessEvents(Dictionary<Items.ItemDescription, int> items, TimeWeather timeweather) {
            return (null, null);
        }
    }
}
