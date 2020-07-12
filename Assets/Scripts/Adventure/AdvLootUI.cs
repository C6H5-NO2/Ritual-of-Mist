using System.Collections.Generic;
using ThisGame.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace ThisGame.Adventure {
    public class AdvLootUI : MonoBehaviour {
        public Text descText;
        public Button allBagBtn, inSceneBtn;
        public GeneralUI.GridLayoutControl gridLayout;


        private Dictionary<Items.ItemDescription, int> toBag, toScene;

        public void SetLoot(Dictionary<Items.ItemDescription, int> loots) {
            if(loots is null || loots.Count == 0) {
                Destroy(gameObject);
                return;
            }
            toBag = loots;
            toScene = new Dictionary<Items.ItemDescription, int>();
            foreach(var loot in loots) {
                gridLayout.Add(loot.Key, loot.Value);
            }
        }


        private void PushItems() {
            // todo: put all in bag
            Debug.Log("To bag: ");
            foreach(var item in toBag) {
                Debug.Log(item.Key.name + ": " + item.Value);
            }
            Debug.Log("------------------------");

            foreach(var item in toScene) {
                for(var i = 0; i < item.Value; ++i) {
                    var go = item.Key.Instantiate(InSceneObjRef.Instance.CameraItems);
                    UiltFunc.RandPosDelta(go.transform, 250, 200);
                }
            }
            Destroy(gameObject);
        }


        private void OnAllBagBtnClick() {
            PushItems();
        }


        private void OnInSceneBtnClick() {
            if(!gridLayout.HasClickedItem)
                return;

            var clickedItem = gridLayout.ClickedItem;
            var desc = clickedItem.ItemDesc;

            const int numTaken = 1;

            // add to toScene
            toScene.EaddNset(desc, numTaken);

            // take from UI
            var uiCount = clickedItem.ItemCount;
            if(uiCount <= numTaken)
                gridLayout.RemoveClickedItem();
            else
                clickedItem.ItemCount = uiCount - numTaken;

            // take from toBag
            var toBagCount = toBag[desc];
            if(toBagCount <= numTaken) {
                toBag.Remove(desc);
                if(toBag.Count == 0)
                    PushItems();
            }
            else
                toBag[desc] = toBagCount - numTaken;
        }


        private void UpdateDescText(GeneralUI.GridItemControl item) {
            //descText.text = item == null ? "" : item.ItemDesc.desc;
            descText.text = gridLayout.HasClickedItem ? item.ItemDesc.desc : "";
        }


        private void Awake() {
            descText.text = "";
            // mind life time!
            allBagBtn.onClick.AddListener(OnAllBagBtnClick);
            inSceneBtn.onClick.AddListener(OnInSceneBtnClick);
            gridLayout.OnClickedItemChanged = UpdateDescText;
        }
    }
}
