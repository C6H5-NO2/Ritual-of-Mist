using System.Collections.Generic;
using ThisGame.Items;
using UnityEngine;

namespace ThisGame.Adventure.Events {
    [CreateAssetMenu(fileName = "TwilightForestReport2", menuName = "Adventure/Event/TwilightForestReport2")]
    public class TwilightForestReport2 : LocationEvent {
        public LocationData location;

        public override bool IsSuccess(Dictionary<ItemDescription, uint> along, TimeWeather dtw, uint[] count) {
            var ret = location.VisitTimes == 2;
            count[0] = ret ? 1u : 0u;
            count[1] = ret ? 1u : 0u;
            return ret;
        }
    }
}
