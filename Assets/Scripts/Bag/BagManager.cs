using System.Collections.Generic;
using ThisGame.Items;
using ThisGame.Utils;

namespace ThisGame.Bag {
    public class BagManager : SingletonManager<BagManager> {
        public BagStorage bagStorage;

        // todo: write back
        private Dictionary<ItemDescription, int> inBag;
        public Dictionary<ItemDescription, int> InBag {
            get => inBag;
            set {
                inBag = value;
                // todo: add callback
            }
        }


        private int gold;
        public int Gold {
            get => gold;
            set {
                gold = value;
                // todo: add callback
            }
        }


        protected override void OnInstanceAwake() {
            if(bagStorage == null) {
                inBag = new Dictionary<ItemDescription, int>();
                var dict = ItemDescDict.Instance.Dict;
                inBag.EaddNset(dict["strange_pipe"], 1);
                inBag.EaddNset(dict["bottle_solution"], 3);
                inBag.EaddNset(dict["iron_blade"], 1);
                inBag.EaddNset(dict["fishing_rod"], 1);
                gold = 500;
            }
            else {
                inBag = bagStorage.inBag;
                gold = bagStorage.gold;
            }
        }
    }
}
