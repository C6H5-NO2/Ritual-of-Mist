using System.Collections.Generic;
using ThisGame.Items;
using ThisGame.Utils;

namespace ThisGame.Adventure.EvTr {
    public abstract class EvTrBase : IEventTrigger {
        protected Zeit time;
        protected Weather weather;
        protected (uint, uint, uint) hand;

        protected EventData[] events;
        protected (bool, bool, bool) exhaustMask;
        protected IdSoDict<ItemDescription> itemDict;

        private bool[] success;
        private List<uint> loots;


        public bool[] Trigger(EventData[] events, Zeit time, Weather weather, (uint, uint, uint) hand,
                              out List<uint> loots) {
            this.time = time;
            this.weather = weather;
            this.hand = hand;

            this.events = events;
            exhaustMask = (false, false, false);
            itemDict = ItemDescDict.Instance.Dict;

            success = new bool[events.Length];
            this.loots = new List<uint>();

            OnTrigger();

            loots = this.loots;
            return success;
        }


        public static bool RandProb(float prob) => UnityEngine.Random.Range(.0f, 1) <= prob;


        protected void SetSuc(int idx) {
            success[idx] = true;
            events[idx].OnTrigger();
        }


        protected bool AnyNameid(string nameid, out int idx)
            => hand.Any(itemDict.NameidToId(nameid), out idx);


        protected bool AnyPropGt(out int idx, ItemProperty prop, byte val = 0) {
            for(var i = 0; i < 3; ++i) {
                var id = hand.At(i);
                if(id != 0 && itemDict[id].properties[(int)prop] > val) {
                    idx = i;
                    return true;
                }
            }
            idx = -1;
            return false;
        }


        protected void ExhaustIf(int idx) {
            if(itemDict[hand.At(idx)].isExhaust)
                exhaustMask.At(idx) = true;
        }


        protected void AddNameid(string nameid)
            => loots.Add(itemDict.NameidToId(nameid));


        protected void AddHand() {
            for(var i = 0; i < 3; ++i)
                if(hand.At(i) != 0 && !exhaustMask.At(i))
                    loots.Add(hand.At(i));
        }


        protected abstract void OnTrigger();
    }
}
