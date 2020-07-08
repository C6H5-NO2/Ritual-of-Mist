using System.Collections.Generic;
using UnityEngine;

namespace ThisGame.Adventure.Events {
    [CreateAssetMenu(fileName = "TwilightForestSpice", menuName = "Location/Event/TwilightForestSpice")]
    public class TwilightForestSpice : LocationEvent {
        public override bool IsSuccess(List<Items.ItemDescription> along, TimeWeather dtw) {
            const float sucProb = .5f;
            if(Random.Range(.0f, 1) < sucProb)
                return false;
            const int totalCount = 2;
            for(var i = 0; i < totalCount; ++i)
                ++loot[Random.Range(0, 2)].Count;
            return true;
        }
    }
}
