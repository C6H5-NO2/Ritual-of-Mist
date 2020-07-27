using ThisGame.Items;
using ThisGame.Letter;
using ThisGame.Utils;

namespace ThisGame.Adventure.EvTr {
    public class EvTrPlain : EvTrBase {
        protected override void OnTrigger() {
            SetSuc(0);

            if(AnyPropGt(out var idxMetal, ItemProperty.Metal)) {
                SetSuc(1);
                ExhaustIf(idxMetal);
                AddNameid("mysterious_scale");
            }

            if(time.IstAbend() && RandProb(.5f)) {
                SetSuc(2);
                LetterManager.Instance.AddReceivedLetter("letter_plain_1");
                AddNameid("mysterious_scale");
            }

            if(weather == Weather.Sunny)
                SetSuc(3);
            else
                SetSuc(4);

            if(AnyPropGt(out idxMetal, ItemProperty.Metal, 2)) {
                SetSuc(5);
                ExhaustIf(idxMetal);

                if(AnyNameid("branch_huge", out var idxBranch)) {
                    SetSuc(6);
                    exhaustMask.At(idxBranch) = true;
                    AddNameid("dragon_torch");
                }
            }

            SetSuc(7);
            if(AnyPropGt(out idxMetal, ItemProperty.Metal, 1)) {
                SetSuc(9);
                ExhaustIf(idxMetal);
                AddNameid("meat_1");
            }
            else
                SetSuc(8);

            AddHand();
        }
    }
}
