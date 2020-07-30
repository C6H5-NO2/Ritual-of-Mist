using System.Collections.Generic;
using ThisGame.GeneralUI;
using ThisGame.Items;
using ThisGame.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace ThisGame.Adventure {
    public class AdvLootUI : MonoBehaviour {
        public Text descText;
        public Button allBagBtn, toSceneBtn;
        public GridLayoutControl gridLayout;

        private Dictionary<ItemDescription, int> toBag, toScene;


        public void SetLoots(List<uint> loots) {
            if(loots is null || loots.Count == 0) {
                Destroy(gameObject);
                return;
            }

            var itemDict = ItemDescDict.Instance.Dict;
            toBag = new Dictionary<ItemDescription, int>();
            foreach(var loot in loots)
                toBag.EaddNset(itemDict[loot], 1);
            toScene = new Dictionary<ItemDescription, int>();
            foreach(var kvp in toBag)
                gridLayout.Add(kvp.Key, kvp.Value);
        }


        private void OnAllBagBtnClick() {
            var inBag = Bag.BagManager.Instance.InBag;
            foreach(var kvp in toBag)
                inBag.EaddNset(kvp.Key, kvp.Value);

            foreach(var kvp in toScene)
                for(var i = 0; i < kvp.Value; ++i) {
                    var go = kvp.Key.Instantiate(InSceneObjRef.Instance.OnTable);
                    UiltFunc.RandPosDelta(go.transform, 108, 72);
                }

            Destroy(gameObject);
        }


        private void OnToSceneBtnClick() {
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
                    OnAllBagBtnClick();
            }
            else
                toBag[desc] = toBagCount - numTaken;
        }


        private void UpdateDescText(GridItemControl item) {
            if(gridLayout.HasClickedItem) {
                var desc = item.ItemDesc;
                // todo: format description
                descText.text = $"{desc.name}：\n{desc.desc}";
            }
            else
                descText.text = "";
        }


        private void OnDestroy() {
            TimeManager.Instance.Pause = false;
        }


        private void Awake() {
            allBagBtn.onClick.AddListener(OnAllBagBtnClick);
            toSceneBtn.onClick.AddListener(OnToSceneBtnClick);
            gridLayout.OnClickedItemChanged = UpdateDescText;
        }
    }
}
