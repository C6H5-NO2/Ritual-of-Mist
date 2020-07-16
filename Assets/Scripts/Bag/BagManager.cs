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


        protected override void Awake() {
            base.Awake();
            if(bagStorage == null) {
                inBag = new Dictionary<ItemDescription, int>();
                gold = 500; //0;
            }
            else {
                inBag = bagStorage.inBag;
                gold = bagStorage.gold;
            }
        }
    }
}
