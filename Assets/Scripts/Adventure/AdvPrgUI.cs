using UnityEngine;
using UnityEngine.UI;

namespace Adventure {
    public class AdvPrgUI : MonoBehaviour {
        public Text locName;
        public Image locImg;

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
    }
}
