using System;
using ThisGame.NewAdv;
using UnityEngine;

namespace ThisGame.Picbook {
    public class PicbookLabelList : MonoBehaviour {
        public GameObject labelPrefab;

        private Transform listTransfrom;

        public Action<LocationData> OnClickedItemChanged { private get; set; }
        private PicbookLabelHolder clickedlLabel;
        public PicbookLabelHolder ClickedLabel {
            get => clickedlLabel;
            set {
                if(clickedlLabel != null)
                    clickedlLabel.labelTrans.anchoredPosition = PicbookLabelHolder.UnfocusPos;
                if(value == null) {
                    clickedlLabel = null;
                    OnClickedItemChanged?.Invoke(null);
                }
                else {
                    clickedlLabel = value;
                    clickedlLabel.labelTrans.anchoredPosition = Vector2.zero;
                    OnClickedItemChanged?.Invoke(clickedlLabel.Location);
                }
            }
        }

        public void Add(LocationData loc) {
            var go = Instantiate(labelPrefab, listTransfrom, false);
            var scrp = go.GetComponent<PicbookLabelHolder>();
            scrp.ParentList = this;
            scrp.Location = loc;
        }

        public void Clear() {
            foreach(Transform label in listTransfrom)
                Destroy(label.gameObject);
            ClickedLabel = null;
        }

        private void Awake() {
            listTransfrom = transform;
        }
    }
}
