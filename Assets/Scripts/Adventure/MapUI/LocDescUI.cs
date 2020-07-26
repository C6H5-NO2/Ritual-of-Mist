using UnityEngine;
using UnityEngine.UI;

namespace ThisGame.NewAdv {
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
            // todo:
            ((RectTransform)transform).anchoredPosition = locPos;
        }

        private void OnConfirm() {
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
