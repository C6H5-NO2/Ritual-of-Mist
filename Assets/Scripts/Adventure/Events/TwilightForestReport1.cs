using System.Collections.Generic;
using ThisGame.Items;
using UnityEngine;

namespace ThisGame.Adventure.Events {
    [CreateAssetMenu(fileName = "TwilightForestReport1", menuName = "Adventure/Event/TwilightForestReport1")]
    public class TwilightForestReport1 : LocationEvent {
        public LocationData location;

        public override bool IsSuccess(Dictionary<ItemDescription, int> along, TimeWeather dtw, int[] count) {
            var ret = location.VisitTimes == 1;
            count[0] = ret ? 1 : 0;
            count[1] = ret ? 1 : 0;
            return ret;
        }
    }
}
