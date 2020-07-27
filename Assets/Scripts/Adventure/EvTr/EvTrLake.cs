using ThisGame.Items;
using ThisGame.Utils;

namespace ThisGame.Adventure.EvTr {
    public class EvTrLake : EvTrBase {
        protected override void OnTrigger() {
            if(AnyNameid("fishing_rod_momentum", out _)) {
                SetSuc(0);
                SetSuc(3);
                AddNameid("red_fish");
            }
            else if(AnyNameid("fishing_rod", out _)) {
                SetSuc(0);
                if(RandProb(.5f))
                    SetSuc(1);
                else {
                    SetSuc(2);
                    AddNameid("red_fish");
                }
            }

            var visTime = LocDatDict.Instance.Dict["lake"].VisitTimes;
            if(visTime == 1)
                SetSuc(4);
            else if(visTime >= 2) {
                if(visTime == 2)
                    SetSuc(5);
                SetSuc(6);
                AddNameid("bottle_solution");
            }

            if(events[9].TriggeredTimes == 0 && AnyPropGt(out var idxEn, ItemProperty.Energy)) {
                if(AnyNameid("fishing_rod_momentum", out _)) {
                    SetSuc(9);
                    AddNameid("stone_fish");
                }
                else if(AnyNameid("fishing_rod", out _))
                    SetSuc(8);
                else
                    SetSuc(7);
                ExhaustIf(idxEn);
            }

            if(weather == Weather.Rainy && AnyPropGt(out var idxSp, ItemProperty.Spirit)) {
                SetSuc(10);
                ExhaustIf(idxSp);
            }

            if(AnyNameid("potion_water_elf", out var idxElf)) {
                SetSuc(11);
                ExhaustIf(idxElf);
                AddNameid("red_fish");
            }

            if(events[13].TriggeredTimes == 0 && AnyNameid("potion_water_elf", out idxElf)) {
                SetSuc(12);
                if(AnyPropGt(out var idxMetal, ItemProperty.Metal, 1)) {
                    SetSuc(13);
                    ExhaustIf(idxMetal);
                    AddNameid("mountkey_fragment_l");
                }
                ExhaustIf(idxElf);
            }

            AddHand();
        }
    }
}
