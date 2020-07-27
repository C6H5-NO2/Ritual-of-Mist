using ThisGame.Items;
using ThisGame.Letter;
using ThisGame.Utils;

namespace ThisGame.Adventure.EvTr {
    public class EvTrForest : EvTrBase {
        protected override void OnTrigger() {
            SetSuc(0);
            if(RandProb(.3f))
                AddNameid("branch_hard");
            else
                AddNameid(RandProb(.5f) ? "macadamia" : "bloodberry");

            if(AnyPropGt(out var idxMetal, ItemProperty.Metal)) {
                SetSuc(1);
                ExhaustIf(idxMetal);
                AddNameid("meat_2");
            }

            if(weather == Weather.Sunny) {
                SetSuc(2);
                AddNameid(RandProb(.5f) ? "angelica" : "branch_hard");
                AddNameid(RandProb(.5f) ? "macadamia" : "bloodberry");
            }
            else if(weather == Weather.Rainy) {
                SetSuc(3);
                AddNameid(RandProb(.5f) ? "glowing_mushroom" : "ring_mushroom");
            }

            var locDict = LocDatDict.Instance.Dict;
            var visTime = locDict["forest"].VisitTimes;
            if(visTime == 2) {
                SetSuc(4);
                AddNameid("branch_hard");
            }
            else if(visTime == 3) {
                SetSuc(5);
                LetterManager.Instance.AddReceivedLetter("letter_forest_1");
                AddNameid("macadamia");
            }
            else if(visTime > 3 && weather == Weather.Sunny && AnyPropGt(out idxMetal, ItemProperty.Metal)) {
                SetSuc(6);
                ExhaustIf(idxMetal);
                locDict["witch_house"].Available = true;
            }

            if(AnyPropGt(out var idxFood, ItemProperty.Food)) {
                SetSuc(7);
                ExhaustIf(idxFood);
                AddNameid(RandProb(.5f) ? "macadamia" : "bloodberry");
            }

            if(AnyPropGt(out var idxSpirit, ItemProperty.Spirit)) {
                SetSuc(8);
                ExhaustIf(idxSpirit);
                if(AnyNameid("potion_water_elf", out var idxElf)) {
                    SetSuc(9);
                    ExhaustIf(idxElf);
                    locDict["deep_forest"].Available = true;
                }
            }

            AddHand();
        }
    }
}
