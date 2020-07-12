using System.Collections.Generic;
using ThisGame.Items;
using UnityEngine;

namespace ThisGame.Adventure.Events {
    [CreateAssetMenu(fileName = "TwilightForestMushroom", menuName = "Adventure/Event/TwilightForestMushroom")]
    public class TwilightForestMushroom : LocationEvent {
        public override bool IsSuccess(Dictionary<ItemDescription, int> along, TimeWeather dtw, int[] count) {
            if(dtw.weather != TimeWeatherManager.Weather.Rainy)
                return false;
            const int totalCount = 3;
            count[0] = count[1] = 0;
            for(var i = 0; i < totalCount; ++i)
                ++count[Random.Range(0, 2)];
            return true;
        }
    }
}
