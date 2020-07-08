using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ThisGame.Adventure.Events {
    [CreateAssetMenu(fileName = "TwilightForestElf2", menuName = "Location/Event/TwilightForestElf2")]
    public class TwilightForestElf2 : LocationEvent {
        public override bool IsSuccess(List<Items.ItemDescription> along, TimeWeather dtw) {
            //                   todo: use id
            if(along.All(item => item.nameid != "potion_water_elf"))
                return false;
            loot[0].Count = 1;
            return true;
        }
    }
}
