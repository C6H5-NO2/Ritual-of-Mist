using ThisGame.Items;
using ThisGame.Letter;
using ThisGame.Utils;

namespace ThisGame.Adventure.EvTr {
    public class EvTrWitchHouse : EvTrBase {
        protected override void OnTrigger() {
            if(weather != Weather.Sunny || !AnyPropGt(out var idxMetal, ItemProperty.Metal)) {
                SetSuc(0);
                return;
            }
            ExhaustIf(idxMetal);

            if(events[1].TriggeredTimes == 0)
                SetSuc(1);
            else
                SetSuc(2);

            var hasFish = AnyNameid("flavoured_fish", out var idxFish);
            var hasMeat = AnyNameid("meat_roll", out var idxMeat);
            if(!hasFish && !hasMeat)
                SetSuc(3);
            if(hasFish && events[4].TriggeredTimes == 0) {
                SetSuc(4);
                LetterManager.Instance.AddReceivedLetter("letter_witch_1");
                ExhaustIf(idxFish);
                AddNameid("frame_of_paradox");
            }
            if(hasMeat && events[5].TriggeredTimes == 0) {
                SetSuc(5);
                LetterManager.Instance.AddReceivedLetter("letter_witch_2");
                ExhaustIf(idxMeat);
                AddNameid("sextant");
            }

            SetSuc(6);

            AddHand();
        }
    }
}
