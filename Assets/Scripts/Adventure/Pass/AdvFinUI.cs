using System.Collections.Generic;
using ThisGame.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace ThisGame.NewAdv {
    public class AdvFinUI : MonoBehaviour {
        public Text evDesc;
        public Image evImg, locBG;
        public Button leftBtn, rightBtn, allocBtn;
        public GameObject advLootUIPrefab;


        private List<EventData> succeedEvents;
        private List<uint> loots;

        public void SetParams(LocationData location, bool[] mask, List<uint> loots) {
            this.loots = loots;
            succeedEvents = new List<EventData>(location.events.Length);
            for(var i = 0; i < mask.Length; ++i)
                if(mask[i])
                    succeedEvents.Add(location.events[i]);

            locBG.sprite = location.uibgs[2];
            if(succeedEvents.Count > 0)
                UpdateUI(0);
            else
                OnAlloc();
        }


        private int currEvent;

        private void UpdateUI(int idx) {
            leftBtn.interactable = idx > 0;
            rightBtn.interactable = idx < succeedEvents.Count - 1;
            if(idx < 0 || idx >= succeedEvents.Count)
                return;
            evDesc.text = succeedEvents[idx].desc;
            evImg.sprite = succeedEvents[idx].image;
            currEvent = idx;
        }


        private void OnAlloc() {
            var go = Instantiate(advLootUIPrefab, InSceneObjRef.Instance.OverlayUI, false);
            go.GetComponent<AdvLootUI>().SetLoots(loots);
            Destroy(gameObject);
        }


        private void Awake() {
            TimeManager.Instance.Pause = true;

            leftBtn.onClick.AddListener(delegate { UpdateUI(currEvent - 1); });
            rightBtn.onClick.AddListener(delegate { UpdateUI(currEvent + 1); });
            allocBtn.onClick.AddListener(OnAlloc);
        }
    }
}
