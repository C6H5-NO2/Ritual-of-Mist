using UnityEngine;
using UnityEngine.UI;

namespace ThisGame.Adventure {
    public class LocDescUI : MonoBehaviour {
        public Text locName, locDesc, locCost;
        public Image locImage;
        public Button comfirmBtn;
        public GameObject advPrepUIPrefab;

        private LocationData location;
        public LocationData Location {
            get => location;
            set {
                location = value;
                locName.text = location.name;
                locDesc.text = location.desc;
                locImage.sprite = location.image;
                var goldCost = location.goldCost;
                locCost.text = $"{goldCost} G";
                if(Bag.BagManager.Instance.Gold >= goldCost) {
                    locCost.color = Color.yellow;
                    //comfirmBtn.interactable = true;
                }
                else {
                    locCost.color = Color.black;
                    //comfirmBtn.interactable = false;
                }
                FitPosition(location.locSymbolPos);
            }
        }

        private void FitPosition(Vector2 locPos) {
            Vector2 initDelta = new Vector2(270, -120),
                    padding = new Vector2(20, 12),
                    //canvasHalfsize = transform.root.GetComponent<CanvasScaler>().referenceResolution / 2
                    canvasHalfsize = new Vector2(1920, 1080) / 2,
                    //uiHalfSize = ((RectTransform)transform).sizeDelta / 2
                    uiHalfSize = new Vector2(460, 854) / 2;

            locPos += initDelta;
            var max = canvasHalfsize - padding - uiHalfSize;
            if(locPos.x > max.x)
                // assert left fit
                locPos.x -= initDelta.x + initDelta.x;

            locPos.y = Mathf.Clamp(locPos.y, -max.y, max.y);

            ((RectTransform)transform).anchoredPosition = locPos;
        }

        private void OnConfirm() {
            if(Bag.BagManager.Instance.Gold < location.goldCost)
                return;
            Bag.BagManager.Instance.Gold -= location.goldCost;

            var obj = Instantiate(advPrepUIPrefab, Utils.InSceneObjRef.Instance.CustomUI, false);
            Utils.UiltFunc.RandPosDelta(obj.transform, 150, 100);
            obj.GetComponent<AdvPrepUI>().Location = location;

            // map ui
            transform.parent.gameObject.SetActive(false);
        }

        private void Awake() {
            comfirmBtn.onClick.AddListener(OnConfirm);
        }
    }
}
