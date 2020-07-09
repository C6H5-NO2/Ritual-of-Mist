using System.Collections.Generic;
using System.Linq;
using ThisGame.Items;
using UnityEngine;

namespace ThisGame.Adventure.Events {
    [CreateAssetMenu(fileName = "TwilightForestElf1", menuName = "Adventure/Event/TwilightForestElf1")]
    public class TwilightForestElf1 : LocationEvent {
        public override bool IsSuccess(Dictionary<ItemDescription, uint> along, TimeWeather dtw, uint[] count) {
            return dtw.weather == TimeWeatherManager.Weather.Sunny
                   && along.Any(kvp => kvp.Key.properties[(int)ItemProperty.Spirit] > 0);
        }
    }
}
