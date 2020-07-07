using UnityEngine;
using UnityEngine.UI;

namespace Adventure {
    public class AdvPrepUI : MonoBehaviour {
        public Text locName;
        public Image locImg;
        public Button startConfirm;
        public GameObject advPrgUIPrefab;

        private LocationData location;
        public LocationData Location {
            set {
                location = value;
                if(location == null)
                    return;
                locName.text = location.locName;
                locImg.sprite = location.locImage;
            }
        }


        private void StartAdventure() {
            var obj = Instantiate(advPrgUIPrefab, transform.root, false);
            obj.transform.localPosition = transform.localPosition;
            obj.GetComponent<AdvPrgUI>().Location = location;
            // todo: pass items and states
            Destroy(gameObject);
        }


        private void Awake() {
            startConfirm.onClick.AddListener(StartAdventure);
        }
    }
}
