using System.Collections.Generic;
using ThisGame.Items;
using UnityEngine;

namespace ThisGame.Adventure.Events {
    [CreateAssetMenu(fileName = "TwilightForestReport3", menuName = "Adventure/Event/TwilightForestReport3")]
    public class TwilightForestReport3 : LocationEvent {
        public LocationData location;

        public override bool IsSuccess(Dictionary<ItemDescription, int> along, TimeWeather dtw, int[] count) {
            var ret = location.VisitTimes >= 3;
            count[0] = ret ? 1 : 0;
            count[1] = ret ? 1 : 0;
            return ret;
        }
    }
}
