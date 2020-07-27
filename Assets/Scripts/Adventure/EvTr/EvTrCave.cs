using ThisGame.Items;
using ThisGame.Letter;
using ThisGame.Utils;

namespace ThisGame.Adventure.EvTr {
    public class EvTrCave : EvTrBase {
        protected override void OnTrigger() {
            SetSuc(0);
            AddNameid(RandProb(.5f) ? "sapphire_big" : "ruby_big");
            AddNameid("iron_ore");

            var visTime = LocDatDict.Instance.Dict["cave"].VisitTimes;
            var visTimeGe2WithRainElfOrSunFogRope = false;
            if(visTime == 1)
                SetSuc(1);
            else if(visTime >= 2) {
                if(visTime == 2)
                    SetSuc(2);

                if(weather == Weather.Rainy) {
                    SetSuc(4);
                    if(AnyNameid("potion_water_elf", out var idxElf)) {
                        SetSuc(6);
                        ExhaustIf(idxElf);
                        visTimeGe2WithRainElfOrSunFogRope = true;
                    }
                }
                else {
                    SetSuc(3);
                    if(AnyNameid("rope", out var idxRope)) {
                        SetSuc(5);
                        ExhaustIf(idxRope);
                        LetterManager.Instance.AddReceivedLetter("letter_cave_1");
                        visTimeGe2WithRainElfOrSunFogRope = true;

                        var trgTime = events[5].TriggeredTimes;
                        if(trgTime == 1)
                            SetSuc(7);
                        else if(trgTime == 2)
                            SetSuc(8);
                    }
                }
            }

            if(visTimeGe2WithRainElfOrSunFogRope) {
                if(AnyPropGt(out var idxMetal, ItemProperty.Metal, 1)) {
                    SetSuc(9);
                    ExhaustIf(idxMetal);
                    AddNameid(RandProb(.25f) ? "moonlight_crystal" : "ruby_small");
                }
                else
                    SetSuc(10);

                if(AnyNameid("key_to_mountain", out _)) {
                    SetSuc(11);
                    if(events[12].TriggeredTimes == 0) {
                        SetSuc(12);
                        AddNameid("heart_of_mountain");
                    }
                    else
                        SetSuc(13);
                }
            }

            AddHand();
        }
    }
}
