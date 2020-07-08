using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ThisGame.Adventure.Events {
    [CreateAssetMenu(fileName = "TwilightForestElf1", menuName = "Location/Event/TwilightForestElf1")]
    public class TwilightForestElf1 : LocationEvent {
        public override bool IsSuccess(List<Items.ItemDescription> along, TimeWeather dtw) {
            return dtw.weather == TimeWeatherManager.Weather.Sunny
                   && along.Any(item => item.properties[(int)Items.ItemProperty.Spirit] > 0);
        }
    }
}
