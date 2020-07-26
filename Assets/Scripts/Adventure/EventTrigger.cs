using System;
using System.Collections.Generic;
using ThisGame.Items;
using ThisGame.Utils;

namespace ThisGame.NewAdv {
    public static class EvTrUtils {
        public static bool AnyPropGt(ref (uint, uint, uint) hand, ItemProperty prop, byte val = 0,
                                     IdSoDict<ItemDescription> dict = null) {
            if(dict is null)
                dict = ItemDescDict.Instance.Dict;
            var (h0, h1, h2) = hand;
            if(h0 != 0 && dict[h0].properties[(int)prop] > val) {
                // todo: error code:
                // check if exhaust
                hand.Item1 = 0;
                return true;
            }
            if(h1 != 0 && dict[h1].properties[(int)prop] > val) {
                hand.Item2 = 0;
                return true;
            }
            if(h2 != 0 && dict[h2].properties[(int)prop] > val) {
                hand.Item3 = 0;
                return true;
            }
            return false;
        }

        public static bool AnyId(ref (uint, uint, uint) hand, uint id) {
            var (h0, h1, h2) = hand;
            return h0 == id || h1 == id || h2 == id;
        }
    }

    public class EventTriggerMountian : IEventTrigger {
        public bool[] Trigger(EventData[] events, Zeit time, Weather weather, (uint, uint, uint) hand,
                              out List<uint> loots) {
            loots = new List<uint>();
            var success = new bool[events.Length];

            void Succeed(int idx) {
                success[idx] = true;
                events[idx].OnSucceed();
            }

            var hasEnergy = EvTrUtils.AnyPropGt(ref hand, ItemProperty.Energy);
            if(hasEnergy) {
                Succeed(1);
                Succeed(2);
                if(UnityEngine.Random.Range(.0f, 1) < .3f) {
                    Succeed(3);
                    // todo: pass id of 坚冰
                    loots.Add(0);
                }
                //                   todo: pass id of 黄金六分仪
                if(time.IstAbend() && EvTrUtils.AnyId(ref hand, 0)) {
                    Succeed(4);
                    Succeed(5);
                    //     todo: pass id of 道标
                    LocDatDict.Instance.Dict[0].Available = true;
                }
            }
            else {
                Succeed(0);
                // todo: trigger letter
            }


            return success;
        }
    }
}
