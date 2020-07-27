using System.Linq;
using ThisGame.Adventure;
using UnityEngine;
using UnityEngine.UI;

namespace ThisGame.Picbook {
    public class PicbookUI : MonoBehaviour {
        public Image locImg;
        public Text locText, payText;
        public Button leftBtn, rightBtn;
        /// <summary> unused </summary>
        public Button payBtn;
        public PicbookLabelList labelList;


        private EventData[] activeEvents;
        private int focusEvent;

        private void UpdateFocusEvent(int idx = 0) {
            if(idx < 0 || idx >= activeEvents.Length)
                return;
            locImg.sprite = activeEvents[idx].image;
            locText.text = activeEvents[idx].desc;
            leftBtn.interactable = idx > 0;
            rightBtn.interactable = idx < activeEvents.Length - 1;
            focusEvent = idx;
        }


        private void OnClickedItemChanged(LocationData loc) {
            if(loc == null || loc.events is null || loc.events.Length == 0) {
                DisactiveChildren();
                return;
            }
            activeEvents = loc.events.Where(ev => ev.TriggeredTimes > 0).ToArray();
            if(activeEvents.Length == 0) {
                DisactiveChildren();
                return;
            }

            locImg.gameObject.SetActive(true);
            locText.gameObject.SetActive(true);
            leftBtn.gameObject.SetActive(true);
            rightBtn.gameObject.SetActive(true);
            //payBtn.gameObject.SetActive(true);

            UpdateFocusEvent();
        }


        private void DisactiveChildren() {
            locImg.gameObject.SetActive(false);
            locText.gameObject.SetActive(false);
            leftBtn.gameObject.SetActive(false);
            rightBtn.gameObject.SetActive(false);
            //payBtn.gameObject.SetActive(false);
        }


        private void OnEnable() {
            Utils.TimeManager.Instance.Pause = true;
            labelList.Clear();
            foreach(var location in LocDatDict.Instance.Dict)
                if(location.Available)
                    labelList.Add(location);
            DisactiveChildren();
        }

        private void OnDisable() {
            Utils.TimeManager.Instance.Pause = false;
        }

        private void Awake() {
            leftBtn.onClick.AddListener(delegate { UpdateFocusEvent(focusEvent - 1); });
            rightBtn.onClick.AddListener(delegate { UpdateFocusEvent(focusEvent + 1); });
            labelList.OnClickedItemChanged = OnClickedItemChanged;
        }
    }
}
