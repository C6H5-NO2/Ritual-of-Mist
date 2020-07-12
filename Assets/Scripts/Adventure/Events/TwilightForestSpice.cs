using System.Collections.Generic;
using ThisGame.Items;
using UnityEngine;

namespace ThisGame.Adventure.Events {
    [CreateAssetMenu(fileName = "TwilightForestSpice", menuName = "Adventure/Event/TwilightForestSpice")]
    public class TwilightForestSpice : LocationEvent {
        public override bool IsSuccess(Dictionary<ItemDescription, int> along, TimeWeather dtw, int[] count) {
            const float sucProb = .5f;
            if(Random.Range(.0f, 1) < sucProb)
                return false;
            const int totalCount = 2;
            count[0] = count[1] = 0;
            for(var i = 0; i < totalCount; ++i)
                ++count[Random.Range(0, 2)];
            return true;
        }
    }
}
