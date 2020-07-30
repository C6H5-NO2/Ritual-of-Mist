using System.Collections.Generic;
using ThisGame.Items;
using ThisGame.Utils;

namespace ThisGame.Craft {
    public class CraftFormula : SingletonManager<CraftFormula> {
        private List<(uint, uint, uint)> formulae;
        private IdSoDict<ItemDescription> itemDict;


        public void AddFormula(string product, string ingredient0, string ingredient1)
            => AddFormula(itemDict.NameidToId(product),
                          itemDict.NameidToId(ingredient0),
                          itemDict.NameidToId(ingredient1));

        public void AddFormula(uint product, uint ingredient0, uint ingredient1)
            => formulae.Add((product, ingredient0, ingredient1));


        /// <returns> id of product. 0 if failed. </returns>
        public uint CheckIngredient(uint ingredient0, uint ingredient1) {
            foreach(var formula in formulae) {
                var (p, r0, r1) = formula;
                if((ingredient0 == r0 && ingredient1 == r1) || (ingredient0 == r1 && ingredient1 == r0))
                    return p;
            }
            return 0;
        }


        protected override void OnInstanceAwake() {
            formulae = new List<(uint, uint, uint)>();
            itemDict = ItemDescDict.Instance.Dict;

            AddFormula("crystal_blade", "iron_blade", "mixed_crystal");
            AddFormula("ice_blade", "crystal_blade", "ice");
            AddFormula("energywand", "woodwand", "ruby_small");
            AddFormula("fishing_rod_momentum", "fishing_rod", "ruby_small");
            AddFormula("key_to_mountain", "mountkey_fragment_l", "mountkey_fragment_r");
            AddFormula("mountkey_fragment_r", "iron_blade", "stone_fish");
            AddFormula("mountkey_fragment_r", "crystal_blade", "stone_fish");
            AddFormula("mountkey_fragment_r", "ice_blade", "stone_fish");
        }
    }
}
