using System.Collections.Generic;
using UnityEngine;

namespace ThisGame.Adventure.Events {
    [CreateAssetMenu(fileName = "TwilightForestMushroom", menuName = "Location/Event/TwilightForestMushroom")]
    public class TwilightForestMushroom : LocationEvent {
        public override bool IsSuccess(List<Items.ItemDescription> along, TimeWeather dtw) {
            if(dtw.weather != TimeWeatherManager.Weather.Rainy)
                return false;
            const int totalCount = 3;
            for(var i = 0; i < totalCount; ++i)
                ++loot[Random.Range(0, 2)].Count;
            return true;
        }
    }
}
