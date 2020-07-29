using System.Collections;
using UnityEngine;

namespace ThisGame.Brew {
    /// <summary> add to dummy object when starting brewing </summary>
    public class BrewCallback : MonoBehaviour {
        private uint product;
        private BrewUI brewUI;

        public void StartBrew(uint product, BrewUI brewUI) {
            this.product = product;
            this.brewUI = brewUI;
            StartCoroutine(StartBrew());
        }

        private IEnumerator StartBrew() {
            foreach(var slot in brewUI.slots)
                slot.LockItem();
            brewUI.SetState(PotState.BrewPrg);
            const float brewTime = 3.5f;
            yield return new WaitForSeconds(brewTime);

            foreach(var slot in brewUI.slots)
                slot.UnlockItem();
            var msgPos = brewUI.isActiveAndEnabled ? BrewMsgUI.MsgPos.AboveBrewUI : BrewMsgUI.MsgPos.AbovePot;
            if(product == 0)
                brewUI.CreateMsgUI(BrewMsgUI.MsgType.BrewFail, msgPos);
            else {
                brewUI.AddToEmptySlot(product);
                brewUI.CreateMsgUI(BrewMsgUI.MsgType.BrewSuc, msgPos);
            }
            brewUI.SetState(PotState.CraftPrep);
            Destroy(gameObject);
        }
    }
}
