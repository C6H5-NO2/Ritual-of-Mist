using System.Collections.Generic;
using System.Linq;
using ThisGame.Items;
using UnityEngine;

namespace ThisGame.Adventure.Events {
    [CreateAssetMenu(fileName = "TwilightForestElf2", menuName = "Adventure/Event/TwilightForestElf2")]
    public class TwilightForestElf2 : LocationEvent {
        public override bool IsSuccess(Dictionary<ItemDescription, int> along, TimeWeather dtw, int[] count) {
            //                   todo: use id
            if(along.All(kvp => kvp.Key.nameid != "potion_water_elf"))
                return false;
            count[0] = 1;
            return true;
        }
    }
}
