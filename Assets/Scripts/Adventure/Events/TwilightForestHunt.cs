using System.Collections.Generic;
using System.Linq;
using ThisGame.Items;
using UnityEngine;

namespace ThisGame.Adventure.Events {
    [CreateAssetMenu(fileName = "TwilightForestHunt", menuName = "Adventure/Event/TwilightForestHunt")]
    public class TwilightForestHunt : LocationEvent {
        public override bool IsSuccess(Dictionary<ItemDescription, uint> along, TimeWeather dtw, uint[] count) {
            if(!along.Any(kvp => kvp.Key.properties[(int)ItemProperty.Metal] > 2))
                return false;
            count[0] = (uint)Random.Range(1, 3);
            return true;
        }
    }
}
