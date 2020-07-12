using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ThisGame.Adventure {
    public class AdvFinUI : MonoBehaviour {
        public Image evImg;
        public Text evDesc;
        public Button confirmBtn, leftBtn, rightBtn;
        public GameObject advLootUIPrefab;

        private List<LocationEvent> events;
        private int eventCount;
        private Dictionary<Items.ItemDescription, int> loots;

        public void SetEventsAndLoots(List<LocationEvent> events, Dictionary<Items.ItemDescription, int> loots) {
            this.events = events;
            this.loots = loots;
            if(events is null) {
                OnConfirmBtnClick();
                return;
            }
            eventCount = events.Count;
            SetupUI(0);
        }


        private int currEventIdx;

        private void SetupUI(int idx) {
            if(eventCount == 0) {
                //evImg.sprite = null;
                //evImg.color = new Color(1, 1, 1, 0);
                //evDesc.text = "[NO EVENTS]";
                OnConfirmBtnClick();
                return;
            }

            if(idx < 0 || idx >= eventCount)
                return;
            currEventIdx = idx;
            evImg.sprite = events[currEventIdx].image;
            evImg.color = Color.white;
            evDesc.text = events[currEventIdx].desc;
        }


        private void OnConfirmBtnClick() {
            var go = Instantiate(advLootUIPrefab, Utils.InSceneObjRef.Instance.OverlayUI, false);
            go.GetComponent<AdvLootUI>().SetLoot(loots);
            Destroy(gameObject);
        }


        private void Awake() {
            confirmBtn.onClick.AddListener(OnConfirmBtnClick);
            // mind life time!
            leftBtn.onClick.AddListener(delegate { SetupUI(currEventIdx - 1); });
            rightBtn.onClick.AddListener(delegate { SetupUI(currEventIdx + 1); });
        }
    }
}
