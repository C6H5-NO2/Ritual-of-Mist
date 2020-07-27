using System;
using ThisGame.Adventure;
using UnityEngine;

namespace ThisGame.Picbook {
    public class PicbookLabelList : MonoBehaviour {
        public GameObject labelPrefab;
        public Transform listTransfrom;

        public Action<LocationData> OnClickedItemChanged { private get; set; }
        private PicbookLabelHolder clickedlLabel;
        public PicbookLabelHolder ClickedLabel {
            get => clickedlLabel;
            set {
                if(!(clickedlLabel is null) && clickedlLabel.isActiveAndEnabled) {
                    clickedlLabel.IsClicked = false;
                    clickedlLabel.SmoothMoveRight();
                }
                if(value == null) {
                    clickedlLabel = null;
                    OnClickedItemChanged?.Invoke(null);
                }
                else {
                    clickedlLabel = value;
                    clickedlLabel.IsClicked = true;
                    clickedlLabel.SmoothMoveLeft();
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
    }
}
