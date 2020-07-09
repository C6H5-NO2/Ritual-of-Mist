using System.Collections.Generic;
using ThisGame.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace ThisGame.Adventure {
    public class AdvPrepUI : MonoBehaviour {
        public Text locName;
        public Image locImg;
        public Button startConfirm;
        public Transform itemList;
        public GameObject advPrgUIPrefab;

        private LocationData location;
        public LocationData Location {
            set {
                location = value;
                if(location == null)
                    return;
                locName.text = location.name;
                locImg.sprite = location.image;
            }
        }


        private void StartAdventure() {
            var obj = Instantiate(advPrgUIPrefab, transform.root, false);
            obj.transform.localPosition = transform.localPosition;

            var items = new Dictionary<Items.ItemDescription, uint>();
            foreach(Transform item in itemList) {
                var holder = item.GetComponent<Items.ItemDescHolder>();
                // should not be null
                //if(holder == null)
                //    continue;
                items.EaddNset(holder.Description, 1);
            }

            var scrp = obj.GetComponent<AdvPrgUI>();
            scrp.StartAdv(location,
                          items,
                          new TimeWeather(TimeWeatherManager.Instance.DayTime,
                                          TimeWeatherManager.Instance.DayWeather));
            Destroy(gameObject);
        }


        private void Awake() {
            startConfirm.onClick.AddListener(StartAdventure);
        }
    }
}
