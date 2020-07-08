using System.Collections.Generic;
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

            var items = new List<Items.ItemDescription>();
            foreach(Transform item in itemList) {
                var holder = item.GetComponent<Items.ItemDescHolder>();
                if(holder == null)
                    continue;
                items.Add(holder.Description);
            }

            var scrp = obj.GetComponent<AdvPrgUI>();
            scrp.StartAdv(location, items);
            Destroy(gameObject);
        }


        private void Awake() {
            startConfirm.onClick.AddListener(StartAdventure);
        }
    }
}
