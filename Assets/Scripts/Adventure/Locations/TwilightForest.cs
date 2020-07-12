using System.Collections.Generic;
using System.Linq;
using ThisGame.Utils;
using UnityEngine;

namespace ThisGame.Adventure {
    [CreateAssetMenu(fileName = "TwilightForest", menuName = "Adventure/Location/Twilight Forest")]
    public class TwilightForest : LocationData {
        public override (Dictionary<LocationEvent, bool> states, Dictionary<Items.ItemDescription, int> loots)
            ProcessEvents(Dictionary<Items.ItemDescription, int> items, TimeWeather timeweather) {
            var states = new Dictionary<LocationEvent, bool>(events.Length);
            var loots = new Dictionary<Items.ItemDescription, int>();
            var lootLen = int.MinValue;
            foreach(var locationEvent in events) {
                states.Add(locationEvent, false);
                if(lootLen < locationEvent.loot.Length)
                    lootLen = locationEvent.loot.Length;
            }
            var count = new int[lootLen];

            // todo: decouple into logic blocks => tile-matching in Editor
            // todo: assert with id

            {
                var evHunt = events[0];
                if(evHunt.IsSuccess(items, timeweather, count)) {
                    loots.EaddNset(evHunt.loot[0], count[0]);
                    states[evHunt] = true;
                }
                else {
                    // hunt failed
                    // no loot
                    states[events[1]] = true;
                }
            }

            {
                var evElf1 = events[2];
                if(evElf1.IsSuccess(items, timeweather, count)) {
                    // no loot
                    states[evElf1] = true;
                    var evElf2 = events[3];
                    if(evElf2.IsSuccess(items, timeweather, count)) {
                        states[evElf2] = true;
                        loots.EaddNset(evElf2.loot[0], count[0]);
                    }
                }
            }

            {
                var evMushroom = events[4];
                if(evMushroom.IsSuccess(items, timeweather, count)) {
                    states[evMushroom] = true;
                    loots.EaddNset(evMushroom.loot[0], count[0]);
                    loots.EaddNset(evMushroom.loot[1], count[1]);
                }
            }

            {
                var evSpice = events[5];
                if(evSpice.IsSuccess(items, timeweather, count)) {
                    states[evSpice] = true;
                    loots.EaddNset(evSpice.loot[0], count[0]);
                    loots.EaddNset(evSpice.loot[1], count[1]);
                }
            }

            // todo: add to illus book
            var evReport1 = events[6];
            if(evReport1.IsSuccess(items, timeweather, count)) {
                states[evReport1] = true;
                loots.EaddNset(evReport1.loot[0], count[0]);
                //loots.EaddNset(evReport1.loot[1], count[1]);
            }

            var evReport2 = events[7];
            if(evReport2.IsSuccess(items, timeweather, count)) {
                states[evReport2] = true;
                loots.EaddNset(evReport2.loot[0], count[0]);
                //loots.EaddNset(evReport2.loot[1], count[1]);
            }

            var evReport3 = events[8];
            if(evReport3.IsSuccess(items, timeweather, count)) {
                states[evReport3] = true;
                loots.EaddNset(evReport3.loot[0], count[0]);
                //loots.EaddNset(evReport3.loot[1], count[1]);
            }

            foreach(var item in items) {
                var itemDesc = item.Key;
                if(itemDesc.isExhaust)
                    continue;
                loots.EaddNset(itemDesc, item.Value);
            }

            return (states, loots);
        }
    }
}
