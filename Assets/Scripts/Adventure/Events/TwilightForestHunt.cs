using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ThisGame.Adventure.Events {
    [CreateAssetMenu(fileName = "TwilightForestHunt", menuName = "Location/Event/TwilightForestHunt")]
    public class TwilightForestHunt : LocationEvent {
        public override bool IsSuccess(List<Items.ItemDescription> along, TimeWeather dtw) {
            if(!along.Any(item => item.properties[(int)Items.ItemProperty.Metal] > 2))
                return false;
            loot[0].Count = Random.Range(1, 3);
            return true;
        }
    }
}
