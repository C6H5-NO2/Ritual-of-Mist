using System.Collections.Generic;
using ThisGame.Items;
using ThisGame.Utils;

namespace ThisGame.Brew {
    /// <summary> stupid unity cannot serialize value tuple </summary>
    public class BrewFormula : SingletonManager<BrewFormula> {
        private List<(uint, uint, uint, uint, TimeConstraint, Weather)> formulae;
        private IdSoDict<ItemDescription> itemDict;


        public void AddFormula(string product, string ingredient0, string ingredient1)
            => AddFormula(itemDict.NameidToId(product),
                          itemDict.NameidToId(ingredient0),
                          itemDict.NameidToId(ingredient1));

        public void AddFormula(string product, string ingredient0, string ingredient1, string ingredient2)
            => AddFormula(itemDict.NameidToId(product), itemDict.NameidToId(ingredient0),
                          itemDict.NameidToId(ingredient1), itemDict.NameidToId(ingredient2));

        public void AddFormula(string product, string ingredient0, string ingredient1, string ingredient2,
                               TimeConstraint timeconstraint, Weather weatheconstraint)
            => AddFormula(itemDict.NameidToId(product), itemDict.NameidToId(ingredient0),
                          itemDict.NameidToId(ingredient1), itemDict.NameidToId(ingredient2),
                          timeconstraint, weatheconstraint);

        public void AddFormula(uint product, uint ingredient0, uint ingredient1, uint ingredient2 = 0,
                               TimeConstraint timeconstraint = TimeConstraint.None,
                               Weather weatheconstraint = Weather.Count)
            => formulae.Add((product, ingredient0, ingredient1, ingredient2, timeconstraint, weatheconstraint));


        /// <returns> id of product. 0 if failed. </returns>
        public uint CheckIngredient(uint ingredient0, uint ingredient1, uint ingredient2,
                                    Zeit time, Weather weather) {
            foreach(var formula in formulae) {
                var (p, r0, r1, r2, tc, wc) = formula;
                if(!RandomMatch(ingredient0, ingredient1, ingredient2, r0, r1, r2))
                    continue;
                bool tcMatched = true, wcMatched = true;
                switch(tc) {
                    case TimeConstraint.Day:
                        tcMatched = time.IstTag();
                        break;
                    case TimeConstraint.Night:
                        tcMatched = time.IstAbend();
                        break;
                }
                switch(wc) {
                    case Weather.Foggy:
                    case Weather.Rainy:
                    case Weather.Sunny:
                        wcMatched = wc == weather;
                        break;
                }
                if(tcMatched && wcMatched)
                    return p;
            }
            return 0;
        }


        private bool FixedMatch(uint i0, uint i1, uint i2, uint r0, uint r1, uint r2)
            => i0 == r0 && i1 == r1 && i2 == r2;

        private bool RandomMatch(uint i0, uint i1, uint i2, uint r0, uint r1, uint r2)
            => FixedMatch(i0, i1, i2, r0, r1, r2) || FixedMatch(i0, i2, i1, r0, r1, r2) ||
               FixedMatch(i1, i0, i2, r0, r1, r2) || FixedMatch(i1, i2, i0, r0, r1, r2) ||
               FixedMatch(i2, i0, i1, r0, r1, r2) || FixedMatch(i2, i1, i0, r0, r1, r2);


        protected override void Awake() {
            if(Instance == null) {
                formulae = new List<(uint, uint, uint, uint, TimeConstraint, Weather)>();
                itemDict = ItemDescDict.Instance.Dict;

                AddFormula("flavoured_fish", "red_fish", "macadamia", "bloodberry");
                AddFormula("meat_roll", "meat_1", "meat_2", "pupafruit");
                AddFormula("grilled_mushroom", "ring_mushroom", "angelica");
                AddFormula("potion_dragon_breath", "ruby_big", "bloodberry", "mysterious_scale",
                           TimeConstraint.None, Weather.Rainy);
                AddFormula("potion_of_woods", "pupafruit", "acorn", "moonlight_crystal",
                           TimeConstraint.Night, Weather.Sunny);
                AddFormula("woodwand", "branch_hard", "angelica", "macadamia");
                AddFormula("iron_blade", "iron_ore", "iron_ore", "spirit_mould");
                AddFormula("mixed_crystal", "iron_ore", "sapphire_big", "spirit_mould");
                AddFormula("rope", "branch_hard", "macadamia");
            }
            base.Awake();
        }
    }
}
