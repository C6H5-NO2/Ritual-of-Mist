using ThisGame.Items;
using ThisGame.Letter;
using ThisGame.Utils;

namespace ThisGame.Adventure.EvTr {
    public class EvTrRuin : EvTrBase {
        protected override void OnTrigger() {
            SetSuc(0);
            LetterManager.Instance.AddReceivedLetter("letter_ruin_1");

            var locDict = LocDatDict.Instance.Dict;
            var visTime = locDict["ruin"].VisitTimes;
            if(visTime == 1)
                SetSuc(1);
            else {
                SetSuc(2);
                AddNameid("spirit_mould");
                if(visTime == 3)
                    SetSuc(3);
                if(visTime >= 4) {
                    SetSuc(4);

                    int handCount = 0, firstIdx = 0;
                    for(var i = 0; i < 3; ++i)
                        if(hand.At(i) != 0) {
                            ++handCount;
                            firstIdx = i;
                        }
                    if(handCount == 1) {
                        SetSuc(5);
                        var prop = itemDict[hand.At(firstIdx)].properties;
                        if(prop[(int)ItemProperty.Metal] > 0)
                            WeatherManager.Instance.ForceSetWeather(Weather.Foggy);
                        else if(prop[(int)ItemProperty.Energy] > 0)
                            WeatherManager.Instance.ForceSetWeather(Weather.Sunny);
                        else if(prop[(int)ItemProperty.Spirit] > 0)
                            WeatherManager.Instance.ForceSetWeather(Weather.Rainy);
                        ExhaustIf(firstIdx);
                    }
                    else if(handCount > 1)
                        SetSuc(6);
                }
            }

            if(weather == Weather.Foggy) {
                if(AnyNameid("strange_pipe", out _)) {
                    SetSuc(7);
                    var trgTime = events[7].TriggeredTimes;
                    if(trgTime == 1)
                        SetSuc(8);
                    else if(trgTime == 2)
                        SetSuc(9);
                }

                if(AnyNameid("frame_of_paradox", out _)) {
                    SetSuc(10);
                    var trgTime = events[10].TriggeredTimes;
                    if(trgTime == 1)
                        SetSuc(11);
                    else if(trgTime == 2)
                        SetSuc(12);
                }

                if(locDict["beacon"].Available && AnyNameid("heart_of_mountain", out _) &&
                   AnyNameid("dragon_torch", out _) && AnyNameid("potion_of_woods", out var idxPoW)) {
                    SetSuc(13);
                    ExhaustIf(idxPoW);
                }
                else
                    SetSuc(14);
            }

            AddHand();
        }
    }
}
