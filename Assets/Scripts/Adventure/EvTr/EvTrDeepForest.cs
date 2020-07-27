using ThisGame.Items;
using ThisGame.Letter;
using ThisGame.Utils;

namespace ThisGame.Adventure.EvTr {
    public class EvTrDeepForest : EvTrBase {
        protected override void OnTrigger() {
            SetSuc(0);
            SetSuc(1);
            AddNameid("pupafruit");

            if(AnyPropGt(out var idxMetal, ItemProperty.Metal, 1)) {
                SetSuc(2);
                LetterManager.Instance.AddReceivedLetter("letter_deepforest_1");
                ExhaustIf(idxMetal);
                AddNameid("acorn");
            }
            else
                SetSuc(3);

            if(AnyPropGt(out var idxFood, ItemProperty.Food)) {
                if(weather == Weather.Foggy)
                    SetSuc(8);
                else {
                    SetSuc(5);
                    if(events[5].TriggeredTimes == 1) {
                        SetSuc(6);
                        AddNameid("branch_huge");
                    }
                    else
                        SetSuc(7);
                }

                ExhaustIf(idxFood);
            }
            else
                SetSuc(4);

            AddHand();
        }
    }
}
