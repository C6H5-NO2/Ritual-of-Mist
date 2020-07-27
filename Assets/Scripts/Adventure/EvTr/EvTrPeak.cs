using ThisGame.Items;
using ThisGame.Letter;

namespace ThisGame.Adventure.EvTr {
    public class EvTrPeak : EvTrBase {
        protected override void OnTrigger() {
            if(AnyPropGt(out var idx, ItemProperty.Energy)) {
                SetSuc(1);
                SetSuc(2);

                if(RandProb(.3f)) {
                    SetSuc(3);
                    AddNameid("ice");
                }

                if(time.IstAbend() && AnyNameid("sextant", out _)) {
                    SetSuc(4);
                    SetSuc(5);
                    LocDatDict.Instance.Dict["beacon"].Available = true;
                }

                ExhaustIf(idx);
            }
            else {
                SetSuc(0);
                LetterManager.Instance.AddReceivedLetter("letter_peak_1");
            }

            AddHand();
        }
    }
}
