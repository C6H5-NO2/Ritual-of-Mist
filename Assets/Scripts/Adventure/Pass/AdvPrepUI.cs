using ThisGame.GeneralUI;
using ThisGame.Items;
using ThisGame.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace ThisGame.Adventure {
    public class AdvPrepUI : MonoBehaviour {
        public Image locImage;
        public Button confirmBtn;
        public SingleSlotDrop[] slots;
        public GameObject advPrgUIPrefab;

        private Image bg;

        private LocationData location;
        public LocationData Location {
            get => location;
            set {
                location = value;
                bg.sprite = location.uibgs[0];
                locImage.sprite = location.image;
            }
        }

        private void OnConfirm() {
            uint GetId(ItemDescription desc) => desc == null ? 0 : desc.id;
            var obj = Instantiate(advPrgUIPrefab, InSceneObjRef.Instance.CustomUI, false);
            obj.transform.localPosition = transform.localPosition;
            obj.GetComponent<AdvPrgUI>()
               .StartAdv(location,
                         (GetId(slots[0].Item), GetId(slots[1].Item), GetId(slots[2].Item)),
                         TimeManager.Instance.DayTime,
                         WeatherManager.Instance.DayWeather);
            Destroy(gameObject);
        }

        private void Awake() {
            confirmBtn.onClick.AddListener(OnConfirm);
            bg = GetComponent<Image>();
        }
    }
}
