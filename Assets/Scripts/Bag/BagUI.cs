using System.Collections.Generic;
using System.Text;
using ThisGame.GeneralUI;
using ThisGame.Items;
using ThisGame.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace ThisGame.Bag {
    public class BagUI : MonoBehaviour {
        public Button closeBtn, toSceneBtn;
        public Text descText, propText, moneyText;
        public GridLayoutControl gridLayout;


        private Dictionary<ItemDescription, int> toScene;


        public void RefreshAll() {
            gridLayout.ClickedItem = null;

            // todo: use callback
            moneyText.text = BagManager.Instance.Gold + " G";

            var inBag = BagManager.Instance.InBag;
            if(inBag is null)
                return;
            gridLayout.gameObject.SetActive(false);

            // todo: use dirty flag
            gridLayout.Clear();

            foreach(var item in inBag)
                gridLayout.Add(item.Key, item.Value);
            gridLayout.gameObject.SetActive(true);
        }


        private void OnCloseBtnClicked() {
            gameObject.SetActive(false);
        }


        private void OnToSceneBtnClicked() {
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
            var inBag = BagManager.Instance.InBag;
            var toBagCount = inBag[desc];
            if(toBagCount <= numTaken)
                inBag.Remove(desc);
            else
                inBag[desc] = toBagCount - numTaken;
        }


        private void PushItems() {
            foreach(var item in toScene) {
                for(var i = 0; i < item.Value; ++i) {
                    var go = item.Key.Instantiate(InSceneObjRef.Instance.OnTable);
                    UiltFunc.RandPosDelta(go.transform, 250, 200);
                }
            }
            toScene.Clear();
        }


        private void UpdateText(GridItemControl item) {
            if(gridLayout.HasClickedItem) {
                descText.text = item.ItemDesc.desc;
                var prop = item.ItemDesc.properties;
                var sb = new StringBuilder(32);
                sb.AppendFormat("刀刃：{0}", prop[(int)ItemProperty.Metal])
                  .AppendLine()
                  .AppendFormat("灵气：{0}", prop[(int)ItemProperty.Spirit])
                  .AppendLine()
                  .AppendFormat("能量：{0}", prop[(int)ItemProperty.Energy])
                  .AppendLine()
                  .AppendFormat("食物：{0}", prop[(int)ItemProperty.Food]);
                propText.text = sb.ToString();
            }
            else {
                descText.text = "";
                propText.text = "";
            }
        }


        private void Awake() {
            toScene = new Dictionary<ItemDescription, int>();
        }


        private void OnEnable() {
            TimeManager.Instance.Pause = true;
            gridLayout.OnClickedItemChanged = UpdateText;
            closeBtn.onClick.AddListener(OnCloseBtnClicked);
            toSceneBtn.onClick.AddListener(OnToSceneBtnClicked);
            RefreshAll();
        }


        private void OnDisable() {
            PushItems();
            //transform.root.gameObject.SetActive(false);

            gridLayout.OnClickedItemChanged = null;
            closeBtn.onClick.RemoveAllListeners();
            toSceneBtn.onClick.RemoveAllListeners();

            TimeManager.Instance.Pause = false;
        }
    }
}
